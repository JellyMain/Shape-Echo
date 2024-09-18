using EnemyComponents;
using UnityEngine;


namespace StaticData.Data
{
    [CreateAssetMenu(menuName = "StaticData/EnemyStaticData", fileName = "New Enemy")]
    public class EnemyStaticData : ScriptableObject
    {
        public EnemyType enemyType;
        public float maxHealth;
        public float moveSpeed;
        public float shotCooldown;
    }
}
