using System;
using Systems.Tower.Configs;
using Infrastructure.Services;

namespace Systems.UI.BuildingTowerPopup.BuildingButton
{
    public class BuildingButtonModel
    {
        public Action<TowerConfig, BuildingButtonView> BuildingButtonClicked;
        public readonly BuildingButtonConfig Config;

        public BuildingButtonModel(BuildingButtonConfig config) => Config = config;
    }
}