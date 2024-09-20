using System.Collections.Generic;
using Systems.UI.ShopPopup.CoinPurchaseButton;
using Configs;
using Infrastructure.Services;
using RestApiSystem;
using UnityEngine;

namespace Systems.UI.ShopPopup
{
    public class ShopPopupController
    {
        public ShopPopupController(GameConfig gameConfig, ShopPopupView shopPopupView, 
        RestApiMediatorService restApiMediatorService, CurrenciesProviderService currenciesProviderService, 
        GameDataProviderAndSaverService gameDataProviderAndSaverService)
        {
            FillInvoiceLinkHolder(shopPopupView);
            CreateCoinPurchaseButtonsViews(gameConfig, shopPopupView, restApiMediatorService,
                currenciesProviderService, gameDataProviderAndSaverService);
        }

        private static void FillInvoiceLinkHolder(ShopPopupView shopPopupView)
        {
            if (InvoiceLinksHolder.Links != null) return;
            
            InvoiceLinksHolder.Links = new List<string>(shopPopupView.CoinPurchaseButtonViews.Count);
            for (var i = 0; i < InvoiceLinksHolder.Links.Capacity; i++)
                InvoiceLinksHolder.Links.Add(null);
        }

        private static void CreateCoinPurchaseButtonsViews(GameConfig gameConfig, ShopPopupView shopPopupView,
            RestApiMediatorService restApiMediatorService, CurrenciesProviderService currenciesProviderService,
            GameDataProviderAndSaverService gameDataProviderAndSaverService)
        {
            for (var i = 0; i < shopPopupView.CoinPurchaseButtonViews.Count; i++)
            {
                var view = shopPopupView.CoinPurchaseButtonViews[i];
                if (i >= gameConfig.CoinPurchaseButtonConfigs.Count)
                    continue;

                var index = i;
                
                var model = new CoinPurchasingButtonModel(restApiMediatorService,
                    index, gameConfig.CoinPurchaseButtonConfigs[i], gameDataProviderAndSaverService, 
                    currenciesProviderService);
                var controller = new CoinPurchasingButtonController(view, model);
            }
        }
    }
}