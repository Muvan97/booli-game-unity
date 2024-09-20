using System.Collections.Generic;
using TonSdk.Connect;
using UnitonConnect.Core.Data.Common;
using UnitonConnect.Core.Utils;
using UnitonConnect.Core.Utils.View;
using UnityEngine;

namespace Systems.UI.SettingsPopup
{
    public class TonWalletWindowController
    {
        private readonly TonWalletWindowView _windowView;
        private readonly TonWalletWindowModel _model;
        private readonly TonWalletConnectionModel _tonWalletConnectionModel;
        private readonly SelectedTonWalletConnectionPopupView _selectedTonWalletConnectionPopupView;        
        private readonly SelectedTonWalletConnectionPopupController _selectedTonWalletConnectionPopupController;


        public TonWalletWindowController(TonWalletWindowModel model, TonWalletWindowView windowView,
            TonWalletConnectionModel tonWalletConnectionModel,
            SelectedTonWalletConnectionPopupView selectedTonWalletConnectionPopupView,
            SelectedTonWalletConnectionPopupController selectedTonWalletConnectionPopupController)
        {
            _windowView = windowView;
            _model = model;
            _tonWalletConnectionModel = tonWalletConnectionModel;
            _selectedTonWalletConnectionPopupView = selectedTonWalletConnectionPopupView;
            _selectedTonWalletConnectionPopupController = selectedTonWalletConnectionPopupController;
        }

        public void SetView(string appName, Texture2D iconTexture)
        {
            _model.AppName = appName;

            _windowView.TitleText.text = appName;
            _windowView.IconImage.sprite = WalletVisualUtils.GetSpriteFromTexture(iconTexture);

            _model.TargetConfig = WalletConnectUtils.GetTargetWalletConfigWithoutSecondBridge(
                WalletConfigComponents.SSE, appName, _tonWalletConnectionModel.LoadedWallets);

            _windowView.ConnectButton.onClick.RemoveAllListeners();
            _windowView.ConnectButton.onClick.AddListener(() => InitializeSelectedTonWalletConnectionPopupView(_tonWalletConnectionModel.LoadedWallets));
        }

        private void InitializeSelectedTonWalletConnectionPopupView(List<WalletConfig> walletConfigs)
        {
            if (WalletConnectUtils.HasMultipleBridgeTypes(_model.AppName, walletConfigs))
            {
                _model.JavascriptConfig = WalletConnectUtils.GetTargetWalletConfigWithoutSecondBridge(
                    WalletConfigComponents.JAVA_SCRIPT, _model.AppName, walletConfigs);
                _model.HttpConfig = WalletConnectUtils.GetTargetWalletConfigWithoutSecondBridge(
                    WalletConfigComponents.SSE, _model.AppName, walletConfigs);
            }

            _model.TargetConfig = _model.GetBridgeConfiguration(_model.TargetConfig);

            _selectedTonWalletConnectionPopupController.SetOptions(_model.TargetConfig);
            _selectedTonWalletConnectionPopupView.OpenPopup();
        }
    }
}