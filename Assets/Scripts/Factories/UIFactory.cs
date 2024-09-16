using Constants;
using PlayerComponents;
using UI;
using UnityEngine;


namespace Factories
{
    public class UIFactory
    {
        public void CreateHud(PlayerBase player)
        {
            GameObject prefab = Resources.Load<GameObject>(RuntimeConstants.PrefabPaths.HUD);
            
            GameObject hud = Object.Instantiate(prefab);
            
            AmmoUpdaterUI ammoUpdater = hud.GetComponentInChildren<AmmoUpdaterUI>();
            ammoUpdater.Init(player.playerShooting);
        }
    }
}