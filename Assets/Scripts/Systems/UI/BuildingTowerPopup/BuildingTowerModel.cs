using Systems.Tower;
using Systems.Tower.Configs;
using Systems.Tower.TowerPlace;
using Systems.UI.BuildingTowerPopup.BuildingButton;
using Configs;
using Data;
using Infrastructure.Factory;
using Infrastructure.Services;

namespace Systems.UI.BuildingTowerPopup
{
    public class BuildingTowerModel
    {
        public TowerPlaceView LastTowerPlaceView;
        public TowerComponents CurrentUnbuiltTowerInstance;
        public TowerConfig CurrentUnbuiltTowerConfig;
        public BuildingButtonView LastClickedButtonView;
        public readonly GameConfig GameConfig;
        public readonly IUIFactory UIFactory;
        public readonly GameData GameData;
        public readonly IGameFactory GameFactory;

        public BuildingTowerModel(GameConfig gameConfig, IUIFactory uiFactory, IGameFactory gameFactory,
           GameData gameData)
        {
            GameData = gameData;
            GameConfig = gameConfig;
            UIFactory = uiFactory;
            GameFactory = gameFactory;
        }
    }
}