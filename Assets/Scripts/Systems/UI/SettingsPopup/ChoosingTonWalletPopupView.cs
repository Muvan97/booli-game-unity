using Cysharp.Threading.Tasks;
using Infrastructure.Services.StaticData;
using Logic.Observers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.UI.SettingsPopup
{
    public class ChoosingTonWalletPopupView : BasePopup
    {
        public Button CloseButton => closeButton;
        [field: SerializeField] public Transform WalletsParent  { get; private set; }
    }
}