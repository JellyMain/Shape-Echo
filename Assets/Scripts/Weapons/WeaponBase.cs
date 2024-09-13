using UnityEngine;
using Weapons.Bullets;


namespace Weapons
{
    public abstract class WeaponBase : MonoBehaviour
    {
        [SerializeField] protected Transform barrelPosition;
        [SerializeField] protected Bullet bulletPrefab;
        [SerializeField] protected float bulletSpeed;


        public abstract void Shoot(Vector2 direction);
        
    }
}