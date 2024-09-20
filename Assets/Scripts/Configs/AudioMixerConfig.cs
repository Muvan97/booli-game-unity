using System;
using Systems.UI.SettingsPopup.AudioMixerVolume;
using UnityEngine;
using UnityEngine.Audio;

namespace Configs
{
    [Serializable]
    public class AudioMixerConfig
    {
        [field: SerializeField] public AudioMixer AudioMixer { get; private set; }
        [field: SerializeField] public AudioMixers AudioMixerType { get; private set; }
    }
}