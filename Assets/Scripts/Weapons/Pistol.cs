using UnityEngine;
using Weapons.Bullets;


namespace Weapons
{
    public class Pistol : WeaponBase
    {
        public override void Shoot(Vector2 direction)
        {
            Bullet bullet = Instantiate(bulletPrefab, barrelPosition.position, transform.rotation);
            bullet.Rb2d.velocity = direction * bulletSpeed;
        }
    }
}