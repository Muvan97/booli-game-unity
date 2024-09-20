using Logic.Observers;
using Logic.UI;
using UnityEngine;

namespace Systems.UI.UpgradeTowerPopup
{
    [RequireComponent(typeof(DestroyReporter))]
    public class UpgradeTowerPopupView : BasePopup
    {
        [field: SerializeField] public DestroyReporter DestroyReporter { get; private set; }
        [field: SerializeField] public Transform BattonsParent { get; private set; }
        [field: SerializeField] public CounterView CoinsCounterView { get; private set; }
    }
}