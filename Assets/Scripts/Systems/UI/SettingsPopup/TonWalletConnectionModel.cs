using System.Collections.Generic;
using TonSdk.Connect;
using UnitonConnect.Core;

namespace Systems.UI.SettingsPopup
{
    public class TonWalletConnectionModel
    {
        public List<WalletConfig> LoadedWallets { get; set; }
        public WalletConfig LatestAuthorizedWallet { get; set; }
        public readonly List<TonWalletWindowView> ActiveWalletsViews = new List<TonWalletWindowView>();
        public readonly UnitonConnectSDK UnitonConnectSDK;

        public TonWalletConnectionModel(UnitonConnectSDK unitonConnectSdk) => UnitonConnectSDK = unitonConnectSdk;
    }
}