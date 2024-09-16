using System;
using Infrastructure;
using Input.Services;
using UnityEngine;
using Weapons;


namespace PlayerComponents
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private Transform weaponHand;
        [SerializeField] private Collider2D col;
        public WeaponBase WeaponBasePrefab { get; set; }
        public float WeaponRotationSpeed { get; set; }
        private WeaponBase currentWeapon;
        private int maxAmmo;
        private int currentAmmo;
        private float reloadDuration;
        private Vector2 mousePosition;
        private InputService inputService;
        private float shotCooldown;
        private float shotTimer;
        private bool isShooting;
        private float reloadTimer;
        private bool isReloading;
        

        public event Action<int> Reloaded;
        public event Action<int> Shot;
        public event Action<int> WeaponSet;
        public event Action<float> ReloadStarted;


        private void Awake()
        {
            inputService = ServiceLocator.Container.Single<InputService>();
        }


        private void Start()
        {
            CreateWeapon();
        }


        private void OnEnable()
        {
            inputService.CurrentInput.ShotPressed += StartShooting;
            inputService.CurrentInput.ShotReleased += StopShooting;
            inputService.CurrentInput.ReloadPressed += StartReload;
        }


        private void OnDisable()
        {
            inputService.CurrentInput.ShotPressed -= StartShooting;
            inputService.CurrentInput.ShotReleased -= StopShooting;
            inputService.CurrentInput.ReloadPressed -= StartReload;
        }


        private void Update()
        {
            RotateWeapon();
            TickShot();
            TickReloadTimer();
        }


        private void CreateWeapon()
        {
            currentWeapon = Instantiate(WeaponBasePrefab, weaponHand.transform.position, Quaternion.identity,
                weaponHand);

            shotCooldown = currentWeapon.ShotCooldown;
            maxAmmo = currentWeapon.MaxAmmo;
            reloadDuration = currentWeapon.ReloadDuration;

            WeaponSet?.Invoke(maxAmmo);
        }


        public void SetNewWeapon(WeaponBase weapon)
        {
            Destroy(currentWeapon.gameObject);

            weapon.transform.SetParent(weaponHand);
            weapon.transform.position = weaponHand.position;

            currentWeapon = weapon;
            shotCooldown = currentWeapon.ShotCooldown;
            maxAmmo = currentWeapon.MaxAmmo;
            reloadDuration = currentWeapon.ReloadDuration;

            WeaponSet?.Invoke(maxAmmo);
        }


       


        private void StartShooting()
        {
            isShooting = true;
        }


        private void StopShooting()
        {
            shotTimer = 0;
            isShooting = false;
        }


        private void TickShot()
        {
            if (isShooting)
            {
                shotTimer -= Time.deltaTime;

                if (shotTimer <= 0)
                {
                    shotTimer = shotCooldown;

                    Vector2 direction = (mousePosition - (Vector2)currentWeapon.transform.position).normalized;
                    currentWeapon.Shoot(direction);
                    Shot?.Invoke(currentWeapon.CurrentAmmo);
                }
            }
        }


        private void TickReloadTimer()
        {
            if (isReloading)
            {
                reloadTimer -= Time.deltaTime;

                if (reloadTimer <= 0)
                {
                    isReloading = false;
                    currentWeapon.Reload();
                    Reloaded?.Invoke(currentWeapon.CurrentAmmo);
                }
            }
        }


        private void StartReload()
        {
            reloadTimer = reloadDuration;
            isReloading = true;
            
            ReloadStarted?.Invoke(reloadDuration);
        }


        private void CancelReload()
        {
            reloadTimer = reloadDuration;
        }


        private void RotateWeapon()
        {
            if (currentWeapon != null)
            {
                mousePosition = inputService.CurrentInput.GetWorldMousePosition();

                Vector2 targetPosition = mousePosition - (Vector2)currentWeapon.transform.position;

                float targetAngle = Mathf.Atan2(targetPosition.y, targetPosition.x) * Mathf.Rad2Deg;

                Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

                currentWeapon.transform.rotation =
                    Quaternion.Slerp(currentWeapon.transform.rotation, targetRotation,
                        Time.deltaTime * WeaponRotationSpeed);
            }
        }
    }
}
