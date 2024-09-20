using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using TonSdk.Connect;
using UnitonConnect.Core.Data;
using UnitonConnect.Core.Utils;
using UnitonConnect.Core.Utils.Debugging;
using UnitonConnect.Core.Utils.View;
using UnityEngine;

namespace Systems.UI.SettingsPopup
{
    public class ChoosingTonWalletPopupController
    {
        private readonly ChoosingTonWalletPopupView _view;
        private readonly ChoosingTonWalletPopupModel _model;
        private readonly SelectedTonWalletConnectionPopupController _selectedTonWalletConnectionPopupController;
        private readonly SelectedTonWalletConnectionPopupView _selectedTonWalletConnectionPopupView;
        private readonly TonWalletConnectionModel _tonWalletConnectionModel;

        public ChoosingTonWalletPopupController(ChoosingTonWalletPopupView view, ChoosingTonWalletPopupModel model,
            SelectedTonWalletConnectionPopupController selectedTonWalletConnectionPopupController,
            SelectedTonWalletConnectionPopupView selectedTonWalletConnectionPopupView,
            TonWalletConnectionModel tonWalletConnectionModel)
        {
            _view = view;
            _model = model;
            _selectedTonWalletConnectionPopupController = selectedTonWalletConnectionPopupController;
            _selectedTonWalletConnectionPopupView = selectedTonWalletConnectionPopupView;
            _tonWalletConnectionModel = tonWalletConnectionModel;
            
            _view.CloseButton.onClick.AddListener(_model.UnitonConnectSDK.PauseConnection);
        }

        public void DestroyWalletsWindows() => _model.ActiveWalletsViews.ForEach(wallet => Object.Destroy(wallet.gameObject));

        public async UniTask CreateWalletsWindows(List<WalletConfig> wallets)
        {
            var walletsConfigs = WalletConnectUtils.GetSupportedWalletsListForUse(wallets);

            UnitonConnectLogger.Log($"Created {walletsConfigs.Capacity} wallets");

            //_model.LoadedWallets = walletsConfigs;

            UnitonConnectLogger.Log(JsonConvert.SerializeObject(walletsConfigs));

            var walletsViewList = new List<WalletViewData>();

            foreach (var wallet in _tonWalletConnectionModel.LoadedWallets)
            {
                var walletView = await WalletVisualUtils.GetWalletViewIfIconIsNotExist(
                    wallet, _model.StaticDataProviderService.WalletsProvidersData);

                walletsViewList.Add(walletView);
            }

            walletsViewList.ForEach(CreateTonWalletWindow);
        }
        
        private void CreateTonWalletWindow(WalletViewData walletViewData)
        {
            var name = walletViewData.Name;
            var icon = walletViewData.Icon;

            var walletWindowView = _model.UiFactory.GetTonWalletWindowView(_view.WalletsParent);
            var walletWindowModel = new TonWalletWindowModel(_model.UnitonConnectSDK);
            var walletWindowController = new TonWalletWindowController(walletWindowModel,
                walletWindowView, _tonWalletConnectionModel, _selectedTonWalletConnectionPopupView,
                _selectedTonWalletConnectionPopupController);

            walletWindowController.SetView(name, icon);

            _model.ActiveWalletsViews.Add(walletWindowView);
        }
    }
}