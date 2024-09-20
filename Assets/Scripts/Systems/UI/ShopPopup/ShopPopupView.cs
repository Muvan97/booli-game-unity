using System.Collections.Generic;
using Systems.UI.ShopPopup.CoinPurchaseButton;
using UnityEngine;

namespace Systems.UI.ShopPopup
{
    public class ShopPopupView : BasePopup
    {
        [field: SerializeField] public List<CoinPurchasingButtonView> CoinPurchaseButtonViews { get; private set; }
    }
}