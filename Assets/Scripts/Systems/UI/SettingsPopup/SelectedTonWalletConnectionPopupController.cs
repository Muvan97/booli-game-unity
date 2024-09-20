using TonSdk.Connect;
using UnitonConnect.Core;
using UnitonConnect.Core.Utils;
using UnitonConnect.Core.Utils.Debugging;
using UnitonConnect.Core.Utils.View;
using UnityEngine;

namespace Systems.UI.SettingsPopup
{
    public class SelectedTonWalletConnectionPopupController
    {
        private readonly SelectedTonWalletConnectionPopupView _view;
        private readonly SelectedTonWalletConnectionPopupModel _model;

        public SelectedTonWalletConnectionPopupController(SelectedTonWalletConnectionPopupView view,
            SelectedTonWalletConnectionPopupModel model)
        {
            _view = view;
            _model = model;
            
            _view.Observer.Enabled += OnObserverEnable;
            _view.Observer.Disabled += OnObserverDisable;
            
            _view.CloseButton.onClick.AddListener(_model.UnitonConnectSDK.PauseConnection);
        }

        private void OnObserverEnable() => _model.UnitonConnectSDK.OnWalletConnectionFinished += OnWalletConnectionFinish;

        private void OnObserverDisable()
        {
            _model.UnitonConnectSDK.OnWalletConnectionFinished -= OnWalletConnectionFinish;

            _view.DeepLinkButton.onClick.RemoveAllListeners();
        }

        private async void LoadConnectWalletContent()
        {
            _model.ConnectionURL = await _model.UnitonConnectSDK.GenerateConnectURL(_model.CurrentConfig);

            UnitonConnectLogger.Log($"Generated connect link {_model.ConnectionURL} " +
                                    $"for wallet: {_model.CurrentConfig.Name}");

            _model.QRCodeForConnect = WalletVisualUtils.GetQRCodeFromUrl(_model.ConnectionURL);

            _view.DeepLinkButton.onClick.AddListener(Connect);

            _view.QRCodeRawImage.texture = _model.QRCodeForConnect;
        }

        private void OnWalletConnectionFinish(Wallet wallet) => _view.ClosePopup();

        public async void SetOptions(WalletConfig connectionConfig)
        {
            _model.CurrentConfig = connectionConfig;

            if (_model.UnitonConnectSDK.IsWalletConnected)
            {
                Debug.LogWarning($"The wallet named {connectionConfig.Name} is already connected," +
                                 $" the process of disconnecting it from the session begins");

                await _model.UnitonConnectSDK.DisconnectWallet();

                return;
            }

            LoadConnectWalletContent();
        }

        private async void Connect()
        {
            if (WalletConnectUtils.HasHttpBridge(_model.CurrentConfig))
            {
                _model.UnitonConnectSDK.ConnectHttpBridgeWalletViaDeepLink(
                    _model.CurrentConfig, _model.ConnectionURL);
            }
            else if (WalletConnectUtils.HasJSBridge(_model.CurrentConfig) &&
                     _model.UnitonConnectSDK.IsUseWebWallets)
            {
                await _model.UnitonConnectSDK.ConnectJavaScriptBridgeWalletViaDeeplink(_model.CurrentConfig);
            }
        }
    }
}