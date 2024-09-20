using Configs;
using Infrastructure.Services;
using RestApiSystem;
using UnitonConnect.Core;

namespace Systems.UI.SettingsPopup
{
    public class WithdrawBooliPopupModel
    {
        public readonly GameDataProviderAndSaverService GameDataProviderAndSaverService;
        public readonly CurrenciesProviderService CurrenciesProviderService;
        public readonly RestApiMediatorService RestApiMediatorService;
        public readonly UnitonConnectSDK UnitonConnectSdk;
        public readonly GameConfig GameConfig;
        public long LastInputtedBooliAmount;

        public WithdrawBooliPopupModel(UnitonConnectSDK unitonConnectSdk, GameConfig gameConfig, 
            CurrenciesProviderService currenciesProviderService, GameDataProviderAndSaverService gameDataProviderAndSaverService, 
            RestApiMediatorService restApiMediatorService)
        {
            UnitonConnectSdk = unitonConnectSdk;
            GameConfig = gameConfig;
            CurrenciesProviderService = currenciesProviderService;
            GameDataProviderAndSaverService = gameDataProviderAndSaverService;
            RestApiMediatorService = restApiMediatorService;
        }
    }
}