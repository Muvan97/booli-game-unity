using Cysharp.Threading.Tasks;
using Data;
using Infrastructure.Services.StaticData;

namespace Infrastructure.Services.SaveLoad
{
    public class LoadingYandexDataService : LoadingDataService
    {
        public LoadingYandexDataService(IStaticDataProviderService staticDataService, GameDataProviderAndSaverService gameDataProviderAndSaverService, CurrenciesProviderService currenciesProviderService) 
            : base(staticDataService, gameDataProviderAndSaverService, currenciesProviderService)
        {
        }

        protected override async UniTask LoadGameData()
        {
            var isDataLoaded = false;
            //YandexGame.GetDataEvent += () => isDataLoaded = true;

            await UniTask.WaitUntil(() => isDataLoaded);
            //gameDataProviderAndSaverService.UpdateGameData(YandexGame.savesData.GameData ?? new GameData());

            //YandexGame.RewardVideoEvent += OnRewardVideoWatch;
        }

        private void OnRewardVideoWatch(int id)
        {
        }
    }
}