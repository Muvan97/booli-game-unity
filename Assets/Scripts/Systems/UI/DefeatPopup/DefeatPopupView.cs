using UnityEngine;
using UnityEngine.UI;

namespace Systems.UI.DefeatPopup
{
    [RequireComponent(typeof(AudioSource))]
    public class DefeatPopupView : BasePopup
    {
        [field: SerializeField] public Button HomeButton { get; private set; }
        [field: SerializeField] public Button AgainButton { get; private set; }
    }
}