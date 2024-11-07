using Constants;
using Dungeon;
using Enemies;
using EnemyComponents;
using Factories;
using Infrastructure.GameStates.Interfaces;
using Infrastructure.Services;
using Pathfinding;
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
        private readonly PlayerFactory playerFactory;
        private readonly EnemiesFactory enemiesFactory;
        private readonly DungeonGenerator dungeonGenerator;


        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader,
            UIFactory uiFactory, PlayerFactory playerFactory, EnemiesFactory enemiesFactory,
            DungeonGenerator dungeonGenerator)
        {
            this.gameStateMachine = gameStateMachine;
            this.sceneLoader = sceneLoader;
            this.uiFactory = uiFactory;
            this.playerFactory = playerFactory;
            this.enemiesFactory = enemiesFactory;
            this.dungeonGenerator = dungeonGenerator;
        }


        public void Enter()
        {
            sceneLoader.Load(RuntimeConstants.Scenes.GAME_SCENE, CreateLevel);
        }


        private void CreateLevel()
        {
            dungeonGenerator.GenerateDungeon();
            PlayerBase player = playerFactory.CreatePlayer(Vector2.zero);
            uiFactory.CreateHud(player);
            SpawnEnemies();
        }
        

        private void SpawnEnemies()
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag(RuntimeConstants.Tags.ENEMY_SPAWNER))
            {
                EnemySpawner spawner = obj.GetComponent<EnemySpawner>();
                spawner.Spawn();
            }
        }
    }
}
