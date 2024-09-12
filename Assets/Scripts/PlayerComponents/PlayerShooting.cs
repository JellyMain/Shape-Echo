using System;
using Infrastructure;
using Input.Services;
using UnityEngine;


namespace PlayerComponents
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private Weapon currentWeapon;
        [SerializeField] private float weaponRotationSpeed;
        public Vector2 MousePosition { get; private set; }
        private InputService inputService;


        private void Awake()
        {
            inputService = ServiceLocator.Container.Single<InputService>();
        }


        private void OnEnable()
        {
            inputService.CurrentInput.Shot += currentWeapon.Shoot;
        }


        private void OnDisable() { }



        private void Update()
        {
            RotateWeapon();
        }



        private void RotateWeapon()
        {
           MousePosition = inputService.CurrentInput.GetWorldMousePosition();

            float targetAngle = Mathf.Atan2(MousePosition.y, MousePosition.x) * Mathf.Rad2Deg;

            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

            currentWeapon.transform.rotation =
                Quaternion.Slerp(currentWeapon.transform.rotation, targetRotation, Time.deltaTime * weaponRotationSpeed);
        }
    }
}
