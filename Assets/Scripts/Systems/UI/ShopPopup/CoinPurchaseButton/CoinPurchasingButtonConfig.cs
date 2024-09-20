using System;
using UnityEngine;

namespace Systems.UI.ShopPopup.CoinPurchaseButton
{
    [Serializable]
    public class CoinPurchasingButtonConfig
    {
        [field: SerializeField] public int NumberMoney { get; private set; }
        [field: SerializeField] public int NumberBonusMoney { get; private set; }
        [field: SerializeField] public int Price { get; private set; }
        [field: SerializeField] public int ItemIndex { get; private set; }
        [field: SerializeField] public string ItemDescription { get; private set; }
    }
}