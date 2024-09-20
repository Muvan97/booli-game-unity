using Systems.Level.LevelPopup;
using Systems.Level.LevelWaves;
using Systems.UI;
using Systems.UI.BuildingTowerPopup;
using Systems.UI.BuildingTowerPopup.BuildingButton;
using Systems.UI.DefeatPopup;
using Systems.UI.DesktopPopup;
using Systems.UI.FriendsPopup;
using Systems.UI.MainMenuWindow;
using Systems.UI.SettingsPopup;
using Systems.UI.ShopPopup;
using Systems.UI.UpgradeTowerPopup;
using Systems.UI.UpgradeTowerPopup.UpgradeTowerButton;
using Systems.UI.Win;
using Infrastructure.AssetManagement;
using Infrastructure.EventBusSystem;
using Infrastructure.Services;
using Infrastructure.Services.Input;
using Infrastructure.Services.StaticData;
using Infrastructure.States;
using RestApiSystem;
using TMPro;
using UnitonConnect.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IStaticDataProviderService _staticDataProviderService;
        private readonly IInputService _inputService;
        private readonly IEventBus _eventBus;
        private readonly IGameFactory _gameFactory;

        private readonly FactoriesContainerProviderService _factoriesContainerProviderService;
        private readonly TimeScaleChangingService _timeScaleChangingService;
        private readonly GameDataProviderAndSaverService _gameDataProviderAndSaverService;
        private readonly CurrenciesProviderService _currenciesProviderService;
        private readonly RestApiMediatorService _restApiMediatorService;


        public UIFactory(IStaticDataProviderService staticDataProviderService, IEventBus eventBus,
            IGameFactory gameFactory, IInputService inputService, TimeScaleChangingService timeScaleChangingService,
            GameDataProviderAndSaverService gameDataProviderAndSaverService,
            CurrenciesProviderService currenciesProviderService,
            FactoriesContainerProviderService factoriesContainerProviderService,
            RestApiMediatorService restApiMediatorService)
        {
            _staticDataProviderService = staticDataProviderService;
            _eventBus = eventBus;
            _gameFactory = gameFactory;
            _inputService = inputService;
            _timeScaleChangingService = timeScaleChangingService;
            _gameDataProviderAndSaverService = gameDataProviderAndSaverService;
            _currenciesProviderService = currenciesProviderService;
            _factoriesContainerProviderService = factoriesContainerProviderService;
            _restApiMediatorService = restApiMediatorService;
        }

        public BuildingButtonView GetBuildingButtonView(BuildingButtonConfig data, Transform parent)
        {
            var prefab = _factoriesContainerProviderService.GetOrLoadAndRegisterMonoBehaviour
                <BuildingButtonView>(AssetPath.BuildingButtonView, false);

            var instance = Object.Instantiate(prefab, parent);
            instance.Construct(data);

            return instance;
        }

        public BuildingTowerPopupView GetBuildingTowerPopup() =>
            _factoriesContainerProviderService.GetOrLoadAndRegisterMonoBehaviour
                <BuildingTowerPopupView>(AssetPath.BuildingTowerPopup);

        public TaskPopup GetTaskPopup() =>
            _factoriesContainerProviderService.GetOrLoadAndRegisterMonoBehaviour
                <TaskPopup>(AssetPath.TaskPopup);

        public ShopPopupView GetShopPopup()
        {
            var popup = _factoriesContainerProviderService.GetOrLoadAndRegisterMonoBehaviour
                <ShopPopupView>(AssetPath.ShopPopup, out var isLoaded);

            if (isLoaded)
                new ShopPopupController(_staticDataProviderService.GameConfig, popup,
                    _restApiMediatorService, _currenciesProviderService, _gameDataProviderAndSaverService);

            return popup;
        }

        public UpgradeTowerPopupView GetUpgradeTowerPopup()
        {
            var popup = _factoriesContainerProviderService.GetOrLoadAndRegisterMonoBehaviour
                <UpgradeTowerPopupView>(AssetPath.UpgradeTowerPopup, out var isLoaded);

            if (isLoaded)
            {
                var model = new UpgradeTowerPopupModel();
                _factoriesContainerProviderService.Container.RegisterSingle(model);
                new UpgradeTowerPopupController(this, _staticDataProviderService.GameConfig,
                    _gameDataProviderAndSaverService, _currenciesProviderService, popup, model);

                popup.DestroyReporter.Destroyed += () =>
                    _factoriesContainerProviderService.Container.MakeNullSingle<UpgradeTowerPopupModel>();
            }

            return popup;
        }

        public FriendsPopupView GetFriendsPopup()
        {
            var popup = _factoriesContainerProviderService.GetOrLoadAndRegisterMonoBehaviour
                <FriendsPopupView>(AssetPath.FriendsPopup, out var isLoaded);

            if (isLoaded)
            {
                var friendsPopupModel = new FriendsPopupModel(_gameDataProviderAndSaverService, this,
                    _restApiMediatorService, _staticDataProviderService.GameConfig);
                new FriendsPopupController(popup, friendsPopupModel);
            }

            return popup;
        }

        public DesktopPopupView GetDesktopPopup() =>
            _factoriesContainerProviderService.GetOrLoadAndRegisterMonoBehaviour
                <DesktopPopupView>(AssetPath.DesktopPopup);

        public DefeatPopupView GetDefeatPopup(GameStateMachine gameStateMachine)
        {
            var popup = _factoriesContainerProviderService.GetOrLoadAndRegisterMonoBehaviour
                <DefeatPopupView>(AssetPath.DefeatPopup, out var isLoaded);

            if (isLoaded)
                new DefeatPopupController(popup, gameStateMachine, _timeScaleChangingService);

            return popup;
        }

        public WinPopup GetWinPopup() =>
            _factoriesContainerProviderService.GetOrLoadAndRegisterMonoBehaviour
                <WinPopup>(AssetPath.WinPopup);

        public LevelPopupView GetLevelPopup() =>
            _factoriesContainerProviderService.GetOrLoadAndRegisterMonoBehaviour
                <LevelPopupView>(AssetPath.LevelPopup);

        public UpgradeTowerButtonView GetUpgradeTowerButtonView(Transform parent)
        {
            var prefab = _factoriesContainerProviderService.GetOrLoadAndRegisterMonoBehaviour
                <UpgradeTowerButtonView>(AssetPath.UpgradeTowerButtonView, false);

            return Object.Instantiate(prefab, parent);
        }

        public FriendImageView GetFriendImageView(Transform parent)
        {
            var prefab = _factoriesContainerProviderService.GetOrLoadAndRegisterMonoBehaviour
                <FriendImageView>(AssetPath.FriendImageView, false);

            var instance = Object.Instantiate(prefab, parent);
            return instance;
        }

        public TonWalletWindowView GetTonWalletWindowView(Transform parent)
        {
            var prefab = _factoriesContainerProviderService.GetOrLoadAndRegisterMonoBehaviour
                <TonWalletWindowView>(AssetPath.TonWalletWindowView, false);

            var instance = Object.Instantiate(prefab, parent);
            return instance;
        }

        public TextMeshPro GetTextOverEnemy(Vector2 startPosition)
        {
            var prefab = _factoriesContainerProviderService.GetOrLoadAndRegisterMonoBehaviour
                <TextMeshPro>(AssetPath.RewardForKillOverEnemyText, false);

            var instance = Object.Instantiate(prefab, startPosition, Quaternion.identity);

            return instance;
        }

        public MoneyCounterPopup GetMoneyCounterPopup()
        {
            var popup = _factoriesContainerProviderService.GetOrLoadAndRegisterMonoBehaviour
                <MoneyCounterPopup>(AssetPath.MoneyCounterPopup, out var isLoaded);

            if (isLoaded)
                popup.Construct(_currenciesProviderService);

            return popup;
        }

        public MainMenuWindowView GetMainMenuWindow() =>
            _factoriesContainerProviderService.GetOrLoadAndRegisterMonoBehaviour
                <MainMenuWindowView>(AssetPath.MainMenuWindow);

        public SettingsPopupView GetSettingsPopup(Button openButton)
        {
            var popup = _factoriesContainerProviderService.GetOrLoadAndRegisterMonoBehaviour
                <SettingsPopupView>(AssetPath.SettingsPopup, out var isLoaded);

            if (isLoaded)
            {
                var controller = new SettingsPopupController(popup, openButton, _staticDataProviderService,
                    _gameDataProviderAndSaverService);

                var tonWalletConnectionPopup = GetTonWalletConnectionPopup(popup, UnitonConnectSDK.Instance);
            }

            return popup;
        }

        private SelectedTonWalletConnectionPopupView GetSelectedTonWalletConnectionPopup(
            UnitonConnectSDK unitonConnectSdk,
            out SelectedTonWalletConnectionPopupModel model, out SelectedTonWalletConnectionPopupController controller)
        {
            var popup = _factoriesContainerProviderService.GetOrLoadAndRegisterMonoBehaviour
                <SelectedTonWalletConnectionPopupView>(AssetPath.SelectedTonWalletConnectionPopupView);

            model = new SelectedTonWalletConnectionPopupModel(unitonConnectSdk);
            controller = new SelectedTonWalletConnectionPopupController(popup, model);

            return popup;
        }

        private ChoosingTonWalletPopupView GetChoosingTonWalletPopup(UnitonConnectSDK unitonConnectSDK,
            SelectedTonWalletConnectionPopupController selectedTonWalletConnectionPopupController,
            SelectedTonWalletConnectionPopupView selectedTonWalletConnectionPopupView,
            TonWalletConnectionModel tonWalletConnectionModel, out
                ChoosingTonWalletPopupController controller)
        {
            var popup = _factoriesContainerProviderService.GetOrLoadAndRegisterMonoBehaviour
                <ChoosingTonWalletPopupView>(AssetPath.ChoosingTonWalletPopupView);

            var model = new ChoosingTonWalletPopupModel(unitonConnectSDK,
                _staticDataProviderService, this);

            controller = new ChoosingTonWalletPopupController(
                popup, model, selectedTonWalletConnectionPopupController,
                selectedTonWalletConnectionPopupView, tonWalletConnectionModel);

            return popup;
        }

        private WithdrawBooliPopupView GetWithdrawBooliPopupView(UnitonConnectSDK unitonSdk,
            TonWalletConnectionModel tonWalletCOnnectionModel)
        {
            var popup = _factoriesContainerProviderService.GetOrLoadAndRegisterMonoBehaviour
                <WithdrawBooliPopupView>(AssetPath.WithdreawBooliPopupView, out var isLoaded);

            if (isLoaded)
            {
                var model = new WithdrawBooliPopupModel(unitonSdk, _staticDataProviderService.GameConfig,
                    _currenciesProviderService, _gameDataProviderAndSaverService, _restApiMediatorService);
                new WithdrawBooliPopupController(popup, model, tonWalletCOnnectionModel);
            }

            return popup;
        }

        private TonWalletConnectionPopup GetTonWalletConnectionPopup(SettingsPopupView settingsPopupView,
            UnitonConnectSDK unitonConnectSdk)
        {
            var popup = _factoriesContainerProviderService.GetOrLoadAndRegisterMonoBehaviour
                <TonWalletConnectionPopup>(AssetPath.TonWalletConnectionPopup, out var isLoaded);

            if (isLoaded)
            {
                var model = new TonWalletConnectionModel(unitonConnectSdk);

                var selectedTonWalletConnectionPopup = GetSelectedTonWalletConnectionPopup(unitonConnectSdk,
                    out var selectedTonWalletConnectionPopupModel,
                    out var selectedTonWalletConnectionPopupController);

                var choosingTonWalletPopup = GetChoosingTonWalletPopup(unitonConnectSdk,
                    selectedTonWalletConnectionPopupController, selectedTonWalletConnectionPopup, model,
                    out var choosingTonWalletPopupController);

                var withdrawBooliPopupView = GetWithdrawBooliPopupView(unitonConnectSdk, model);

                var controller = new TonWalletConnectionController(popup, model, settingsPopupView, 
                    choosingTonWalletPopupController, choosingTonWalletPopup, withdrawBooliPopupView);
            }

            return popup;
        }
    }
}