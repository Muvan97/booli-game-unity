using System.Collections.Generic;
using System.Linq;
using Systems.Tower.Configs;
using Systems.UI.UpgradeTowerPopup.UpgradeTowerButton;
using Cysharp.Threading.Tasks;
using Data;
using Infrastructure.Services.StaticData;
using UnityEngine;

namespace Infrastructure.Services.SaveLoad
{
    public abstract class LoadingDataService : IService
    {
        protected readonly IStaticDataProviderService staticDataService;
        protected readonly GameDataProviderAndSaverService gameDataProviderAndSaverService;
        protected readonly CurrenciesProviderService currenciesProviderService;

        protected LoadingDataService(IStaticDataProviderService staticDataService,
            GameDataProviderAndSaverService gameDataProviderAndSaverService,
            CurrenciesProviderService currenciesProviderService)
        {
            this.staticDataService = staticDataService;
            this.gameDataProviderAndSaverService = gameDataProviderAndSaverService;
            this.currenciesProviderService = currenciesProviderService;
        }

        public async UniTask LoadData()
        {
            await LoadGameData();

            var isNewPlayer = gameDataProviderAndSaverService.GameData == null;

            if (isNewPlayer)
                gameDataProviderAndSaverService.UpdateGameData(new GameData());

            var gameData = gameDataProviderAndSaverService.GameData;

            ValidateUpgradeTowerData(gameData);
            currenciesProviderService.InitializeCurrencies();

            if (isNewPlayer)
            {
                currenciesProviderService.Coins.Number += staticDataService.GameConfig.StartBalance;
                gameDataProviderAndSaverService.SaveData();
            }
        }

        private void ValidateUpgradeTowerData(GameData gameData)
        {
            var configs = staticDataService.GameConfig.TowersConfig.Configs;

            if (configs.Count == 0)
            {
                InitializeData(gameData, configs);
                return;
            }

            var towerUpgradeData = gameDataProviderAndSaverService.GameData.TowerUpgradeData;
            var allDataIndexes = towerUpgradeData.Select(data => data.TowerIndex);
            var allConfigsIndexes = configs.Select(config => config.TowerIndex);

            AddMissingData(gameData, allConfigsIndexes, allDataIndexes);
            RemoveExtraData(gameData, allDataIndexes, allConfigsIndexes);

            gameData.TowerUpgradeData = gameData.TowerUpgradeData.OrderBy(data => data.TowerIndex).ToList();
        }

        private void InitializeData(GameData gameData, List<TowerConfig> configs)
        {
            foreach (var towerConfig in configs)
            {
                var data = new UpgradeTowerData(towerConfig.TowerIndex);
                gameData.TowerUpgradeData.Add(data);
            }
        }

        private void AddMissingData(GameData gameData, IEnumerable<int> allConfigsIndexes,
            IEnumerable<int> allDataIndexes)
        {
            var missingDataIndexes = allConfigsIndexes.Except(allDataIndexes);
            
            foreach (var missingDataIndex in missingDataIndexes)
            {
                var data = new UpgradeTowerData(missingDataIndex);
                gameData.TowerUpgradeData.Add(data);
            }
        }

        private void RemoveExtraData(GameData gameData, IEnumerable<int> allDataIndexes,
            IEnumerable<int> allConfigsIndexes)
        {
            var extraDataIndexes = allDataIndexes.Except(allConfigsIndexes);
            gameData.TowerUpgradeData.RemoveAll(upgradeData => extraDataIndexes.Contains(upgradeData.TowerIndex));

            gameDataProviderAndSaverService.GameData.TowerUpgradeData =
                gameDataProviderAndSaverService.GameData.TowerUpgradeData.Distinct().ToList();
        }

        protected abstract UniTask LoadGameData();
    }
}