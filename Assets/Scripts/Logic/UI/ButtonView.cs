using System;
using UnityEngine;
using UnityEngine.UI;

namespace Logic.UI
{
    [Serializable]
    public class ButtonView
    {
        [field: SerializeField] public Button Button { get; private set; }
        [SerializeField] private Sprite _defaultSprite;
        private float _defaultPixelPerUnitMultiplier;

        public void Initialize()
        {
            if (!_defaultSprite)
                _defaultSprite = Button.image.sprite;
            _defaultPixelPerUnitMultiplier = (float) Button.image?.pixelsPerUnitMultiplier;
        }
        public void ChangeSprite(Sprite sprite, float pixelPerUnitMultiplier)
        {
            Button.image.sprite = sprite;
            Button.image.pixelsPerUnitMultiplier = pixelPerUnitMultiplier;
        }

        public void ReturnDefaultSprite() => ChangeSprite(_defaultSprite, _defaultPixelPerUnitMultiplier);
    }
}