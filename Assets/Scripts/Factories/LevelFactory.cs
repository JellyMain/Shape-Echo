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


        public void CreatePlayer(Vector2 position)
        {
            GameObject prefab = Resources.Load<GameObject>(RuntimeConstants.PrefabPaths.PLAYER);
            PlayerBase player = Object.Instantiate(prefab, position, Quaternion.identity).GetComponent<PlayerBase>();

            PlayerStaticData playerStaticData = staticDataService.PlayerStaticData;
            

            player.playerMovement.DashDuration = playerStaticData.dashDuration;
            player.playerMovement.MoveSpeed = playerStaticData.moveSpeed;
            player.playerMovement.DashSpeed = playerStaticData.dashSpeed;
            player.playerMovement.RotationSpeed = playerStaticData.rotationSpeed;

            player.playerShooting.WeaponBasePrefab = playerStaticData.defaultWeaponBase;
            player.playerShooting.WeaponRotationSpeed = playerStaticData.rotationSpeed;
            
            player.playerShooting.CreateWeapon();
        }
    }
}
