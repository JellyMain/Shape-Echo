using UnityEngine;


namespace PlayerComponents
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private PlayerBase playerBase;
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int MoveUp = Animator.StringToHash("MoveUp");
        private static readonly int MoveUpRight = Animator.StringToHash("MoveUpRight");
        private static readonly int MoveUpLeft = Animator.StringToHash("MoveUpLeft");
        private static readonly int MoveLeft = Animator.StringToHash("MoveLeft");
        private static readonly int MoveRight = Animator.StringToHash("MoveRight");
        private static readonly int MoveDown = Animator.StringToHash("MoveDown");
        private static readonly int MoveDownRight = Animator.StringToHash("MoveDownRight");
        private static readonly int MoveDownLeft = Animator.StringToHash("MoveDownLeft");
        private int currentAnimation;
        private Animator animator;


        private void Awake()
        {
            animator = GetComponent<Animator>();
        }


        private void Update()
        {
            PlayMoveAnimations();
        }


        private void PlayMoveAnimations()
        {
            Vector2 moveDirection = playerBase.playerMovement.MoveDirection;
            
            if (moveDirection == Vector2.zero)
            {
                ChangeAnimation(Idle);
                return;
            }

            float threshold = 0.1f;
            
            if (moveDirection == Vector2.right)
            {
                ChangeAnimation(MoveRight);
                Debug.Log("playing");
            }
            else if (moveDirection == Vector2.left)
            {
                ChangeAnimation(MoveLeft);
            }
            else if (moveDirection == Vector2.up)
            {
                ChangeAnimation(MoveUp);
            }
            else if (moveDirection == Vector2.down)
            {
                ChangeAnimation(MoveDown);
            }
            if (moveDirection.x > threshold && moveDirection.y > threshold)
            {
                ChangeAnimation(MoveUpRight);
            }
            else if (moveDirection.x < -threshold && moveDirection.y > threshold)
            {
                ChangeAnimation(MoveUpLeft);
            }
            else if (moveDirection.x > threshold && moveDirection.y < -threshold)
            {
                ChangeAnimation(MoveDownRight);
            }
            else if (moveDirection.x < -threshold && moveDirection.y < -threshold)
            {
                ChangeAnimation(MoveDownLeft);
            }
           
        }


        private void ChangeAnimation(int animationHash)
        {
            if (currentAnimation != animationHash)
            {
                currentAnimation = animationHash;
                animator.Play(currentAnimation);
            }
        }
    }
}
