using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Infrastructure
{
    public class ServiceLocator
    {
        private static ServiceLocator instance;
        public static ServiceLocator Container => instance ??= new ServiceLocator();

        private readonly Dictionary<Type, object> globalServices = new Dictionary<Type, object>();
        private readonly Dictionary<Type, object> sceneServices = new Dictionary<Type, object>();


        public TService RegisterGlobalSingle<TService>(TService implementation) where TService : class
        {
             globalServices[typeof(TService)] = implementation;
             return globalServices[typeof(TService)] as TService;
        }


        public TService RegisterSceneSingle<TService>(TService implementation) where TService : class
        {
            sceneServices[typeof(TService)] = implementation;
            return sceneServices[typeof(TService)] as TService;
        }


        public TService RegisterSceneFromNewPrefab<TService>(TService prefab) where TService : Object
        {
            TService implementation = GameObject.Instantiate(prefab);
            sceneServices[typeof(TService)] = implementation;

            return implementation;
        }


        public TService Single<TService>() where TService : class
        {
            if (globalServices.TryGetValue(typeof(TService), out var service))
            {
                return service as TService;
            }

            if (sceneServices.TryGetValue(typeof(TService), out var ser))
            {
                return ser as TService;
            }

            throw new Exception($"Service {typeof(TService)} was not found");
        }


        public void CleanSceneServices()
        {
            sceneServices.Clear();
        }
    }
}
