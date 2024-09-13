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
        public WeaponBase WeaponBasePrefab { get; set; }
        public float WeaponRotationSpeed { get; set; }
        private WeaponBase currentWeaponBase;
        private Vector2 mousePosition;
        private InputService inputService;


        private void Awake()
        {
            inputService = ServiceLocator.Container.Single<InputService>();
        }


        private void OnEnable()
        {
            inputService.CurrentInput.Shot += OnShot;
        }


        private void OnDisable()
        {
            inputService.CurrentInput.Shot -= OnShot;
        }


        private void Update()
        {
            RotateWeapon();
        }



        public void CreateWeapon()
        {
            currentWeaponBase = Instantiate(WeaponBasePrefab, weaponHand.transform.position, Quaternion.identity, weaponHand);
        }


        private void OnShot()
        {
            Vector2 direction = (mousePosition - (Vector2)currentWeaponBase.transform.position).normalized;
            currentWeaponBase.Shoot(direction);
        }


        private void RotateWeapon()
        {
            mousePosition = inputService.CurrentInput.GetWorldMousePosition();

            Vector2 targetPosition = mousePosition - (Vector2)currentWeaponBase.transform.position;

            float targetAngle = Mathf.Atan2(targetPosition.y, targetPosition.x) * Mathf.Rad2Deg;

            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

            currentWeaponBase.transform.rotation =
                Quaternion.Slerp(currentWeaponBase.transform.rotation, targetRotation,
                    Time.deltaTime * WeaponRotationSpeed);
        }
    }
}
