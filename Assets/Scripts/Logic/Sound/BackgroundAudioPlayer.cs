using System.Collections.Generic;
using System.Linq;
using Logic.Other;
using UnityEngine;

namespace Logic.Sound
{
    [RequireComponent(typeof(AudioSource))]
    public class BackgroundAudioPlayer : MonoBehaviour
    {
        public List<AudioClip> Clips { get; private set; }
        private AudioSource _audioSource;
        private Timer _timer;
        
        private int _currentClipIndex = -1;
        public void ConstructAndStartPlaySounds(List<AudioClip> clips)
        {
            _audioSource = GetComponent<AudioSource>();
            _timer = new Timer(destroyCancellationToken);
            _timer.Ended += TryPlayNextSound;
            Clips = clips.Where(clip => clip != null).ToList();
            TryPlayNextSound();
        }

        public void SetAudioClipsAndTryPlayNextSound(List<AudioClip> clips)
        {
            Clips = clips;
            TryPlayNextSound();
        }

        private void TryPlayNextSound()
        {
            if (Clips.Count == 0)
            {
                _audioSource.Stop();
                return;
            }
            
            UpdateCurrentClipIndex();

            var clip = Clips[_currentClipIndex];
            _audioSource.clip = clip;
            _audioSource.Play();
            _timer.StartCountingTime(clip.length);
        }

        private void UpdateCurrentClipIndex()
        {
            if (_currentClipIndex + 1 == Clips.Count)
                _currentClipIndex = 0;
            else
                _currentClipIndex++;
        }
    }
}