using Constants;
using EnemyComponents;
using Factories;
using Infrastructure.GameStates.Interfaces;
using Infrastructure.Services;
using PlayerComponents;
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
        private readonly EnemiesFactory enemiesFactory;


        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader,
            UIFactory uiFactory, LevelFactory levelFactory, EnemiesFactory enemiesFactory)
        {
            this.gameStateMachine = gameStateMachine;
            this.sceneLoader = sceneLoader;
            this.uiFactory = uiFactory;
            this.levelFactory = levelFactory;
            this.enemiesFactory = enemiesFactory;
        }


        public void Enter()
        {
            sceneLoader.Load(RuntimeConstants.Scenes.GAME_SCENE, CreateLevel);
        }


        private void CreateLevel()
        {
            PlayerBase player = levelFactory.CreatePlayer(Vector2.zero);
            enemiesFactory.CreateEnemy(EnemyType.Triangle, Vector2.one, player);
            uiFactory.CreateHud(player);
        }
    }
}
