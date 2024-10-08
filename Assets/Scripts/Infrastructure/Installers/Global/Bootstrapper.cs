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


        private void Start()
        {
            gameStateMachine.Enter<BootstrapState>();
        }


        private void RegisterServices()
        {
            RegisterInputService();

            StaticDataService staticDataService = RegisterStaticDataService();
            LevelFactory levelFactory = RegisterLevelFactory(staticDataService);
            LoadingScreen loadingScreen = CreateLoadingScreen();
            SceneLoader sceneLoader = RegisterSceneLoader(loadingScreen);
            UIFactory uiFactory = RegisterUIFactory();
            EnemiesFactory enemiesFactory = RegisterEnemiesFactory(staticDataService);

            gameStateMachine = RegisterGameStateMachine();

            RegisterGameMachineStates(gameStateMachine, sceneLoader, uiFactory, levelFactory, enemiesFactory);
        }


        private EnemiesFactory RegisterEnemiesFactory(StaticDataService staticDataService)
        {
            return ServiceLocator.Container.RegisterGlobalSingle(new EnemiesFactory(staticDataService));
        }


        private static LevelFactory RegisterLevelFactory(StaticDataService staticDataService)
        {
            return ServiceLocator.Container.RegisterGlobalSingle(new LevelFactory(staticDataService));
        }


        private static void InitServices()
        {
            ServiceLocator.Container.Single<GameStateMachine>().Init();
            ServiceLocator.Container.Single<StaticDataService>().Init();
        }


        private static void RegisterGameMachineStates(GameStateMachine gameStateMachine, SceneLoader sceneLoader,
            UIFactory uiFactory, LevelFactory levelFactory, EnemiesFactory enemiesFactory)
        {
            ServiceLocator.Container.RegisterGlobalSingle(new BootstrapState(gameStateMachine));
            ServiceLocator.Container.RegisterGlobalSingle(new LoadProgressState(gameStateMachine));
            ServiceLocator.Container.RegisterGlobalSingle(new LoadMetaState(gameStateMachine, sceneLoader));
            ServiceLocator.Container.RegisterGlobalSingle(new LoadLevelState(gameStateMachine, sceneLoader, uiFactory,
                levelFactory, enemiesFactory));
            ServiceLocator.Container.RegisterGlobalSingle(new GameLoopState(gameStateMachine));
        }



        private static StaticDataService RegisterStaticDataService()
        {
            return ServiceLocator.Container.RegisterGlobalSingle(new StaticDataService());
        }


        private static SceneLoader RegisterSceneLoader(LoadingScreen loadingScreen)
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
