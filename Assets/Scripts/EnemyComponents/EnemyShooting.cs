using System;
using System.Collections;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using EnemyComponents.ShootingPatterns;
using UnityEngine;
using Weapons.Bullets;


namespace EnemyComponents
{
    public class EnemyShooting : MonoBehaviour
    {
        [SerializeField] private Transform weaponHand;
        [SerializeField] private LayerMask playerLayerMask;
        public Bullet BulletPrefab { get; set; }
        public ShootingPatternBase ShootingPattern { get; set; }
        public float PlayerShootRadius { get; set; }
        public float ShotCooldown { get; set; }
        private Transform player;
        private bool isShooting;
        private float shotTimer;



        private void Start()
        {
            ShootingPattern.Init(BulletPrefab, weaponHand);
        }


        private void Update()
        {
            FindPlayer();
        }


        private void FindPlayer()
        {
            Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, PlayerShootRadius, playerLayerMask);

            if (playerCollider != null)
            {
                player = playerCollider.transform;

                if (!isShooting)
                {
                    StartShooting();
                }
            }
            else if (isShooting)
            {
                StopShooting();
                player = null;
            }
        }
        

        private void StartShooting()
        {
            Shoot().Forget();
        }


        private async UniTaskVoid Shoot()
        {
            isShooting = true;
            shotTimer = 0;

            while (isShooting)
            {
                if (player != null && shotTimer >= ShotCooldown)
                {
                    Vector2 direction = (player.position - transform.position).normalized;
                    ShootingPattern.Shoot(direction);
                    shotTimer = 0;
                }

                shotTimer += Time.deltaTime;

                await UniTask.Yield();
            }
        }


        private void StopShooting()
        {
            isShooting = false;
        }


        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, PlayerShootRadius);
        }
    }
}
