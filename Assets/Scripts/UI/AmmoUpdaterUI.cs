using System;
using System.Collections.Generic;
using Infrastructure;
using PlayerComponents;
using Sirenix.OdinInspector;
using StaticData.Services;
using UnityEngine;
using UnityEngine.UI;
using Weapons.Bullets;


namespace UI
{
    public class AmmoUpdaterUI : MonoBehaviour
    {
        [SerializeField, Required] private Transform slotsContainer;
        [SerializeField, Required] private GridLayoutGroup layoutGroup;
        private readonly List<BulletUI> createdBullets = new List<BulletUI>();
        private PlayerShooting playerShooting;
        private StaticDataService staticDataService;
        


        public void Init(PlayerShooting playerShooting)
        {
            this.playerShooting = playerShooting;

            this.playerShooting.Reloaded += ReloadBullets;
            this.playerShooting.Shot += DisableBullet;
            this.playerShooting.WeaponSet += OnWeaponSet;
        }


        private void Awake()
        {
            staticDataService = ServiceLocator.Container.Single<StaticDataService>();
        }


        private void OnDisable()
        {
            playerShooting.Reloaded -= ReloadBullets;
            playerShooting.Shot -= DisableBullet;
            playerShooting.WeaponSet -= OnWeaponSet;
        }


        private async void ReloadBullets(int currentAmmo)
        {
            layoutGroup.enabled = false;

            for (int i = 0; i < currentAmmo; i++)
            {
                BulletUI bulletUI = createdBullets[i];

                if (!bulletUI.IsActive)
                {
                    await bulletUI.ActivateBullet();
                }
            }

            layoutGroup.enabled = true;
        }


        private void DisableBullet(int currentAmmo)
        {
            int shotBullets = createdBullets.Count - currentAmmo;

            for (int i = 0; i < shotBullets; i++)
            {
                BulletUI bulletUI = createdBullets[i];

                if (bulletUI.IsActive)
                {
                    bulletUI.DeactivateBullet();
                }
            }
        }


        private void OnWeaponSet(int maxAmmo, AmmoType ammoType)
        {
            BulletUI bulletUIPrefab = staticDataService.BulletUIPrefabForAmmoType(ammoType);


            Vector2 slotSize = bulletUIPrefab.GetComponent<RectTransform>().sizeDelta;
            layoutGroup.cellSize = slotSize;

            ClearBulletSlots();
            CreateBulletSlots(maxAmmo, bulletUIPrefab);
        }


        private void CreateBulletSlots(int ammo, BulletUI bulletUIPrefab)
        {
            for (int i = 0; i < ammo; i++)
            {
                BulletUI bulletUI = Instantiate(bulletUIPrefab, slotsContainer);
                createdBullets.Add(bulletUI);
            }
        }


        private void ClearBulletSlots()
        {
            foreach (BulletUI slot in createdBullets)
            {
                Destroy(slot.gameObject);
            }

            createdBullets.Clear();
        }
    }
}
