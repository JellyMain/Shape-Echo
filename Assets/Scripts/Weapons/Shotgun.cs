using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Weapons.Bullets;


namespace Weapons
{
    public class Shotgun : WeaponBase
    {
        [SerializeField] private int bulletsCount = 5;
        [SerializeField] private float shootAngle = 30f;


        public override void Shoot(Vector2 direction)
        {
            
            
            float angleStep = shootAngle / (bulletsCount - 1);
            float startAngle = -shootAngle / 2;

            for (int i = 0; i < bulletsCount; i++)
            {
                if(!HasAmmo()) return;
                
                float currentAngle = startAngle + i * angleStep;

                float angleInRadians = currentAngle * Mathf.Deg2Rad;
                float cos = Mathf.Cos(angleInRadians);
                float sin = Mathf.Sin(angleInRadians);

                Vector2 bulletDirection = new Vector2(
                    direction.x * cos - direction.y * sin,
                    direction.x * sin + direction.y * cos
                );

                Bullet bullet = Instantiate(bulletPrefab, barrelPosition.position, transform.rotation);
                bullet.Rb2d.velocity = bulletDirection * bulletSpeed;


                CurrentAmmo--;
            }
        }
    }
}
