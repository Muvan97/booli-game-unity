﻿using System;
using System.Collections.Generic;
 using Cysharp.Threading.Tasks;
 using Infrastructure.EventBusSystem;
 using Infrastructure.Factory;
 using Infrastructure.Services;
 using Infrastructure.Services.Input;
using Infrastructure.Services.SaveLoad;
using Infrastructure.Services.StaticData;
 using RestApiSystem;
 using UnityEngine;
 using VContainer;
using VContainer.Unity;

 namespace Infrastructure.States
{
    public class GameStateMachine : IStartable
    {
        public IExitableState PastState { get; private set; }
        public Action StateChanged;
        
        private IExitableState _activeState;

        private Dictionary<Type, IExitableState> _states;

        [Inject]
        public GameStateMachine(LoadingDataService loadingDataService,
            IStaticDataProviderService staticDataProviderService,
            IUIFactory uiFactory, IEventBus eventBus, IGameFactory gameFactory,
            TimeScaleChangingService timeScaleChangingService, CurrenciesProviderService currenciesProviderService,
            GameDataProviderAndSaverService gameDataProviderAndSaverService,
            FactoriesContainerProviderService factoriesContainerProviderService)
        {
            RegisterState<BootstrapState>(new BootstrapState(this, eventBus, gameFactory, uiFactory, timeScaleChangingService));
            
            RegisterState<LoadingDataState>(new LoadingDataState(this, loadingDataService));

            RegisterState<MainMenuState>(new MainMenuState(uiFactory, gameFactory, this, 
                gameDataProviderAndSaverService, staticDataProviderService, currenciesProviderService, 
                gameDataProviderAndSaverService, factoriesContainerProviderService));
            
            RegisterState<LevelState>(new LevelState(staticDataProviderService, gameFactory, uiFactory, currenciesProviderService, 
                gameDataProviderAndSaverService, this, timeScaleChangingService, eventBus));

            Enter<BootstrapState>();
        }

        private void RegisterState<TState>(TState implementation) where TState : class, IExitableState
        {
            _states ??= new Dictionary<Type, IExitableState>();
            _states[typeof(TState)] = implementation;
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter().Forget();
            StateChanged?.Invoke();
        }
        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            var state = ChangeState<TState>();
            state.Enter(payload).Forget();
            StateChanged?.Invoke();
        }
        
        public void Enter<TState, TPayload1, TPayload2>(TPayload1 payload1, TPayload2 payload2) where TState : class, IPayloadedState<TPayload1, TPayload2>
        {
            var state = ChangeState<TState>();
            state.Enter(payload1, payload2).Forget();
            StateChanged?.Invoke();
        }
        
        public void Enter<TState, TPayload1, TPayload2, TPayload3>(TPayload1 payload1, TPayload2 payload2, TPayload3 payload3) 
            where TState : class, IPayloadedState<TPayload1, TPayload2, TPayload3>
        {
            var state = ChangeState<TState>();
            state.Enter(payload1, payload2, payload3).Forget();
            StateChanged?.Invoke();
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            PastState = _activeState;
            _activeState?.Exit().Forget();

            var state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState
        {
            return _states[typeof(TState)] as TState;
        }

        public void Start()
        {
            
        }
    }
}