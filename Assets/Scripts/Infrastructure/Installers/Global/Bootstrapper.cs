using Factories;
using Infrastructure.GameStates;
using Infrastructure.Services;
using Input.Services;
using StaticData.Services;
using UI;
using UnityEngine;


namespace Infrastructure.Installers.Global
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private LoadingScreen loadingScreenPrefab;
        private GameStateMachine gameStateMachine;


        private void Awake()
        {
            RegisterServices();
            InitServices();
        }


        private void RegisterServices()
        {
            RegisterInputService();
            RegisterStaticDataService();

            LoadingScreen loadingScreen = CreateLoadingScreen();
            SceneLoader sceneLoader = RegisterSceneLoader(loadingScreen);
            UIFactory uiFactory = RegisterUIFactory();
            LevelFactory levelFactory = RegisterLevelFactory();
            gameStateMachine = RegisterGameStateMachine();

            RegisterGameMachineStates(gameStateMachine, sceneLoader, uiFactory, levelFactory);
        }

        
        private void Start()
        {
            gameStateMachine.Enter<BootstrapState>();
        }


        private void InitServices()
        {
            ServiceLocator.Container.Single<GameStateMachine>().Init();
            ServiceLocator.Container.Single<StaticDataService>().Init();
            ServiceLocator.Container.Single<LevelFactory>().InitPrefabs();
        }


        private static void RegisterGameMachineStates(GameStateMachine gameStateMachine, SceneLoader sceneLoader,
            UIFactory uiFactory, LevelFactory levelFactory)
        {
            ServiceLocator.Container.RegisterGlobalSingle(new BootstrapState(gameStateMachine));
            ServiceLocator.Container.RegisterGlobalSingle(new LoadProgressState(gameStateMachine));
            ServiceLocator.Container.RegisterGlobalSingle(new LoadMetaState(gameStateMachine, sceneLoader));
            ServiceLocator.Container.RegisterGlobalSingle(new LoadLevelState(gameStateMachine, sceneLoader, uiFactory, levelFactory));
            ServiceLocator.Container.RegisterGlobalSingle(new GameLoopState(gameStateMachine));
        }


        private static LevelFactory RegisterLevelFactory()
        {
            StaticDataService staticDataService = ServiceLocator.Container.Single<StaticDataService>();
            return ServiceLocator.Container.RegisterGlobalSingle(new LevelFactory(staticDataService));
        }


        private static void RegisterStaticDataService()
        {
            ServiceLocator.Container.RegisterGlobalSingle(new StaticDataService());
        }


        private SceneLoader RegisterSceneLoader(LoadingScreen loadingScreen)
        {
            SceneLoader sceneLoader =
                ServiceLocator.Container.RegisterGlobalSingle(new SceneLoader(loadingScreen));
            return sceneLoader;
        }


        private static UIFactory RegisterUIFactory()
        {
            UIFactory uiFactory = ServiceLocator.Container.RegisterGlobalSingle(new UIFactory());
            return uiFactory;
        }


        private static GameStateMachine RegisterGameStateMachine()
        {
            GameStateMachine gameStateMachine = ServiceLocator.Container.RegisterGlobalSingle(new GameStateMachine());
            return gameStateMachine;
        }


        private static void RegisterInputService()
        {
            ServiceLocator.Container.RegisterGlobalSingle(new InputService());
        }


        private LoadingScreen CreateLoadingScreen()
        {
            LoadingScreen loadingScreen = Instantiate(loadingScreenPrefab);
            return loadingScreen;
        }
    }
}