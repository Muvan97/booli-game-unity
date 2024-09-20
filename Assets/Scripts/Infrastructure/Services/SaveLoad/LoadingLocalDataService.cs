using System.IO;
using Cysharp.Threading.Tasks;
using Data;
using Infrastructure.Services.StaticData;
using UnityEngine;

namespace Infrastructure.Services.SaveLoad
{
    public class LoadingLocalDataService : LoadingDataService
    {
        private static string FilePath => Application.dataPath + "/Saves/GameData.json";

        public LoadingLocalDataService(IStaticDataProviderService staticDataService, GameDataProviderAndSaverService gameDataProviderAndSaverService, 
            CurrenciesProviderService currenciesProviderService) : base(staticDataService, gameDataProviderAndSaverService, currenciesProviderService)
        {
        }

        protected override UniTask LoadGameData()
        {
            if (!File.Exists(FilePath) || JsonUtility.FromJson<GameData>(File.ReadAllText(FilePath)) == null)
            {
                var directory = Path.GetDirectoryName(FilePath);

                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                File.WriteAllText(FilePath, JsonUtility.ToJson(new GameData()));
            }

            gameDataProviderAndSaverService.UpdateGameData(JsonUtility.FromJson<GameData>(File.ReadAllText(FilePath)));
            return UniTask.CompletedTask;
        }
    }
}