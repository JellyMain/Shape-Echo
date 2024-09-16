using System.Collections;
using UnityEngine;
using Weapons.Bullets;


namespace Weapons
{
    public class MachineGun : WeaponBase
    {
        public override void Shoot(Vector2 direction)
        {
            if(!HasAmmo()) return;
            
            Bullet bullet = Instantiate(bulletPrefab, barrelPosition.position, transform.rotation);
            
            Vector2 directionWithSpread = AddRandomSpread(direction);
            bullet.Rb2d.velocity = directionWithSpread * bulletSpeed;
            
            CurrentAmmo--;
        }
    }
}
