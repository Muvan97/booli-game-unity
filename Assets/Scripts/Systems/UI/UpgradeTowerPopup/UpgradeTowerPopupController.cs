using System.Collections.Generic;
using System.Linq;
using Systems.Tower.Configs;
using Systems.UI.UpgradeTowerPopup.UpgradeTowerButton;
using Configs;
using Data;
using Infrastructure.Factory;
using Infrastructure.Services;
using Tools;

namespace Systems.UI.UpgradeTowerPopup
{
    public class UpgradeTowerPopupController
    {
        public UpgradeTowerPopupController(IUIFactory uiFactory, GameConfig gameConfig, 
            GameDataProviderAndSaverService gameDataProviderAndSaverService,
            CurrenciesProviderService currenciesProviderService,
            UpgradeTowerPopupView view, UpgradeTowerPopupModel model)
        {
            UITools.InitializeCoinsCounterView(view.CoinsCounterView, currenciesProviderService.Coins);
            
            CreateUpgradeTowersButtonsViewsAndControllers(uiFactory, gameConfig, view, 
                gameDataProviderAndSaverService, currenciesProviderService, model);
        }

        private void CreateUpgradeTowersButtonsViewsAndControllers(IUIFactory uiFactory, GameConfig gameConfig,
            UpgradeTowerPopupView towerPopupView, GameDataProviderAndSaverService gameDataProviderAndSaverService,
            CurrenciesProviderService currenciesProviderService, UpgradeTowerPopupModel model)
        {
            foreach (var towerConfig in gameConfig.TowersConfig.Configs)
            {
                var view = uiFactory.GetUpgradeTowerButtonView(towerPopupView.BattonsParent);

                var towerUpgradeData = gameDataProviderAndSaverService.GameData.TowerUpgradeData.Find(data => data.TowerIndex == towerConfig.TowerIndex);

                if (towerUpgradeData == null)
                    continue;

                List<TowerLevelConfig> towerLevelConfigs;

                switch (towerConfig)
                {
                    case NotBomberTowerConfig notBomberTowerConfig:
                        towerLevelConfigs = notBomberTowerConfig.TowerConfigs;
                        break;
                    case BomberTowerConfig bomberTowerConfig:
                        towerLevelConfigs = bomberTowerConfig.TowerConfigs.Cast<TowerLevelConfig>().ToList();
                        break;
                    default:
                        continue;
                }

                var buttonModel = new UpgradeTowerButtonModel(towerLevelConfigs,
                    towerUpgradeData, gameDataProviderAndSaverService, towerConfig, gameConfig, currenciesProviderService);

                buttonModel.TowerUpgraded += () => model.TowerUpgraded?.Invoke();
                
                var controller = new UpgradeTowerButtonController(buttonModel, view);
            }
        }
    }
}