using Systems.UI;
using Logic.Observers;
using Logic.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.Level.LevelPopup
{
    public class LevelPopupView : BasePopup
    {
        [field: SerializeField] public RectTransform LowerElementsParent { get; private set; }
        [field: SerializeField] public Image ClickOnTowerButtonNotificationImage { get; private set; }
        [field: SerializeField] public TMP_Text LifeCounterText { get; private set; }
        [field: SerializeField] public Button SetActiveStateBuildingPopupButton { get; private set; }
        [field: SerializeField] public TMP_Text RemainingEnemyCounterText { get; private set; }
        [field: SerializeField] public TMP_Text CurrentWaveText { get; private set; }
        [field: SerializeField] public TMP_Text GameAccelerationPriceText { get; private set; }
        [field: SerializeField] public CounterView CoinsCounterView { get; private set; }
        [field: SerializeField] public Button GameAccelerationButton { get; private set; }
        [field: SerializeField] public DestroyReporter DestroyReporter { get; private set; }
    }
}