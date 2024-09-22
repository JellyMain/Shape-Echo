using PlayerComponents;
using UnityEngine;


namespace EnemyComponents.MovementBehaviour
{
    [CreateAssetMenu(menuName = "MovementBehaviours/FollowPlayer", fileName = "FollowPlayer")]
    public class FollowPlayer : MovementBehaviourBase
    {
        public override void Move()
        {
            Vector2 moveDirection = (player.transform.position - enemy.transform.position).normalized;
            rb2d.velocity = moveDirection * moveSpeed;
        }
    }
}
