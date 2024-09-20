using Logic.Observers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.UI.UpgradeTowerPopup.UpgradeTowerButton
{
    [RequireComponent(typeof(DestroyReporter))]
    public class UpgradeTowerButtonView : MonoBehaviour
    {
        [field: SerializeField] public DestroyReporter DestroyReporter { get; private set; }
        [field: SerializeField] public Button UpgradeButton { get; private set; }
        [field: SerializeField] public Image TowerImage { get; private set; }
        [field: SerializeField] public TMP_Text TowerNameText { get; private set; }
        [field: SerializeField] public TMP_Text LevelNumberText { get; private set; }
        [field: SerializeField] public TMP_Text CooldownNumberText { get; private set; }
        [field: SerializeField] public TMP_Text DamageNumberText { get; private set; }
        [field: SerializeField] public TMP_Text PriceText { get; private set; }
        [field: SerializeField] public TMP_Text UpgradeText { get; private set; }
    }
}