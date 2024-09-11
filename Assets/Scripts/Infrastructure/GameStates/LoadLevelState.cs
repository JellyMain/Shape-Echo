using Constants;
using Factories;
using Infrastructure.GameStates.Interfaces;
using Infrastructure.Services;
using UI;
using UnityEngine;


namespace Infrastructure.GameStates
{
    public class LoadLevelState : IGameState
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly SceneLoader sceneLoader;
        private readonly UIFactory uiFactory;
        private readonly LevelFactory levelFactory;


        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader,
            UIFactory uiFactory, LevelFactory levelFactory)
        {
            this.gameStateMachine = gameStateMachine;
            this.sceneLoader = sceneLoader;
            this.uiFactory = uiFactory;
            this.levelFactory = levelFactory;
        }


        public void Enter()
        {
            sceneLoader.Load(RuntimeConstants.Scenes.GAME_SCENE, CreateLevel);
        }


        private void CreateLevel()
        {
            levelFactory.CreatePlayer(Vector2.zero);
        }
    }
}
