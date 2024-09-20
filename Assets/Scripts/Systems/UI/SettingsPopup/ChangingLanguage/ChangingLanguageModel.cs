using System.Collections.Generic;
using Holders;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Systems.UI.SettingsPopup.ChangingLanguage
{
    public class ChangingLanguageModel
    {
        public Locale SelectedLocale => LocalizationSettings.SelectedLocale;
        public List<Locale> AvailableLocales => LocalizationSettings.AvailableLocales.Locales;
        public void SelectAndSaveLocale(int index)
        {
            LocalizationSettings.SelectedLocale = AvailableLocales[index];
            SaveLocale(index);
        }

        private void SaveLocale(int index) => PlayerPrefs.SetInt(VariablesNamesPlayerPrefHolder.SelectedLocaleIndex, index);
    }
}