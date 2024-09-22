using EnemyComponents;
using EnemyComponents.ShootingPatterns;
using UnityEngine;
using Weapons.Bullets;


namespace StaticData.Data
{
    [CreateAssetMenu(menuName = "StaticData/EnemyStaticData", fileName = "New Enemy")]
    public class EnemyStaticData : ScriptableObject
    {
        public EnemyBase enemyPrefab;
        public EnemyType enemyType;
        public float maxHealth;
        public float moveSpeed;
        public Bullet bulletPrefab;
        public ShootingPatternBase shootingPattern;
        public float playerShootRadius;
        public float shotCooldown;
    }
}
