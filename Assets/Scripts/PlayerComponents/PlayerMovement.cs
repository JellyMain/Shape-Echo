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
        public bool isMoving;
        public event Action DashStarted;
        public event Action DashEnded;


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


        private void Update()
        {
            RotateToMoveDirection();
        }


        private void Move()
        {
            if (isDashing) return;


            Vector2 moveDirection = inputService.CurrentInput.GetNormalizedMoveInput();

            isMoving = moveDirection != Vector2.zero;

            rb2d.velocity = moveDirection * moveSpeed;
        }


        private void OnDash()
        {
            Dash().Forget();
        }


        private async UniTaskVoid Dash()
        {
            if (!isDashing)
            {
                DashStarted?.Invoke();
                isDashing = true;
                Vector2 moveDirection = inputService.CurrentInput.GetNormalizedMoveInput();
                rb2d.velocity = moveDirection * dashSpeed;
                transform.right = moveDirection;

                int delay = dashDuration.SecondsToMilliseconds();
                await UniTask.Delay(delay);

                isDashing = false;
                DashEnded?.Invoke();
            }
        }


        private void RotateToMoveDirection()
        {
            Vector2 moveDirection = inputService.CurrentInput.GetNormalizedMoveInput();

            float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;

            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
            
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
