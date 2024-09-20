using System;
using System.Collections.Generic;
using Systems.UI.UpgradeTowerPopup.UpgradeTowerButton;
using Infrastructure.Services.SaveLoad;

namespace Data
{
    [Serializable]
    public class GameData
    {
        public List<UpgradeTowerData> TowerUpgradeData = new List<UpgradeTowerData>();
        public CurrenciesData CurrenciesData = new CurrenciesData();
        public int OpenLevelIndex;
    }
}