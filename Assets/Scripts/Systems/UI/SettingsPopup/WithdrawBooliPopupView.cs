using System.Globalization;
using Holders;
using Logic.Observers;
using Logic.UI;
using TMPro;
using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.UI.SettingsPopup
{
    [RequireComponent(typeof(MonoBehaviourObserver))]
    public class WithdrawBooliPopupView : BasePopup
    {
        [field: SerializeField] public CounterView BooliCounterView { get; private set; }
        [field: SerializeField] public MonoBehaviourObserver Observer { get; private set; }
        [field: SerializeField] public TMP_Text CommissionText { get; private set; }
        [field: SerializeField] public TMP_Text SendingStateText { get; private set; }
        [field: SerializeField] public TMP_InputField AmountInputField { get; private set; }
        [field: SerializeField] public Button SendTonButton { get; private set; }

        [SerializeField] private TMP_Text _balanceText;

        public async void SetTonBalanceText(decimal balance) => _balanceText.text = await LocalizationTools.GetLocalizedString(
            LocalizationKeysHolder.Balance, LocalizationTable.WithdrawBooliPopup) + ": " + balance + " TON";
    }
}