using System;
using Cysharp.Threading.Tasks;
using Infrastructure;
using Input.Services;
using Sirenix.OdinInspector;
using UnityEngine;
using Utils;


namespace PlayerComponents
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField, Required] private Rigidbody2D rb2d;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float dashSpeed = 30;
        [SerializeField] private float dashDuration = 1;
        [SerializeField] private float moveSpeed;
        private InputService inputService;
        private bool isDashing;


        private void Awake()
        {
            inputService = ServiceLocator.Container.Single<InputService>();
        }


        private void OnEnable()
        {
            inputService.CurrentInput.Dashed += OnDash;
        }


        private void OnDisable()
        {
            inputService.CurrentInput.Dashed -= OnDash;
        }


        private void FixedUpdate()
        {
            Move();
        }
        

        private void Move()
        {
            if (isDashing) return;

            Vector2 moveDirection = inputService.CurrentInput.GetNormalizedMoveInput();
            rb2d.velocity = moveDirection * moveSpeed;
            RotateToMoveDirection(moveDirection);
        }


        private void OnDash()
        {
            Dash().Forget();
        }


        private async UniTaskVoid Dash()
        {
            if (!isDashing)
            {
                isDashing = true;
                Vector2 moveDirection = inputService.CurrentInput.GetNormalizedMoveInput();
                rb2d.velocity = moveDirection * dashSpeed;
                transform.up = moveDirection;
                
                int delay = dashDuration.SecondsToMilliseconds();
                await UniTask.Delay(delay);

                isDashing = false;
            }
        }


        private void RotateToMoveDirection(Vector2 moveDirection)
        {
            transform.up = Vector2.Lerp(transform.up, moveDirection, Time.deltaTime * rotationSpeed);
        }
    }
}
