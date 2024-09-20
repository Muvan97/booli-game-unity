using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Configs
{
    [Serializable]
    public class AnimationsConfigs
    {
        [field: SerializeField] public TextTakingOffAnimationConfig EarnedBooliTextAnimation { get; private set; }
    }

    public class TextTakingOffAnimation
    {
        public TextTakingOffAnimation(TMP_Text text, TextTakingOffAnimationConfig config) => StartAnimation(text, config);

        private void StartAnimation(TMP_Text text, TextTakingOffAnimationConfig config) 
            => text.transform.DOMoveY(text.transform.position.y + config.OffsetY, config.Duration);
    }

    [Serializable]
    public class TextTakingOffAnimationConfig
    {
        [field: SerializeField] public float Duration { get; private set; }
        [field: SerializeField] public float OffsetY { get; private set; }
    }
}