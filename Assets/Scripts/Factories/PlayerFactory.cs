using Constants;
using PlayerComponents;
using StaticData.Data;
using StaticData.Services;
using UnityEngine;
using Zenject;


namespace Factories
{
    public class PlayerFactory
    {
        private readonly StaticDataService staticDataService;
        private readonly DiContainer diContainer;


        public PlayerFactory(StaticDataService staticDataService, DiContainer diContainer)
        {
            this.staticDataService = staticDataService;
            this.diContainer = diContainer;
        }


        public PlayerBase CreatePlayer(Vector2 position)
        {
            GameObject playerParent = new GameObject("Player");
            
            GameObject prefab = Resources.Load<GameObject>(RuntimeConstants.PrefabPaths.PLAYER);
            PlayerBase player = diContainer.InstantiatePrefab(prefab, position, Quaternion.identity, playerParent.transform).GetComponent<PlayerBase>();

            PlayerStaticData playerStaticData = staticDataService.PlayerStaticData;

            player.playerHealth.Max = playerStaticData.maxHealth;
            player.playerHealth.Current = playerStaticData.maxHealth;
            
            player.playerMovement.DashDuration = playerStaticData.dashDuration;
            player.playerMovement.MoveSpeed = playerStaticData.moveSpeed;
            player.playerMovement.DashSpeed = playerStaticData.dashSpeed;
            player.playerMovement.DashCooldown = playerStaticData.dashCooldown;

            player.playerShooting.WeaponRotationSpeed = playerStaticData.weaponRotationSpeed;
            player.playerShooting.CurrentWeapon = playerStaticData.defaultWeaponBase;

            
            return player;
        }
    }
}