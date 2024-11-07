using Dungeon;
using Factories;
using Infrastructure.GameStates;
using Infrastructure.Services;
using Input;
using Input.Interfaces;
using Input.Services;
using StaticData.Services;
using UI;
using UnityEngine;
using Zenject;


namespace Infrastructure.Installers.Global
{
    public class InfrastructureInstaller : MonoInstaller
    {
        [SerializeField] private LoadingScreen loadingScreen;


        public override void InstallBindings()
        {
            BindStaticDataService();
            BindPlayerFactory();
            CreateAndBindLoadingScreen();
            BindSceneLoader();
            BindUIFactory();
            BindEnemyFactory();
            BindDungeonGenerator();
            BindInputService();
            BindGameStatesFactory();
            BindLoadStates();
            BindGameStateMachine();
        }


        private void BindGameStatesFactory()
        {
            Container.Bind<GameStatesFactory>().AsSingle();
        }


        private void BindGameStateMachine()
        {
            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle().NonLazy();
        }


        private void BindLoadStates()
        {
            Container.Bind<BootstrapState>().AsSingle().NonLazy();
            Container.Bind<LoadProgressState>().AsSingle().NonLazy();
            Container.Bind<LoadMetaState>().AsSingle().NonLazy();
            Container.Bind<LoadLevelState>().AsSingle().NonLazy();
            Container.Bind<GameLoopState>().AsSingle().NonLazy();
        }


        private void BindInputService()
        {
            if (Application.isEditor)
            {
                Container.Bind<IInput>().FromInstance(new PcInput());
            }
            else if (Application.isMobilePlatform)
            {
                Container.Bind<IInput>().FromInstance(new MobileInput());
            }
        }


        private void BindDungeonGenerator()
        {
            Container.Bind<DungeonGenerator>().AsSingle();
        }


        private void BindEnemyFactory()
        {
            Container.Bind<EnemiesFactory>().AsSingle();
        }


        private void BindUIFactory()
        {
            Container.Bind<UIFactory>().AsSingle();
        }


        private void BindSceneLoader()
        {
            Container.Bind<SceneLoader>().AsSingle();
        }


        private void CreateAndBindLoadingScreen()
        {
            Container.Bind<LoadingScreen>().FromComponentInNewPrefab(loadingScreen).AsSingle().NonLazy();
        }


        private void BindPlayerFactory()
        {
            Container.Bind<PlayerFactory>().AsSingle();
        }


        private void BindStaticDataService()
        {
            Container.BindInterfacesAndSelfTo<StaticDataService>().AsSingle();
        }
    }
}
