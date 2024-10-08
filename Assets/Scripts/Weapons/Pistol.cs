using System.Collections;
using UnityEngine;
using Weapons.Bullets;


namespace Weapons
{
    public class Pistol : WeaponBase
    {
        public override void Shoot(Vector2 direction)
        {
            if(!HasAmmo()) return;
            
            Bullet bullet = Instantiate(bulletPrefab, barrelPosition.position, transform.rotation);
            bullet.Rb2d.velocity = direction * bulletSpeed;

            CurrentAmmo--;
        }
    }
}