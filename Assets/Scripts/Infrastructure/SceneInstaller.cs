using UnityEngine;


namespace Infrastructure
{
    public abstract class SceneInstaller : MonoBehaviour
    {
        public abstract void RegisterServices();


        public virtual void InitServices()
        {
        
        }

        public virtual void OnDestroy()
        {
            ServiceLocator.Container.CleanSceneServices();
        }
    }
}