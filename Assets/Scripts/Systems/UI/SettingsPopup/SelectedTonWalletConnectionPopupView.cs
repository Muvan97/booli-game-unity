using Logic.Observers;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.UI.SettingsPopup
{
    [RequireComponent(typeof(MonoBehaviourObserver))]
    public class SelectedTonWalletConnectionPopupView : BasePopup
    {
        public Button CloseButton => closeButton;
        [field: SerializeField] public RawImage QRCodeRawImage { get; private set; }
        [field: SerializeField] public Button DeepLinkButton { get; private set; }
        [field: SerializeField] public MonoBehaviourObserver Observer { get; private set; }
    }
}