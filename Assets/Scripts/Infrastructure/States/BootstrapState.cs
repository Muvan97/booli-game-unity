using Cysharp.Threading.Tasks;
using Holders;
using Infrastructure.EventBusSystem;
using Infrastructure.Factory;
using Infrastructure.Services;
using UnigramPayment.Runtime.Core;
using UnitonConnect.Core;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly IEventBus _eventBus;
        private readonly GameStateMachine _gameStateMachine;
        private readonly IUIFactory _uiFactory;
        private readonly IGameFactory _gameFactory;
        private readonly TimeScaleChangingService _timeScaleChangingService;

        public BootstrapState(GameStateMachine gameStateMachine, IEventBus eventBus, IGameFactory gameFactory,
            IUIFactory uiFactory, TimeScaleChangingService timeScaleChangingService)
        {
            _gameStateMachine = gameStateMachine;
            _eventBus = eventBus;
            _uiFactory = uiFactory;
            _gameFactory = gameFactory;
            _timeScaleChangingService = timeScaleChangingService;
        }

        public async UniTask Enter()
        {
            await LocalizationSettings.InitializationOperation;
            
            //Object.FindObjectOfType<UnigramPaymentSDK>().Initialize();
            Object.FindObjectOfType<UnitonConnectSDK>().Initialize();

            Subscribe();
            
            _gameFactory.CreateButtonsClickSoundSource();
            _gameStateMachine.Enter<LoadingDataState>();
        }

        public UniTask Exit()
        {
            return UniTask.CompletedTask;
        }

        private void Subscribe()
        {
        }
    }
}