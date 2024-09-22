using UnityEngine;
using Weapons.Bullets;


namespace EnemyComponents.ShootingPatterns
{
    public abstract class ShootingPatternBase : ScriptableObject
    {
        [SerializeField] protected float bulletSpeed;
        [SerializeField] protected float spreadAngle;
        protected Bullet bulletPrefab;
        protected Transform weaponHand;


        public void Init(Bullet bulletPrefab, Transform weaponHand)
        {
            this.bulletPrefab = bulletPrefab;
            this.weaponHand = weaponHand;
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
    }
}
