using System.Collections.Generic;
using Systems.UI.SettingsPopup.AudioMixerVolume;
using Systems.UI.SettingsPopup.ChangingLanguage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.UI.SettingsPopup
{
    public class SettingsPopupView : BasePopup
    {
        [field: SerializeField] public List<AudioMixerVolumeView> AudioMixerVolumeViews { get; private set; }
        [field: SerializeField] public ChangingLanguageView ChangingLanguageView { get; private set; }
        [field: SerializeField] public TMP_Text UserShortNicknameText { get; private set; }
        [field: SerializeField] public TMP_Text UserNicknameText { get; private set; }
        [field: SerializeField] public RawImage UserAvatarRawImage { get; private set; }
        [field: SerializeField] public Button WithdrawBooliButton { get; private set; }
        [field: SerializeField] public Button LinkOrUnlinkWalletButton { get; private set; }
        [field: SerializeField] public TMP_Text LinkOrUnlinkWalletText { get; private set; }
        [field: SerializeField] public TMP_Text ShortWalletAddressText { get; private set; }
    }
}