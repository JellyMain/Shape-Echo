using System;
using UnityEngine;


namespace PlayerComponents
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private PlayerShooting playerShooting;
        [SerializeField] private Transform barrelPosition;
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private float bulletSpeed;
        
        
        public void Shoot()
        {
            Vector2 direction = (playerShooting.MousePosition - (Vector2)transform.position).normalized; 
            
            Bullet bullet = Instantiate(bulletPrefab, barrelPosition.position, transform.rotation);
            bullet.Rb2d.velocity = direction * bulletSpeed;
        }
    }
}