using System;
using Logic.Observers;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Tools
{
    public static class SubscribeTools
    {
        public static void SubscribeToLocalizationChanged(Action<Locale> actions, DestroyReporter destroyReporter)
        {
            LocalizationSettings.SelectedLocaleChanged += actions;
            destroyReporter.Destroyed += () => LocalizationSettings.SelectedLocaleChanged -= actions;
        }
    }
}