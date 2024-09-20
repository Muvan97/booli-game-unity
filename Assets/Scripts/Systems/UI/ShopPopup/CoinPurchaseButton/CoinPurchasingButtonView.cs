using Logic.Observers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.UI.ShopPopup.CoinPurchaseButton
{
    [RequireComponent(typeof(DestroyReporter))]
    public class CoinPurchasingButtonView : MonoBehaviour
    {
        [field: SerializeField] public DestroyReporter DestroyReporter { get; private set; }
        [field: SerializeField] public TMP_Text PriceText { get; private set; }
        [field: SerializeField] public TMP_Text NumberCoinsText { get; private set; }
        [field: SerializeField] public TMP_Text NumberBonusCoinsText { get; private set; }
        [field: SerializeField] public Button Button { get; private set; }
    }
}