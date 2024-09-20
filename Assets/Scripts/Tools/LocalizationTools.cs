using Cysharp.Threading.Tasks;
using Holders;
using UnityEngine.Localization.Settings;

namespace Tools
{
    public static class LocalizationTools
    {
        public static async UniTask<string> GetLocalizedString(string key, LocalizationTable table)
            => await LocalizationSettings.StringDatabase.GetLocalizedStringAsync(table.ToString(), key);
    }
}