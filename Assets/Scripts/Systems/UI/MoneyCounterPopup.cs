using Infrastructure.Services;
using Logic.UI;
using UnityEngine;

namespace Systems.UI
{
    public class MoneyCounterPopup : BasePopup
    {
        [SerializeField] private CounterView counterView;
        
        public void Construct(CurrenciesProviderService currenciesProviderService)
        {
            counterView.UpdateText(currenciesProviderService.Coins.Number);
            currenciesProviderService.Coins.NumberChanged += counterView.UpdateText;
        }
    }
}