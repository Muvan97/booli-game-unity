using TonSdk.Connect;
using UnitonConnect.Core;
using UnityEngine;

namespace Systems.UI.SettingsPopup
{
    public class SelectedTonWalletConnectionPopupModel
    {
        public Texture2D QRCodeForConnect;
        public WalletConfig CurrentConfig;
        public string ConnectionURL;
        public readonly UnitonConnectSDK UnitonConnectSDK;

        public SelectedTonWalletConnectionPopupModel(UnitonConnectSDK unitonConnectSdk)
        {
            UnitonConnectSDK = unitonConnectSdk;
        }
    }
}