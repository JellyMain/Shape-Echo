using System;
using System.Collections.Generic;
using Factories;
using Infrastructure.GameStates;
using Infrastructure.GameStates.Interfaces;


namespace Infrastructure.Services
{
    public class GameStateMachine 
    {
        private Dictionary<Type, IGameState> states;
        
        
        
        public void Init()
        {
            states = new Dictionary<Type, IGameState>()
            {
                [typeof(BootstrapState)] = ServiceLocator.Container.Single<BootstrapState>(),
                [typeof(LoadProgressState)] = ServiceLocator.Container.Single<LoadProgressState>(),
                [typeof(LoadMetaState)] = ServiceLocator.Container.Single<LoadMetaState>(),
                [typeof(LoadLevelState)] = ServiceLocator.Container.Single<LoadLevelState>(),
                [typeof(GameLoopState)] = ServiceLocator.Container.Single<GameLoopState>()
            };
        }
        
        
        public void Enter<TState>() where TState : class, IGameState
        {
            TState newState = GetState<TState>();
            newState.Enter();
        }
        
        
        private TState GetState<TState>() where TState : class, IGameState
        {
            return states[typeof(TState)] as TState;
        }
        
    }
}