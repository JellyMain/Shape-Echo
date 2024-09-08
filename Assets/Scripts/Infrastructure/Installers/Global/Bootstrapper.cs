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

            JoinPointsSpawner joinPointsSpawner = RegisterJoinPointsSpawner();
            StaticDataService staticDataService = RegisterStaticDataService();
            ShapeSpawner shapeSpawner = RegisterShapeSpawner(joinPointsSpawner, staticDataService);
            LevelValidator levelValidator = RegisterLevelValidator(staticDataService);
            LoadingScreen loadingScreen = CreateLoadingScreen();
            SceneLoader sceneLoader = RegisterSceneLoader(loadingScreen);
            UIFactory uiFactory = RegisterUIFactory();
            LevelFactory levelFactory = RegisterLevelFactory(staticDataService, shapeSpawner, joinPointsSpawner);

            gameStateMachine = RegisterGameStateMachine();

            RegisterGameMachineStates(gameStateMachine, sceneLoader, uiFactory, levelFactory);
        }


        private ShapeSpawner RegisterShapeSpawner(JoinPointsSpawner joinPointsSpawner,
            StaticDataService staticDataService)
        {
            return ServiceLocator.Container.RegisterGlobalSingle(new ShapeSpawner(joinPointsSpawner,
                staticDataService));
        }





        private static void InitServices()
        {
            ServiceLocator.Container.Single<GameStateMachine>().Init();
            ServiceLocator.Container.Single<StaticDataService>().Init();
            ServiceLocator.Container.Single<LevelValidator>().Init();
            ServiceLocator.Container.Single<JoinPointsSpawner>().Init();
        }


        private static void RegisterGameMachineStates(GameStateMachine gameStateMachine, SceneLoader sceneLoader,
            UIFactory uiFactory, LevelFactory levelFactory)
        {
            ServiceLocator.Container.RegisterGlobalSingle(new BootstrapState(gameStateMachine));
            ServiceLocator.Container.RegisterGlobalSingle(new LoadProgressState(gameStateMachine));
            ServiceLocator.Container.RegisterGlobalSingle(new LoadMetaState(gameStateMachine, sceneLoader));
            ServiceLocator.Container.RegisterGlobalSingle(new LoadLevelState(gameStateMachine, sceneLoader, uiFactory,
                levelFactory));
            ServiceLocator.Container.RegisterGlobalSingle(new GameLoopState(gameStateMachine));
        }


        private static LevelValidator RegisterLevelValidator(StaticDataService staticDataService)
        {
            return ServiceLocator.Container.RegisterGlobalSingle(new LevelValidator(staticDataService));
        }


        private static LevelFactory RegisterLevelFactory(StaticDataService staticDataService, ShapeSpawner shapeSpawner,
            JoinPointsSpawner joinPointsSpawner)
        {
            return ServiceLocator.Container.RegisterGlobalSingle(new LevelFactory(staticDataService, shapeSpawner,
                joinPointsSpawner));
        }


        private JoinPointsSpawner RegisterJoinPointsSpawner()
        {
            return ServiceLocator.Container.RegisterGlobalSingle(new JoinPointsSpawner());
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
