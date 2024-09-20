using Systems.UI.BuildingTowerPopup;
using Cysharp.Threading.Tasks;
using Holders;
using Tools;

namespace Systems.Level.LevelPopup
{
    public class LevelPopupController
    {
        private readonly LevelPopupView _view;
        private readonly LevelPopupModel _model;

        public LevelPopupController(LevelPopupView levelPopupView, BuildingTowerPopupView buildingTowerPopupView, LevelPopupModel model)
        {
            _view = levelPopupView;
            _model = model;

            var isFirstLevel = _model.GameDataProviderAndSaverService.GameData.OpenLevelIndex == 0;
            levelPopupView.ClickOnTowerButtonNotificationImage.gameObject.SetActive(isFirstLevel);
            
            if (isFirstLevel)
                levelPopupView.SetActiveStateBuildingPopupButton.onClick.AddListener(SetActiveClickOnTowerButtonNotificationImage);

            InitializeWaveText().Forget();
            InitializeGameAccelerationPriceText(levelPopupView);
            UpdateViewCoinsCounter(_model.CurrenciesProviderService.Coins.Number);
            Subscribe(buildingTowerPopupView, levelPopupView);
        }

        private void SetActiveClickOnTowerButtonNotificationImage() =>
            _view.ClickOnTowerButtonNotificationImage.gameObject.SetActive(
                !_view.ClickOnTowerButtonNotificationImage.gameObject.activeSelf);

        private void InitializeGameAccelerationPriceText(LevelPopupView levelPopupView) 
            => levelPopupView.GameAccelerationPriceText.text = "<sprite=0>" + _model.GameConfig.PriceForUsingGameAcceleration;

        private void Subscribe(BuildingTowerPopupView buildingTowerPopupView, LevelPopupView levelPopupView)
        {
            _model.CurrenciesProviderService.Coins.NumberChanged += UpdateViewCoinsCounter;
            _view.DestroyReporter.Destroyed += Unsubscribe;
            SubscribeTools.SubscribeToLocalizationChanged((locale => InitializeWaveText().Forget()), 
                levelPopupView.DestroyReporter);
            buildingTowerPopupView.Opened += () => MoveLowerElementsParent(true);
            buildingTowerPopupView.Closed += () => MoveLowerElementsParent(false);
        }
        
        private void MoveLowerElementsParent(bool isBuildingTowerPopupOpen)
        {
            _view.LowerElementsParent.transform.position = isBuildingTowerPopupOpen
                ? _model.LowerElementsPositionOnBuildingPopupOpen
                : _model.LowerElementsParentStartPosition;
        }
        private async UniTask InitializeWaveText()
        {
            _view.CurrentWaveText.text =
                await LocalizationTools.GetLocalizedString(LocalizationKeysHolder.Wave, LocalizationTable.MainMenu)
                + " " + (_model.GameDataProviderAndSaverService.GameData.OpenLevelIndex + 1) + " / " + _model.GameConfig
                    .LevelsConfig.LevelConfigs.Count;
        }
        
        private void UpdateViewCoinsCounter(decimal number) => _view.CoinsCounterView.UpdateText(number);

        private void Unsubscribe() => _model.CurrenciesProviderService.Coins.NumberChanged -= UpdateViewCoinsCounter;
    }
}