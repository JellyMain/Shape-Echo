using System;
using Cysharp.Threading.Tasks;
using Infrastructure;
using Input.Interfaces;
using Input.Services;
using UnityEngine;
using Weapons;
using Weapons.Bullets;
using Zenject;


namespace PlayerComponents
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private Transform weaponHand;
        public float WeaponRotationSpeed { get; set; }
        public WeaponBase CurrentWeapon { get; set; }
        private Vector2 mousePosition;
        private IInput inputService;

        public bool IsReloading { get; private set; }
        public float ReloadTimer { get; private set; }
        private bool isShooting;


        public event Action<float> ReloadStarted;
        public event Action<int> Reloaded;
        public event Action<int> Shot;
        public event Action<int, AmmoType> WeaponSet;

        
        [Inject]
        private void Construct(IInput inputService)
        {
            this.inputService = inputService;
        }
        

        private void Start()
        {
            CreateWeapon();
        }


        private void OnEnable()
        {
            inputService.ShotPressed += StartShooting;
            inputService.ShotReleased += StopShooting;
            inputService.ReloadPressed += StartReload;
        }


        private void OnDisable()
        {
            inputService.ShotPressed -= StartShooting;
            inputService.ShotReleased -= StopShooting;
            inputService.ReloadPressed -= StartReload;
        }


        private void Update()
        {
            RotateWeapon();
        }


        private void CreateWeapon()
        {
            CurrentWeapon = Instantiate(CurrentWeapon.Prefab, weaponHand.transform.position, Quaternion.identity,
                weaponHand);

            WeaponSet?.Invoke(CurrentWeapon.MaxAmmo, CurrentWeapon.AmmoType);
        }


        public void SetNewWeapon(WeaponBase weapon)
        {
            Destroy(CurrentWeapon.gameObject);

            weapon.transform.SetParent(weaponHand);
            weapon.transform.position = weaponHand.position;

            CurrentWeapon = weapon;

            WeaponSet?.Invoke(CurrentWeapon.MaxAmmo, CurrentWeapon.AmmoType);
        }



        private void StartShooting()
        {
            Shoot().Forget();
        }


        private async UniTaskVoid Shoot()
        {
            isShooting = true;
            float shotTimer = CurrentWeapon.ShotCooldown;

            while (isShooting)
            {
                shotTimer += Time.deltaTime;

                if (shotTimer > CurrentWeapon.ShotCooldown)
                {
                    shotTimer = 0;

                    Vector2 direction = (mousePosition - (Vector2)CurrentWeapon.transform.position).normalized;
                    CurrentWeapon.Shoot(direction);
                    CancelReload();
                    Shot?.Invoke(CurrentWeapon.CurrentAmmo);
                }

                await UniTask.Yield();
            }
        }


        private void StopShooting()
        {
            isShooting = false;
        }



        private void StartReload()
        {
            Reload().Forget();
        }


        private async UniTaskVoid Reload()
        {
            IsReloading = true;
            ReloadTimer = 0;

            ReloadStarted?.Invoke(CurrentWeapon.ReloadDuration);

            while (IsReloading)
            {
                ReloadTimer += Time.deltaTime;

                if (ReloadTimer >= CurrentWeapon.ReloadDuration)
                {
                    CurrentWeapon.Reload();
                    Reloaded?.Invoke(CurrentWeapon.CurrentAmmo);
                    IsReloading = false;
                }

                await UniTask.Yield();
            }
        }


        private void CancelReload()
        {
            IsReloading = false;
            ReloadTimer = 0;
            Reloaded?.Invoke(0);
        }


        private void RotateWeapon()
        {
            if (CurrentWeapon != null)
            {
                mousePosition = inputService.GetWorldMousePosition();

                Vector2 targetPosition = mousePosition - (Vector2)CurrentWeapon.transform.position;

                float targetAngle = Mathf.Atan2(targetPosition.y, targetPosition.x) * Mathf.Rad2Deg;

                Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

                CurrentWeapon.transform.rotation =
                    Quaternion.Slerp(CurrentWeapon.transform.rotation, targetRotation,
                        Time.deltaTime * WeaponRotationSpeed);
            }
        }
    }
}
