using System.Collections.Generic;
using UnityEngine;


namespace Infrastructure
{
    public class SceneContext : MonoBehaviour
    {
        [SerializeField] private List<SceneInstaller> sceneInstallers;
    
    
        private void Awake()
        {
            RegisterAllServices();
            InitAllServices();
        }


        private void InitAllServices()
        {
            foreach (SceneInstaller installer in sceneInstallers)
            {
                installer.InitServices();
            }
        }


        private void RegisterAllServices()
        {
            foreach(SceneInstaller installer in sceneInstallers)
            {
                installer.RegisterServices();
            }
        }
    }
}