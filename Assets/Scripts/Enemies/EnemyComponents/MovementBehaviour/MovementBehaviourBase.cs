using PlayerComponents;
using UnityEngine;


namespace EnemyComponents.MovementBehaviour
{
    public abstract class MovementBehaviourBase : ScriptableObject
    {
        protected Rigidbody2D rb2d;
        protected Transform enemy;
        protected Transform player;
        protected float moveSpeed;


        public void Init(Rigidbody2D rb2d, Transform player, Transform enemy, float moveSpeed)
        {
            this.rb2d = rb2d;
            this.player = player;
            this.moveSpeed = moveSpeed;
            this.enemy = enemy;
        }


        public abstract void Move();
    }
}