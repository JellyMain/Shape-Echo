using UnityEngine;


namespace Infrastructure.Installers.Scene
{
    public class GameSceneInstaller : SceneInstaller
    {
        [SerializeField] private ShapeMover shapeMoverPrefab;
    
    
        public override void RegisterServices()
        {
            RegisterShapeMover();
        }


    

    
        private void RegisterShapeMover()
        {
            ServiceLocator.Container.RegisterSceneFromNewPrefab(shapeMoverPrefab);
        }
    }
}
