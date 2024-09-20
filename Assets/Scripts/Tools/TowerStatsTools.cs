using System.Collections.Generic;
using System.Linq;
using Systems.Tower.Configs;
using Systems.UI.UpgradeTowerPopup.UpgradeTowerButton;
using UnityEngine;

namespace Tools
{
    public static class TowerConfigTools
    {
        public static List<TowerLevelConfig> GetTowerLevelConfigs(this TowerConfig towerConfig)
        {
            switch (towerConfig)
            {
                case BomberTowerConfig bomberTowerConfig:
                    return new List<TowerLevelConfig>(bomberTowerConfig.TowerConfigs);
                case NotBomberTowerConfig notBomberTowerConfig:
                    return notBomberTowerConfig.TowerConfigs;
                default:
                    return null;
            }
        }

        public static int GetLevel(this TowerConfig towerConfig, List<UpgradeTowerData> upgradeTowerData)
            => upgradeTowerData.Find(data => data.TowerIndex == towerConfig.TowerIndex).Level;

        public static TowerLevelConfig GetCurrentTowerLevelConfig(this TowerConfig config, int level) 
            =>  config.GetTowerLevelConfigs().FindAll(levelConfig => levelConfig.MinimumLevel <= level).Max();
        
        public static TowerLevelConfig GetCurrentTowerLevelConfig(this TowerConfig config, List<UpgradeTowerData> upgradeTowerData)
        {
            var level = config.GetLevel(upgradeTowerData);
            return config.GetTowerLevelConfigs().FindAll(levelConfig => levelConfig.MinimumLevel <= level).Max();
        }

        public static TowerLevelConfig GetCurrentTowerLevelConfig(this List<TowerLevelConfig> configs, int level)
            => configs.FindAll(levelConfig => levelConfig.MinimumLevel <= level).Max();
    }
    public static class TowerStatsTools
    {
        public static float GetCooldown(List<TowerLevelConfig> towerLevelConfigs, int level, float startCooldown, float reducingCooldownValuePerOpenedLevelConfig)
        {
            var openedLevelConfigs = towerLevelConfigs.FindAll(config => config.MinimumLevel <= level).Count;
            
            return startCooldown - (openedLevelConfigs - 1) * reducingCooldownValuePerOpenedLevelConfig;
        }
        
        public static float GetDamage(int level, float startDamage, float addingValuePerLevel)
        {
            return startDamage + (Mathf.Max(level, 1) - 1) * addingValuePerLevel;
        }
    }
}