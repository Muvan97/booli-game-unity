using System;
using System.Collections.Generic;
using UnityEngine;

namespace Logic.UI
{
    [Serializable]
    public class TargetButtonChanger
    {
        public ButtonView TargetButton { get; private set; } 
        [SerializeField] private List<ButtonView> _buttons;
        [SerializeField] private Sprite _targetButtonSprite;
        [SerializeField] private float _targetButtonPixelPerMultiplier;

        public void Initialize()
        {
            foreach (var buttonView in _buttons)
            {
                buttonView.Button.onClick.AddListener(() => ChangeTargetButton(buttonView));
                buttonView.Initialize();
            }

            if (_buttons.Count > 0)
                ChangeTargetButton(_buttons[0]);
        }

        private void ChangeTargetButton(ButtonView buttonView)
        {
            TargetButton = buttonView;
            _buttons.ForEach(button => button.ReturnDefaultSprite());
            buttonView.ChangeSprite(_targetButtonSprite, _targetButtonPixelPerMultiplier);
        }
    }
}