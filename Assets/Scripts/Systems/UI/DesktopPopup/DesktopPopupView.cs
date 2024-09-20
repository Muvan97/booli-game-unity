using Logic.Observers;
using Logic.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.UI.DesktopPopup
{
    [RequireComponent(typeof(DestroyReporter))]
    public class DesktopPopupView : BasePopup
    {
        [field: SerializeField] public DestroyReporter DestroyReporter { get; private set; }
        [field: SerializeField] public CounterView CoinsCounterView { get; private set; }
        [field: SerializeField] public CounterView BooliCounterView { get; private set; }
        [field: SerializeField] public TMP_Text UserShortNicknameText { get; private set; }
        [field: SerializeField] public Image BuyTowerWarningImage { get; private set; }
        [field: SerializeField] public TMP_Text WaveText { get; private set; }
        [field: SerializeField] public RawImage UserAvatarRawImage { get; private set; }
        [field: SerializeField] public Button PlayButton { get; private set; }
        [field: SerializeField] public Button OpenSettingsButton { get; private set; }
        [field: SerializeField] public Button OpenFriendsPopupButton { get; private set; }
        [field: SerializeField] public Button OpenTaskPopupButton { get; private set; }
    }
}