using Systems.UI.DesktopPopup;
using Systems.UI.MainMenuWindow;
using Systems.UI.UpgradeTowerPopup;
using Cysharp.Threading.Tasks;
using Data;
using Holders;
using Infrastructure.Factory;
using Infrastructure.Services;
using Infrastructure.Services.SaveLoad;
using Infrastructure.Services.StaticData;
using RestApiSystem;
using Tools;
using UnityEngine.SceneManagement;

namespace Infrastructure.States
{
    public class MainMenuState : IState
    {
        private readonly IUIFactory _uiFactory;
        private readonly IGameFactory _gameFactory;
        private readonly GameStateMachine _gameStateMachine;
        private readonly GameDataProviderAndSaverService _dataProviderService;
        private readonly IStaticDataProviderService _staticDataService;
        private readonly CurrenciesProviderService _currenciesProviderService;
        private readonly GameDataProviderAndSaverService _gameDataProviderAndSaverService;
        private readonly FactoriesContainerProviderService _factoriesContainerProviderService;

        public MainMenuState(IUIFactory uiFactory, IGameFactory gameFactory, GameStateMachine gameStateMachine,
            GameDataProviderAndSaverService dataProviderService, IStaticDataProviderService staticDataService,
            CurrenciesProviderService currenciesProviderService, GameDataProviderAndSaverService gameDataProviderAndSaverService,
            FactoriesContainerProviderService factoriesContainerProviderService)
        {
            _gameStateMachine = gameStateMachine;
            _uiFactory = uiFactory;
            _gameFactory = gameFactory;
            _dataProviderService = dataProviderService;
            _staticDataService = staticDataService;
            _currenciesProviderService = currenciesProviderService;
            _gameDataProviderAndSaverService = gameDataProviderAndSaverService;
            _factoriesContainerProviderService = factoriesContainerProviderService;
        }

        public async UniTask Enter()
        {
            await SceneManager.LoadSceneAsync(SceneNames.MainMenu);
            _gameFactory.CreateButtonsClickSoundSource().SubscribeToButtons();
            _gameDataProviderAndSaverService.TryDownloadPlayerAvatar().Forget();

            var mainMenuWindow = _uiFactory.GetMainMenuWindow();
            var mainMenuController = new MainMenuWindowController(mainMenuWindow, _uiFactory);

            var desktopPopup = _uiFactory.GetDesktopPopup();
            var controller = new DesktopPopupController(_gameStateMachine,
                _dataProviderService, desktopPopup, _uiFactory, _staticDataService.GameConfig, 
                _currenciesProviderService, _factoriesContainerProviderService.Container
                    .GetSingle<UpgradeTowerPopupModel>());
            
            desktopPopup.OpenPopup();
            
            var audioPlayer = _gameFactory.CreateBackgroundAudioPlayer();

            audioPlayer.SetAudioClipsAndTryPlayNextSound(_staticDataService.GameConfig.SoundsConfig.MainMenuMusicClips);
        }

        public UniTask Exit() => UniTask.CompletedTask;
    }
}