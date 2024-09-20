using System.Collections.Generic;
using Infrastructure.Factory;
using Infrastructure.Services.StaticData;
using UnitonConnect.Core;

namespace Systems.UI.SettingsPopup
{
    public class ChoosingTonWalletPopupModel
    {
        public readonly List<TonWalletWindowView> ActiveWalletsViews = new List<TonWalletWindowView>();
        public readonly UnitonConnectSDK UnitonConnectSDK;
        public readonly IStaticDataProviderService StaticDataProviderService;
        public readonly IUIFactory UiFactory;

        public ChoosingTonWalletPopupModel(UnitonConnectSDK unitonConnectSdk, 
            IStaticDataProviderService staticDataProviderService, IUIFactory uiFactory)
        {
            UnitonConnectSDK = unitonConnectSdk;
            StaticDataProviderService = staticDataProviderService;
            UiFactory = uiFactory;
        }
    }
}