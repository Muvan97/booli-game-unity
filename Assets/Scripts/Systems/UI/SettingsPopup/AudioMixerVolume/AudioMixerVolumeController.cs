using System;
using UnityEngine;

namespace Systems.UI.SettingsPopup.AudioMixerVolume
{
    public enum AudioMixers
    {
        Music,
        Sound
    }

    public class AudioMixerVolumeController
    {
        private readonly AudioMixerVolumeModel _model;
        private readonly AudioMixerVolumeView _view;
        
        private const string VolumeKey = "volume";
        private const float VolumeMultiplier = 20;
        private const int MaximumVolumeValue = 1;
        private const float MinimumVolumeValue = 0.0000001f;

        public AudioMixerVolumeController(AudioMixerVolumeModel model, AudioMixerVolumeView view)
        {
            _model = model;
            _view = view;
            
            Initialize();
        }

        private void Initialize()
        {
            if (!PlayerPrefs.HasKey(_model.NameVariableSliderInPlayerPref))
                _model.SaveValueSliderInPlayerPref();

            _view.Toggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt(_model.NameVariableSliderInPlayerPref));
            _view.Toggle.onValueChanged.AddListener(SetVolumeAndSave);
            SetVolumeInMixer(_view.Toggle.isOn);

            _view.ApplicationFocused += OnApplicationFocus;
        }

        private void SetVolumeInMixer(bool isOn)
        {
            _model.IsOn = isOn;
            _model.AudioMixer.SetFloat(VolumeKey, Mathf.Log10(_model.IsOn ? MaximumVolumeValue : MinimumVolumeValue) * VolumeMultiplier);
        }

        private void OnApplicationFocus(bool hasFocus) => SetVolumeInMixer(hasFocus && _model.IsOn);

        private void SetVolumeAndSave(bool isOn)
        {
            SetVolumeInMixer(isOn);
            _model.SaveValueSliderInPlayerPref();
        }
    }
}