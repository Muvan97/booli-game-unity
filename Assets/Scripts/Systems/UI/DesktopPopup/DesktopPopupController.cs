using System;
using System.Linq;
using Systems.UI.UpgradeTowerPopup;
using Configs;
using Cysharp.Threading.Tasks;
using Holders;
using Infrastructure.Factory;
using Infrastructure.Services;
using Infrastructure.States;
using RestApiSystem;
using Tools;
using UnityEngine;

namespace Systems.UI.DesktopPopup
{
    public class DesktopPopupController
    {
        public DesktopPopupController(GameStateMachine gameStateMachine,
            GameDataProviderAndSaverService providerAndSaverService,
            DesktopPopupView popupView, IUIFactory uiFactory, GameConfig gameConfig,
            CurrenciesProviderService currenciesProviderService, UpgradeTowerPopupModel upgradeTowerPopupModel) =>
            Initialize(popupView, uiFactory, gameConfig, providerAndSaverService, gameStateMachine, 
                currenciesProviderService, upgradeTowerPopupModel).Forget();

        private async UniTask Initialize(DesktopPopupView popupView, IUIFactory uiFactory, GameConfig gameConfig,
            GameDataProviderAndSaverService gameDataProviderAndSaverService, GameStateMachine gameStateMachine,
            CurrenciesProviderService currenciesProviderService, UpgradeTowerPopupModel upgradeTowerPopupModel)
        {
            var isPlayerHasTower = IsHasTower(gameDataProviderAndSaverService);
            popupView.BuyTowerWarningImage.gameObject.SetActive(!isPlayerHasTower);

            if (!isPlayerHasTower)
            {
                popupView.PlayButton.interactable = false;

                void EnablePlayButtonAndUnsubscribe()
                {
                    popupView.PlayButton.interactable = true;
                    popupView.BuyTowerWarningImage.gameObject.SetActive(false);
                    upgradeTowerPopupModel.TowerUpgraded -= EnablePlayButtonAndUnsubscribe;
                }

                upgradeTowerPopupModel.TowerUpgraded += EnablePlayButtonAndUnsubscribe;
            }

            UITools.InitializeCoinsCounterView(popupView.CoinsCounterView, currenciesProviderService.Coins);
            UITools.InitializeCoinsCounterView(popupView.BooliCounterView, currenciesProviderService.Booli);
            uiFactory.GetSettingsPopup(popupView.OpenSettingsButton);
            Subscribe(popupView, uiFactory, gameStateMachine);
            InitializeWavesText(popupView, gameConfig, gameDataProviderAndSaverService).Forget();
            SubscribeTools.SubscribeToLocalizationChanged(locale =>
                InitializeWavesText(popupView, gameConfig, gameDataProviderAndSaverService).Forget(), popupView.DestroyReporter);

            UITools.InitializeUserNicknameText(popupView.UserShortNicknameText, gameDataProviderAndSaverService.PlayerNickname,
                gameConfig.ShortNicknameLength);
            await UITools.UpdateShortNicknameTextAndAvatarRawImage(popupView.UserAvatarRawImage, 
                gameDataProviderAndSaverService, popupView.UserShortNicknameText, popupView.destroyCancellationToken);
        }

        private static bool IsHasTower(GameDataProviderAndSaverService gameDataProviderAndSaverService) => 
            gameDataProviderAndSaverService.GameData.TowerUpgradeData.Any(data => data.Level > 0);
        

        private async UniTask InitializeWavesText(DesktopPopupView popupView, GameConfig gameConfig,
            GameDataProviderAndSaverService gameDataProviderAndSaverService)
        {
            popupView.WaveText.text =
                await LocalizationTools.GetLocalizedString(LocalizationKeysHolder.Wave, LocalizationTable.MainMenu)
                + " " + (gameDataProviderAndSaverService.GameData.OpenLevelIndex + 1) + " / " + gameConfig
                    .LevelsConfig.LevelConfigs.Count;
        }

        private void Subscribe(DesktopPopupView popupView, IUIFactory uiFactory, GameStateMachine gameStateMachine)
        {
            popupView.PlayButton.onClick.AddListener(gameStateMachine.Enter<LevelState>);
            popupView.OpenTaskPopupButton.onClick.AddListener(uiFactory.GetTaskPopup().OpenPopup);
            popupView.OpenFriendsPopupButton.onClick.AddListener(uiFactory.GetFriendsPopup().OpenPopup);
        }
    }
}