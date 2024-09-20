using TMPro;
using Tools;
using UnityEngine;

namespace Logic.UI
{
    public class CounterView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        public void UpdateText(decimal moneyNumber)
            => _text.text = moneyNumber.ToKMBString();
    }
}