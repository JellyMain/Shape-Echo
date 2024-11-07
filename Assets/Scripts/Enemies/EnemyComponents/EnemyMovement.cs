using EnemyComponents.MovementBehaviour;
using UnityEngine;


namespace Enemies.EnemyComponents
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private MovementBehaviourBase movementBehaviour;
        private Rigidbody2D rb2d;
        public Transform Player { get; set; }
        public float MoveSpeed { get; set; }


        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
        }


        private void Start()
        {
            movementBehaviour.Init(rb2d, Player, transform, MoveSpeed);
        }
        
        
        private void FixedUpdate()
        {
            if (Player != null)
            {
                movementBehaviour.Move();
            }
        }
    }
}
