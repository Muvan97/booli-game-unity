using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Systems.UI.SettingsPopup.AudioMixerVolume
{
    public class AudioMixerVolumeModel
    {
        public readonly AudioMixer AudioMixer;
        public bool IsOn = true;
        public readonly string NameVariableSliderInPlayerPref;

        public AudioMixerVolumeModel(AudioMixer audioMixer, AudioMixers audioMixerType)
        {
            NameVariableSliderInPlayerPref = audioMixerType.ToString();
            AudioMixer = audioMixer;
        }

        public void SaveValueSliderInPlayerPref() => PlayerPrefs.SetInt(NameVariableSliderInPlayerPref, Convert.ToInt32(IsOn));
    }
}