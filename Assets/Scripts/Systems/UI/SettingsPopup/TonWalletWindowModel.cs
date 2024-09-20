using TonSdk.Connect;
using UnitonConnect.Core;
using UnitonConnect.Core.Utils;

namespace Systems.UI.SettingsPopup
{
    public class TonWalletWindowModel
    {
        public WalletConfig JavascriptConfig;
        public WalletConfig HttpConfig;
        public WalletConfig TargetConfig;
        private readonly UnitonConnectSDK _unitonConnectSdk;

        public string AppName;

        public TonWalletWindowModel(UnitonConnectSDK unitonConnectSdk) => _unitonConnectSdk = unitonConnectSdk;

        public WalletConfig GetBridgeConfiguration(WalletConfig config)
        {
            WalletConfig targetConfig;

            if (IsUseWebWallets() && WalletConnectUtils.HasJSBridge(config))
            {
                targetConfig = JavascriptConfig;
            }
            
            else if (!IsUseWebWallets() && WalletConnectUtils.HasHttpBridge(config))
            {
                targetConfig = HttpConfig;
            }

            return targetConfig;
        }
        
        private bool IsUseWebWallets() => _unitonConnectSdk.IsUseWebWallets;
    }
}