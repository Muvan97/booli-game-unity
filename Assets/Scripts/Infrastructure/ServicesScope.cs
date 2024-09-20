using Configs;
using Infrastructure.EventBusSystem;
using Infrastructure.Factory;
using Infrastructure.Services;
using Infrastructure.Services.Input;
using Infrastructure.Services.Randomizer;
using Infrastructure.Services.SaveLoad;
using Infrastructure.Services.StaticData;
using Infrastructure.States;
using RestApiSystem;
using Sentry;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure
{
    public class ServicesScope : LifetimeScope
    {
        private CustomCoroutineRunner _coroutineRunnerInstance;
        private IContainerBuilder _builder;

        private IStaticDataProviderService _staticDataProviderService;

        protected override void Configure(IContainerBuilder containerBuilder)
        {
            SentrySdk.CaptureMessage("Test event");
            
            DontDestroyOnLoad(gameObject);
            _builder = containerBuilder;

            RegisterBuilder();
            RegisterFactoriesContainerProviderService();
            RegisterStaticDataProviderService();
            RegisterRestApiMediatorService();
            RegisterDataProviderAndSaverService();
            RegisterCurrenciesProviderService();
            RegisterGameFactory();
            RegisterLoadDataService();
            RegisterTimeScaleChangingService();
            RegisterInputService();
            RegisterRandomService();
            RegisterUIFactory();

            var eventBus = RegisterEventBus();
            RegisterAnalyticsSubscriberService(eventBus);

            RegisterCoroutineRunner();

            RegisterGameStateMachine();
        }

        private void RegisterCurrenciesProviderService() =>
            _builder.Register<CurrenciesProviderService>(Lifetime.Singleton);

        private void RegisterTimeScaleChangingService() =>
            _builder.Register<TimeScaleChangingService>(Lifetime.Singleton);

        private void RegisterGameFactory()
        {
            _builder.Register<GameFactory>(Lifetime.Singleton).As<IGameFactory>();
        }

        private void RegisterAnalyticsSubscriberService(IEventBus eventBus)
        {
            var service = new AnalyticsSubscriberService(eventBus);
            _builder.RegisterInstance(service);
        }

        private void RegisterFactoriesContainerProviderService() => _builder.Register<FactoriesContainerProviderService>(Lifetime.Singleton);

        private void RegisterDataProviderAndSaverService() => _builder.Register<GameDataProviderAndSaverService>(Lifetime.Singleton);

        private void RegisterRestApiMediatorService() => _builder.Register<RestApiMediatorService>(Lifetime.Singleton);

        private IEventBus RegisterEventBus()
        {
            var eventBus = new EventBus();
            _builder.RegisterInstance(eventBus).As<IEventBus>();

            return eventBus;
        }

        private void RegisterUIFactory() => _builder.Register<UIFactory>(Lifetime.Singleton).As<IUIFactory>();

        private void RegisterInputService() =>
            _builder.Register<GeneralInputService>(Lifetime.Singleton)
                .As<IInputService>();

        private void RegisterLoadDataService()
        {
            var serviceType = _staticDataProviderService.GameConfig.LoadSDKType switch
            {
                SDK.Yandex => typeof(LoadingTelegramDataService),
                SDK.Local => typeof(LoadingLocalDataService),
                SDK.Telegram => typeof(LoadingTelegramDataService),
                _ => null
            };

            _builder.Register(serviceType, Lifetime.Singleton)
                .As<LoadingDataService>();
        }

        private void RegisterCoroutineRunner()
        {
            CreateCoroutineRunner();

            DontDestroyOnLoad(_coroutineRunnerInstance);

            _builder.RegisterComponent<ICoroutineRunner>(_coroutineRunnerInstance);
            _builder.RegisterComponent<MonoBehaviour>(_coroutineRunnerInstance);
        }

        private void CreateCoroutineRunner()
        {
            _coroutineRunnerInstance = new GameObject("CoroutineRunner", new[] {typeof(CustomCoroutineRunner)})
                .GetComponent<CustomCoroutineRunner>();
        }

        private void RegisterBuilder() => _builder.RegisterInstance(_builder);

        private void RegisterRandomService()
        {
            _builder.Register<RandomService>(Lifetime.Singleton)
                .As<IRandomService>();
        }

        private void RegisterStaticDataProviderService()
        {
            _staticDataProviderService = new StaticDataProviderService();
            _staticDataProviderService.Load();

            _builder.RegisterInstance(_staticDataProviderService);
        }

        private void RegisterGameStateMachine() => _builder.RegisterEntryPoint<GameStateMachine>();
    }
}