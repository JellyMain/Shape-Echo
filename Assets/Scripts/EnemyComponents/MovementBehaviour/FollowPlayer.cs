using EnemyComponents.MovementBehaviour;
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


public class Roam : MovementBehaviourBase
{
    private float nextPointTimer;
    private bool isMoving;


    public override void Move()
    {
        Vector2 randomPoint = Random.insideUnitCircle;

        Vector2 moveDirection = (randomPoint - (Vector2)enemy.position).normalized;
        rb2d.velocity = moveDirection * moveSpeed;
    }


}
