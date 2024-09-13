using UnityEngine;
using Weapons.Bullets;


namespace Weapons
{
    public class Shotgun : WeaponBase
    {
        [SerializeField] private int bulletsCount = 5;
        [SerializeField] private float spreadAngle = 30f;


        public override void Shoot(Vector2 direction)
        {
            float angleStep = spreadAngle / (bulletsCount - 1);
            float startAngle = -spreadAngle / 2;
            
            for (int i = 0; i < bulletsCount; i++)
            {
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
            }
        }
    }
}
