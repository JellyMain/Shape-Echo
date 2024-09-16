using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Weapons;


namespace PlayerComponents
{
    public class PlayerPicker : MonoBehaviour
    {
        [SerializeField, Required] private PlayerShooting playerShooting; 
        
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out WeaponBase weapon))
            {
                playerShooting.SetNewWeapon(weapon);            
            }
        }

        
    }
}
