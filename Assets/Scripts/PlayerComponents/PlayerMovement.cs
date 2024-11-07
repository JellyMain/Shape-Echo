using System;
using Cysharp.Threading.Tasks;
using Infrastructure;
using Input.Interfaces;
using Input.Services;
using Sirenix.OdinInspector;
using UnityEngine;
using Utils;
using Zenject;


namespace PlayerComponents
{
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody2D rb2d;
        private IInput inputService;
        public float DashCooldown { get; set; }
        public float DashSpeed { get; set; }
        public float DashDuration { get; set; }
        public float MoveSpeed { get; set; }
        public bool IsMoving { get; private set; }
        public Vector2 MoveDirection { get; private set; }
        private bool isDashing;
        private bool canDash = true;
        private float dashCooldownTimer;

        public event Action DashStarted;
        public event Action DashEnded;

        
        [Inject]
        private void Construct(IInput inputService)
        {
            this.inputService = inputService;
        }
        
        
        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
        }


        private void OnEnable()
        {
            inputService.DashPressed += OnDash;
        }


        private void OnDisable()
        {
            inputService.DashPressed -= OnDash;
        }


        private void FixedUpdate()
        {
            Move();
        }


        private void Move()
        {
            if (isDashing) return;

            MoveDirection = inputService.GetNormalizedMoveInput();
            IsMoving = MoveDirection != Vector2.zero;

            rb2d.velocity = MoveDirection * MoveSpeed;
        }


        private void OnDash()
        {
            Dash().Forget();
        }


        private async UniTaskVoid Dash()
        {
            if (canDash)
            {
                dashCooldownTimer = 0;
                canDash = false;
                
                DashStarted?.Invoke();
                isDashing = true;
                Vector2 moveDirection = inputService.GetNormalizedMoveInput();
                rb2d.velocity = moveDirection * DashSpeed;

                int delay = DashDuration.SecondsToMilliseconds();
                await UniTask.Delay(delay);

                isDashing = false;
                DashEnded?.Invoke();

                while (dashCooldownTimer < DashCooldown)
                {
                    dashCooldownTimer += Time.deltaTime;
                    await UniTask.Yield();
                }

                canDash = true;
            }
        }
    }
}
