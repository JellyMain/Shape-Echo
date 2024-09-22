using Constants;
using PlayerComponents;
using StaticData.Data;
using StaticData.Services;
using UnityEngine;


namespace Factories
{
    public class LevelFactory
    {
        private readonly StaticDataService staticDataService;
        
        
        public LevelFactory(StaticDataService staticDataService)
        {
            this.staticDataService = staticDataService;
        }


        public PlayerBase CreatePlayer(Vector2 position)
        {
            GameObject prefab = Resources.Load<GameObject>(RuntimeConstants.PrefabPaths.PLAYER);
            PlayerBase player = Object.Instantiate(prefab, position, Quaternion.identity).GetComponent<PlayerBase>();

            PlayerStaticData playerStaticData = staticDataService.PlayerStaticData;

            player.playerHealth.Max = playerStaticData.maxHealth;
            player.playerHealth.Current = playerStaticData.maxHealth;
            
            player.playerMovement.DashDuration = playerStaticData.dashDuration;
            player.playerMovement.MoveSpeed = playerStaticData.moveSpeed;
            player.playerMovement.DashSpeed = playerStaticData.dashSpeed;
            player.playerMovement.RotationSpeed = playerStaticData.rotationSpeed;

            player.playerShooting.WeaponRotationSpeed = playerStaticData.rotationSpeed;
            player.playerShooting.CurrentWeapon = playerStaticData.defaultWeaponBase;

            
            return player;
        }
    }
}