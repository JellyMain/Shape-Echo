using System;
using Infrastructure;
using Infrastructure.GameStates;
using Infrastructure.Services;
using UnityEngine;


namespace UI
{
    public class MainMenuUI : MonoBehaviour
    {
        private GameStateMachine gameStateMachine;


        private void Awake()
        {
            gameStateMachine = ServiceLocator.Container.Single<GameStateMachine>();
        }
        
        
        public void StartGame()
        {
            gameStateMachine.Enter<LoadLevelState>();
        }
    }
}
