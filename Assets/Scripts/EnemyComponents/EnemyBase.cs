using UnityEngine;


namespace EnemyComponents
{
    public class EnemyBase : MonoBehaviour
    {
        [SerializeField] private EnemyType enemyType;
        public EnemyShooting enemyShooting;
        public EnemyHealth enemyHealth;
        public EnemyMovement enemyMovement;
        
        
    }
}