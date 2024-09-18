using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Weapons;


namespace PlayerComponents
{
    public class PlayerPicker : MonoBehaviour
    {
        private PlayerBase playerBase;


        private void Awake()
        {
            playerBase = GetComponent<PlayerBase>();
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out WeaponBase weapon))
            {
                playerBase.playerShooting.SetNewWeapon(weapon);
            }
        }
    }
}
