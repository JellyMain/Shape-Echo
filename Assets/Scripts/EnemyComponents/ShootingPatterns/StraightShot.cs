using UnityEngine;
using Weapons.Bullets;


namespace EnemyComponents.ShootingPatterns
{
    [CreateAssetMenu(menuName = "ShootingPatterns/StraightShot", fileName = "StraightShot")]
    public class StraightShot : ShootingPatternBase
    {
        public override void Shoot(Vector2 direction)
        {
            Vector2 finalDirection = AddRandomSpread(direction);

            Bullet bullet = Instantiate(bulletPrefab, weaponHand.transform.position, Quaternion.identity);
            bullet.Rb2d.velocity = finalDirection * bulletSpeed;
        }
    }
}
