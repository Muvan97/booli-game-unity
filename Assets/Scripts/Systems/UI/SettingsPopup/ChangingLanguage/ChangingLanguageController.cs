using System.Collections.Generic;
using Holders;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.UI.SettingsPopup.ChangingLanguage
{
    public class ChangingLanguageController
    {
        private readonly ChangingLanguageModel _model;
        private const int EnglishLocaleIndex = 0;
        
        public ChangingLanguageController(ChangingLanguageView changingLanguageView, ChangingLanguageModel model)
        {
            _model = model;
            Initialize(changingLanguageView.Buttons);
        }

        private void Initialize(List<Button> buttons)
        {
            var selected = !PlayerPrefs.HasKey(VariablesNamesPlayerPrefHolder.SelectedLocaleIndex)
                ? GetUserDeviceLocaleIndex() 
                : PlayerPrefs.GetInt(VariablesNamesPlayerPrefHolder.SelectedLocaleIndex);
            
            _model.SelectAndSaveLocale(selected);

            for (var i = 0; i < buttons.Count; i++)
            {
                var button = buttons[i];
                var index = i;
                button.onClick.AddListener(() => _model.SelectAndSaveLocale(index));
            }
        }

        private int GetUserDeviceLocaleIndex()
        {
            var locale = _model.AvailableLocales.Find(locale => locale.Identifier == Application.systemLanguage);

            return !locale 
                ? EnglishLocaleIndex 
                : _model.AvailableLocales.IndexOf(locale);
        }
    }
}