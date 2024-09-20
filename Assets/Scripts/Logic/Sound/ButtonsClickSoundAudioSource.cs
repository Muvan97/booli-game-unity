using Configs;
using Tools;
using UnityEngine;

namespace Logic.Sound
{
    [RequireComponent(typeof(AudioSource))]
    public class ButtonsClickSoundAudioSource : MonoBehaviour
    {
        private AudioSource _audioSource;

        public void Construct(GameConfig gameConfig)
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.clip = gameConfig.SoundsConfig.ClickButtonSound;
        }

        public void SubscribeToButtons()
        {
            var buttons = UITools.FindAllButtons();
            buttons.ForEach(button => button.onClick.AddListener(_audioSource.Play));
        }
    }
}