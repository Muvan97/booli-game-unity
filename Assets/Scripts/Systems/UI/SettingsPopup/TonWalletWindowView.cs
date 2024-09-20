using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.UI.SettingsPopup
{
    public class TonWalletWindowView : MonoBehaviour
    {
        [field: SerializeField] public TMP_Text TitleText { get; private set; }
        [field: SerializeField] public Image IconImage { get; private set; }
        [field: SerializeField] public Button ConnectButton { get; private set; }
    }
}