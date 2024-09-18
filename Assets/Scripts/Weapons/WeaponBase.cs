using System;
using UnityEngine;
using Weapons.Bullets;
using Random = UnityEngine.Random;


namespace Weapons
{
    public abstract class WeaponBase : MonoBehaviour
    {
        [SerializeField] private WeaponBase prefab;
        [SerializeField] protected Transform barrelPosition;
        [SerializeField] protected Bullet bulletPrefab;
        [SerializeField] protected int maxAmmo;
        [SerializeField] protected float reloadDuration;
        [SerializeField] protected float bulletSpeed;
        [SerializeField] protected float shotCooldown;
        [SerializeField] protected float spreadAngle;
        public int CurrentAmmo { get; protected set; }
        public int MaxAmmo => maxAmmo;
        public float ShotCooldown => shotCooldown;
        public float ReloadDuration => reloadDuration;
        public WeaponBase Prefab => prefab;


        private void Start()
        {
            InitAmmo();
        }
        

        private void InitAmmo()
        {
            CurrentAmmo = maxAmmo;
        }


        public abstract void Shoot(Vector2 direction);


        protected Vector2 AddRandomSpread(Vector2 direction)
        {
            float radiansSpread = Random.Range(-spreadAngle, spreadAngle) * Mathf.Deg2Rad;

            float targetXPos = Mathf.Cos(radiansSpread);
            float targetYPos = Mathf.Sin(radiansSpread);

            Vector2 rotatedVector = new Vector2(direction.x * targetXPos - direction.y * targetYPos,
                direction.x * targetYPos + direction.y * targetXPos);

            return rotatedVector;
        }


        protected bool HasAmmo()
        {
            return CurrentAmmo > 0;
        }


        public void Reload()
        {
            CurrentAmmo = maxAmmo;
        }
        

    }
}
