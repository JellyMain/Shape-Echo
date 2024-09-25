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
        private InputService inputService;
        public float RotationSpeed { get; set; }
        public float DashSpeed { get; set; }
        public float DashDuration { get; set; }
        public float MoveSpeed { get; set; }
        public bool IsMoving { get; private set; }
        public Vector2 MoveDirection { get; private set; }
        private bool isDashing;

        public event Action DashStarted;
        public event Action DashEnded;


        private void Awake()
        {
            inputService = ServiceLocator.Container.Single<InputService>();
        }


        private void OnEnable()
        {
            inputService.CurrentInput.DashPressed += OnDash;
        }


        private void OnDisable()
        {
            inputService.CurrentInput.DashPressed -= OnDash;
        }


        private void FixedUpdate()
        {
            Move();
        }


        private void Update()
        {
            // RotateToMoveDirection();
        }


        private void Move()
        {
            if (isDashing) return;
            
            MoveDirection = inputService.CurrentInput.GetNormalizedMoveInput();
            IsMoving = MoveDirection != Vector2.zero;

            rb2d.velocity = MoveDirection * MoveSpeed;
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
                rb2d.velocity = moveDirection * DashSpeed;
                transform.right = moveDirection;

                int delay = DashDuration.SecondsToMilliseconds();
                await UniTask.Delay(delay);

                isDashing = false;
                DashEnded?.Invoke();
            }
        }


        private void RotateToMoveDirection()
        {
            Vector2 moveDirection = inputService.CurrentInput.GetNormalizedMoveInput();

            Quaternion targetRotation = DataUtility.DirectionToQuaternion(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * RotationSpeed);
        }
    }
}
