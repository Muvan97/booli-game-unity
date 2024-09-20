using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using Holders;
using Newtonsoft.Json;
using RestApiSystem;
using Tools;
using UnigramPayment.Runtime.Common;
using UnigramPayment.Runtime.Core;
using UnityEngine;

namespace Systems.UI.ShopPopup.CoinPurchaseButton
{
    public class CoinPurchasingButtonController
    {
        private readonly CoinPurchasingButtonModel _model;
        
        public CoinPurchasingButtonController(CoinPurchasingButtonView view,
            CoinPurchasingButtonModel model)
        {
            _model = model;
            
            view.PriceText.text = _model.Config.Price + " TON";
            view.NumberBonusCoinsText.text = "+" +  _model.Config.NumberBonusMoney.ToKMBString();
            view.Button.onClick.AddListener(() => OpenPurchasingWindow().Forget());
            InitializeNumberCoinsText(_model.Config, view).Forget();
            SubscribeTools.SubscribeToLocalizationChanged(locale => InitializeNumberCoinsText(_model.Config, view).Forget(), view.DestroyReporter);
        }

        private async UniTask OpenPurchasingWindow()
        {
            if (_model.InvoiceLink == null)
            {
                var fields = new Dictionary<string, string>
                {
                    {
                        RequestFieldNames.InvoiceData, JsonConvert.SerializeObject(
                            new Dictionary<string, string>
                            {
                                {InvoiceFields.Title, (_model.Config.NumberMoney + _model.Config.NumberBonusMoney).ToKMBString() + " " + 
                                                      await LocalizationTools.GetLocalizedString(
                                                          LocalizationKeysHolder.Coins, LocalizationTable.ShopPopup)},
                                {InvoiceFields.Description, _model.Config.ItemDescription},
                                {InvoiceFields.Payload, _model.Config.ItemIndex.ToString()},
                                {InvoiceFields.ProviderToken, ""},
                                {InvoiceFields.Currency, "XTR"},
                                {InvoiceFields.Amount, $"{_model.Config.Price}"},
                            })
                    }
                };

                var result = await _model.RestApiMediatorService.GetProcessedRequestResult(Request.Post, fields);

                if (RestApiTools.IsRequestResultHasErrors(result))
                    return;
                
                _model.UpdateInvoiceLink(result[RestApiMediatorKeys.InvoiceLink]);
            }
            
            UnigramPaymentSDK.Instance.OpenPurchaseInvoice(_model.InvoiceLink, OnInvoiceClose);
        }

        private void OnInvoiceClose(PaymentStatus status, string message)
        {
            if (status != PaymentStatus.paid) return;
            
            _model.CurrenciesProviderService.Coins.Number += _model.Config.NumberMoney + _model.Config.NumberBonusMoney;
            _model.GameDataProviderAndSaverService.SaveData();
        }

        private async UniTask InitializeNumberCoinsText(CoinPurchasingButtonConfig config, CoinPurchasingButtonView view)
        {
            view.NumberCoinsText.text = config.NumberMoney.ToKMBString() + " " +
                                        await LocalizationTools.GetLocalizedString(LocalizationKeysHolder.Coins,
                                            LocalizationTable.ShopPopup);
        }
    }
}