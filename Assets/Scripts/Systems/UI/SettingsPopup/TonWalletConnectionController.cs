using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Holders;
using Newtonsoft.Json;
using TonSdk.Connect;
using TonSdk.Core;
using Tools;
using UnitonConnect.Core.Utils;
using UnitonConnect.Core.Utils.Debugging;
using UnitonConnect.Core.Utils.View;
using UnitonConnect.Editor.Common;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Systems.UI.SettingsPopup
{
    public class TonWalletConnectionController
    {
        private readonly TonWalletConnectionModel _model;
        private readonly SettingsPopupView _settingsPopupView;
        private readonly TonWalletConnectionPopup _view;
        private readonly ChoosingTonWalletPopupController _choosingTonWalletPopupController;
        private readonly ChoosingTonWalletPopupView _choosingTonWalletPopupView;
        private readonly WithdrawBooliPopupView _withdrawBooliPopupView;

        public TonWalletConnectionController(TonWalletConnectionPopup view, TonWalletConnectionModel model,
            SettingsPopupView settingsPopupView, ChoosingTonWalletPopupController choosingTonWalletPopupController,
            ChoosingTonWalletPopupView choosingTonWalletPopupView, WithdrawBooliPopupView withdrawBooliPopupView)
        {
            _view = view;
            _model = model;
            _settingsPopupView = settingsPopupView;
            _choosingTonWalletPopupController = choosingTonWalletPopupController;
            _choosingTonWalletPopupView = choosingTonWalletPopupView;
            _withdrawBooliPopupView = withdrawBooliPopupView;

            view.DestroyReporter.Destroyed += Unsubscribe;
            Subscribe();

            SetActiveSendBooliButton();
            ChangeLinkOrUnlinkText().Forget();

            SubscribeTools.SubscribeToLocalizationChanged(locale => ChangeLinkOrUnlinkText().Forget(),
                _view.DestroyReporter);

            if (_model.UnitonConnectSDK.IsWalletConnected)
                _settingsPopupView.ShortWalletAddressText.text =
                    WalletVisualUtils.ProcessWalletAddress(_model.UnitonConnectSDK.GetWalletAddress(), 6);
        }

        private void SetActiveSendBooliButton() =>
            _settingsPopupView.WithdrawBooliButton.gameObject.SetActive(_model.UnitonConnectSDK.IsWalletConnected);

        private void OnClickLinkOrUnlinkWalletButton()
        {
            if (_model.UnitonConnectSDK.IsWalletConnected)
            {
                UnlinkWallet().Forget();
            }
            else
            {
                OpenChoosingWalletPopupButton();
            }
        }

        private async UniTask UnlinkWallet()
        {
            UnitonConnectLogger.Log("The disconnecting process of the previously connected wallet has been started");

            await _model.UnitonConnectSDK.DisconnectWallet();

            UnitonConnectLogger.Log("Success disconnect");
        }

        private void OpenChoosingWalletPopupButton() => _choosingTonWalletPopupView.OpenPopup();

        private async UniTask ChangeLinkOrUnlinkText()
        {
            _settingsPopupView.LinkOrUnlinkWalletText.text = await LocalizationTools.GetLocalizedString(
                _model.UnitonConnectSDK.IsWalletConnected
                    ? LocalizationKeysHolder.UnlinkWallet
                    : LocalizationKeysHolder.LinkWallet, LocalizationTable.SettingsPopup);
        }

        private void LoadWallets(Action<List<WalletConfig>> walletsLoaded)
        {
            _model.UnitonConnectSDK.LoadWalletsConfigs(ProjectStorageConsts.TEST_SUPPORTED_WALLETS_LINK,
                walletsConfigs => walletsLoaded?.Invoke(walletsConfigs));
        }

        private void UpdateWallets()
        {
            _choosingTonWalletPopupController.DestroyWalletsWindows();
            LoadWallets(walletsConfig =>
            {
                _model.LoadedWallets = WalletConnectUtils.GetSupportedWalletsListForUse(walletsConfig);

                if (_model.UnitonConnectSDK.IsWalletConnected)
                {
                    UnitonConnectLogger.Log("SDK is already initialized, " +
                                            "there is no need to reconnect the wallet. To reconnect, " +
                                            "you need to disconnect the previously connected wallet");

                    return;
                }

                _choosingTonWalletPopupController.CreateWalletsWindows(walletsConfig).Forget();
            });
        }

        private void Subscribe()
        {
            _settingsPopupView.WithdrawBooliButton.onClick.AddListener(_withdrawBooliPopupView.OpenPopup);
            _settingsPopupView.LinkOrUnlinkWalletButton.onClick.AddListener(OnClickLinkOrUnlinkWalletButton);

            _model.UnitonConnectSDK.OnWalletConnectionFinished += WalletConnectionFinished;
            _model.UnitonConnectSDK.OnWalletConnectionFailed += OnWalletConnectionFailed;
            _model.UnitonConnectSDK.OnWalletConnectionRestored += OnWalletConnectionRestored;
            
            _model.UnitonConnectSDK.RestoreConnectionAsync();

            _model.UnitonConnectSDK.OnWalletDisconnected += OnWalletDisconnect;

            if (_model.ActiveWalletsViews.Count < 1)
                _model.UnitonConnectSDK.OnWalletDisconnected += UpdateWallets;

            if (_model.UnitonConnectSDK.IsInitialized)
                UpdateWallets();
            else
                _model.UnitonConnectSDK.OnInitialized += UpdateWallets;
        }

        private void Unsubscribe()
        {
            _model.UnitonConnectSDK.OnInitialized -= UpdateWallets;

            _model.UnitonConnectSDK.OnWalletConnectionFinished -= WalletConnectionFinished;
            _model.UnitonConnectSDK.OnWalletConnectionFailed -= OnWalletConnectionFailed;
            _model.UnitonConnectSDK.OnWalletConnectionRestored -= OnWalletConnectionRestored;

            _model.UnitonConnectSDK.OnWalletDisconnected -= OnWalletDisconnect;

            if (_model.ActiveWalletsViews.Count < 1)
            {
                _model.UnitonConnectSDK.OnWalletDisconnected -= UpdateWallets;
            }
        }

        private WalletConfig GetWalletConfigByName(Wallet wallet)
        {
            var loadedConfig = WalletConnectUtils.GetConfigOfSpecifiedWallet(
                _model.LoadedWallets, wallet.Device.AppName);

            return loadedConfig;
        }

        private void WalletConnectionFinished(Wallet wallet)
        {
            var isWalletConnected = _model.UnitonConnectSDK.IsWalletConnected;

            ChangeLinkOrUnlinkText().Forget();
            SetActiveSendBooliButton();

            _settingsPopupView.ShortWalletAddressText.text = isWalletConnected
                ? GetShortWalletAdress(wallet)
                : string.Empty;

            if (isWalletConnected)
            {
                var successConnectMessage = $"Wallet is connected, full account address: {wallet.Account.Address}, \n" +
                                            $"Platform: {wallet.Device.Platform}, " +
                                            $"Name: {wallet.Device.AppName}, " +
                                            $"Version: {wallet.Device.AppVersion}";

                Debug.Log(successConnectMessage);

                var userAddress = $"{wallet.Account.Address}";

                _choosingTonWalletPopupView.ClosePopup();

                UnitonConnectLogger.Log($"Connected wallet short address: {GetShortWalletAdress(wallet)}");

                if (_model.LoadedWallets != null)
                {
                    Debug.Log(GetWalletConfigByName(wallet));
                    _model.LatestAuthorizedWallet = GetWalletConfigByName(wallet);

                    UnitonConnectLogger.Log($"The current wallet in the list is detected: " +
                                            $"{JsonConvert.SerializeObject(_model.LatestAuthorizedWallet)}");
                }
            }
            else
            {
                UnitonConnectLogger.LogWarning($"Connect status: " +
                                               $"{_model.UnitonConnectSDK.IsWalletConnected}");
            }
        }

        private static string GetShortWalletAdress(Wallet wallet)
        {
            return WalletVisualUtils.ProcessWalletAddress(
                wallet.Account.Address.ToString(), 6);
        }

        private void OnWalletConnectionFailed(string message)
        {
            UnitonConnectLogger.LogError($"Failed to connect " +
                                         $"the wallet due to the following reason: {message}");
        }

        private void OnWalletConnectionRestored(bool isRestored, WalletConfig wallet)
        {
            if (isRestored)
            {
                _model.LatestAuthorizedWallet = wallet;

                UnitonConnectLogger.Log($"Connection to previously connected wallet {wallet.Name} restored," +
                                        $" local configuration updated");
            }
        }

        private void OnWalletDisconnect()
        {
            _model.ActiveWalletsViews.ForEach(wallet => Object.Destroy(wallet.gameObject));
            SetActiveSendBooliButton();
            ChangeLinkOrUnlinkText().Forget();
            _settingsPopupView.ShortWalletAddressText.text = string.Empty;
            _model.ActiveWalletsViews.Clear();
        }
    }
}