using EnemyComponents;
using PlayerComponents;
using StaticData.Data;
using StaticData.Services;
using UnityEngine;


namespace Factories
{
    public class EnemiesFactory
    {
        private readonly StaticDataService staticDataService;


        public EnemiesFactory(StaticDataService staticDataService)
        {
            this.staticDataService = staticDataService;
        }


        public EnemyBase CreateEnemy(EnemyType enemyType, Vector2 position, PlayerBase player)
        {
            EnemyStaticData enemyStaticData = staticDataService.EnemyStaticDataForEnemyType(enemyType);
            EnemyBase prefab = enemyStaticData.enemyPrefab;

            EnemyBase enemy = Object.Instantiate(prefab, position, Quaternion.identity);

            enemy.enemyMovement.MoveSpeed = enemyStaticData.moveSpeed;
            enemy.enemyMovement.Player = player.transform;
            
            enemy.enemyShooting.ShotCooldown = enemyStaticData.shotCooldown;
            enemy.enemyShooting.BulletPrefab = enemyStaticData.bulletPrefab;
            enemy.enemyShooting.ShootingPattern = enemyStaticData.shootingPattern;
            enemy.enemyShooting.PlayerShootRadius = enemyStaticData.playerShootRadius;
            
            enemy.enemyHealth.Max = enemyStaticData.maxHealth;

            return enemy;
        }
    }
}
