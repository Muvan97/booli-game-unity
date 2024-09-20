using System;
using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    [Serializable]
    public struct SoundsConfig
    {
        [field: SerializeField] public List<AudioClip> MainMenuMusicClips { get; private set; }
        [field: SerializeField] public List<AudioClip> LevelMusicClips { get; private set; }
        [field: SerializeField] public AudioClip ClickButtonSound { get; private set; }
        [field: SerializeField] public AudioClip EnemyDeathSound { get; private set; }
    }
}