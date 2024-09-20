using System;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.UI.SettingsPopup.AudioMixerVolume
{
    public class AudioMixerVolumeView : MonoBehaviour
    {
        public Action<bool> ApplicationFocused;
        [field: SerializeField] public Toggle Toggle { get; private set; }
        [field: SerializeField] public AudioMixers MixerType { get; private set; }

        private void OnApplicationFocus(bool hasFocus) => ApplicationFocused?.Invoke(hasFocus);
    }
}