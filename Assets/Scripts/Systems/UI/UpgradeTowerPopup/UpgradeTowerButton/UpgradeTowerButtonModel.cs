using System;
using System.Collections.Generic;
using System.Linq;
using Systems.Tower.Configs;
using Configs;
using Infrastructure.Services;
using Tools;

namespace Systems.UI.UpgradeTowerPopup.UpgradeTowerButton
{
    public class UpgradeTowerButtonModel
    {
        public Action TowerUpgraded;
        
        public readonly UpgradeTowerData UpgradeTowerData;
        public readonly List<TowerLevelConfig> Configs;
        public readonly GameDataProviderAndSaverService GameDataProviderAndSaverService;
        public readonly CurrenciesProviderService CurrenciesProviderService;
        public readonly TowerConfig TowerConfig;

        private readonly GameConfig _gameConfig;


        public UpgradeTowerButtonModel(List<TowerLevelConfig> configs, UpgradeTowerData upgradeTowerData,
            GameDataProviderAndSaverService gameDataProviderAndSaverService, TowerConfig towerConfig, GameConfig gameConfig, 
            CurrenciesProviderService currenciesProviderService)
        {
            Configs = configs;
            UpgradeTowerData = upgradeTowerData;
            GameDataProviderAndSaverService = gameDataProviderAndSaverService;
            TowerConfig = towerConfig;
            _gameConfig = gameConfig;
            CurrenciesProviderService = currenciesProviderService;
        }

        public bool IsCanUpgradeTower(out decimal towerUpgradePrice)
        {
            towerUpgradePrice = GetTowerUpgradePrice();
            return towerUpgradePrice <= CurrenciesProviderService.Coins.Number;
        }

        public TowerLevelConfig GetCurrentTowerLevelConfig() => Configs.GetCurrentTowerLevelConfig(UpgradeTowerData.Level);
        public float GetCurrentTowerLevelDamage() => GetTowerLevelDamage(UpgradeTowerData.Level);
        public float GetNextTowerLevelDamage() => GetTowerLevelDamage(UpgradeTowerData.Level + 1);

        public float GetCurrentTowerLevelCooldown() => GetTowerLevelCooldown(UpgradeTowerData.Level);
        public float GetNextTowerLevelCooldown() => GetTowerLevelCooldown(UpgradeTowerData.Level + 1);

        public bool IsHasMaxLevel() => UpgradeTowerData.Level >= Configs.Select(config => config.MinimumLevel).Max();


        private float GetTowerLevelDamage(int level) =>
            TowerStatsTools.GetDamage(level,
                TowerConfig.StartDamage, _gameConfig.TowersConfig.AddingDamagePerLevel);

        private float GetTowerLevelCooldown(int level)
            => TowerStatsTools.GetCooldown(Configs, level, TowerConfig.StartCooldown,
                _gameConfig.TowersConfig.ReducingCooldownValuePerOpenedLevelConfig);

        public decimal GetTowerUpgradePrice()
        {
            if (UpgradeTowerData.Level == 0)
                return TowerConfig.StartUpgradingPrice;
            
            return Math.Truncate((decimal)(TowerConfig.StartUpgradingPrice * 
                                      Math.Pow(_gameConfig.TowersConfig.UpgradeIncreasingPricePerLevelMultiplier, UpgradeTowerData.Level)));
        }
    }
}