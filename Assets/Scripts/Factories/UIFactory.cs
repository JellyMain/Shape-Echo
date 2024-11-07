using Constants;
using PlayerComponents;
using UI;
using UnityEngine;
using Zenject;


namespace Factories
{
    public class UIFactory
    {
        private readonly DiContainer diContainer;


        public UIFactory(DiContainer diContainer)
        {
            this.diContainer = diContainer;
        }


        public void CreateHud(PlayerBase player)
        {
            
            GameObject prefab = Resources.Load<GameObject>(RuntimeConstants.PrefabPaths.HUD);

            GameObject hudParent = new GameObject("Hud");
            
            GameObject hud = diContainer.InstantiatePrefab(prefab, hudParent.transform);
            
            AmmoUpdaterUI ammoUpdater = hud.GetComponentInChildren<AmmoUpdaterUI>();
            ammoUpdater.Init(player.playerShooting);
        }
    }
}