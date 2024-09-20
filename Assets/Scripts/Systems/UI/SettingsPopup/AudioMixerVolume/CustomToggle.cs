using UnityEngine;
using UnityEngine.UI;

namespace Systems.UI.SettingsPopup.AudioMixerVolume
{
    public class CustomToggle : MonoBehaviour
    {
        [SerializeField] private Sprite _spriteOnEnabled, _spriteOnDisabled;
        [SerializeField] private Toggle _toggle;
        [SerializeField] private Image _image;

        private void Start()
        {
            ChangeSprite(_toggle.isOn);
            _toggle.onValueChanged.AddListener(ChangeSprite);
        }

        private void ChangeSprite(bool isOn)
        {
            _image.sprite = isOn
                ? _spriteOnEnabled
                : _spriteOnDisabled;
        }
    }
}