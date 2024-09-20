using Infrastructure.Services;
using RestApiSystem;
using UnityEngine;

namespace Systems.UI.ShopPopup.CoinPurchaseButton
{
    public class CoinPurchasingButtonModel
    {
        public string InvoiceLink => InvoiceLinksHolder.Links[Index];
        
        public readonly int Index;

        public readonly RestApiMediatorService RestApiMediatorService;
        public readonly CoinPurchasingButtonConfig Config;
        public readonly GameDataProviderAndSaverService GameDataProviderAndSaverService;
        public readonly CurrenciesProviderService CurrenciesProviderService;

        public CoinPurchasingButtonModel(RestApiMediatorService restApiMediatorService, int index, 
            CoinPurchasingButtonConfig config, GameDataProviderAndSaverService gameDataProviderAndSaverService, 
            CurrenciesProviderService currenciesProviderService)
        {
            Index = index;
            Config = config;
            GameDataProviderAndSaverService = gameDataProviderAndSaverService;
            CurrenciesProviderService = currenciesProviderService;
            RestApiMediatorService = restApiMediatorService;
        }

        public void UpdateInvoiceLink(string invoiceLink) => InvoiceLinksHolder.Links[Index] = invoiceLink;
    }
}