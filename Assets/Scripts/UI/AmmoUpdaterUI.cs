using System;
using PlayerComponents;
using Sirenix.OdinInspector;
using UnityEngine;
using TMPro;


namespace UI
{
    public class AmmoUpdaterUI : MonoBehaviour
    {
        [SerializeField, Required] private TMP_Text currentAmmoText;
        [SerializeField, Required] private TMP_Text maxAmmoText;
        private PlayerShooting playerShooting;


        public void Init(PlayerShooting playerShooting)
        {
            this.playerShooting = playerShooting;

            this.playerShooting.Reloaded += UpdateCurrentAmmo;
            this.playerShooting.Shot += UpdateCurrentAmmo;
            this.playerShooting.WeaponSet += OnWeaponSet;
        }
        


        private void OnDisable()
        {
            playerShooting.Reloaded -= UpdateCurrentAmmo;
            playerShooting.Shot -= UpdateCurrentAmmo;
            playerShooting.WeaponSet -= OnWeaponSet;
        }


        private void UpdateCurrentAmmo(int currentAmmo)
        {
            currentAmmoText.text = currentAmmo.ToString();
        }


        private void OnWeaponSet(int maxAmmo)
        {
            maxAmmoText.text = maxAmmo.ToString();
            currentAmmoText.text = maxAmmo.ToString();
        }
    }
}
