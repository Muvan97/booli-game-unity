using Systems.UI.BuildingTowerPopup;
using Configs;
using Infrastructure.Factory;
using Infrastructure.Services;
using Tools;
using UnityEngine;

namespace Systems.Level.LevelPopup
{
    public class LevelPopupModel
    {
        public readonly CurrenciesProviderService CurrenciesProviderService;
        public readonly GameConfig GameConfig;
        public readonly GameDataProviderAndSaverService GameDataProviderAndSaverService;
        public readonly IUIFactory UIFactory;
        public readonly Vector3 LowerElementsParentStartPosition;
        public readonly Vector3 LowerElementsPositionOnBuildingPopupOpen;

        public LevelPopupModel(CurrenciesProviderService currenciesProviderService, 
            GameDataProviderAndSaverService gameDataProviderAndSaverService,
            GameConfig gameConfig, IUIFactory uiFactory, LevelPopupView levelPopupView,
            BuildingTowerPopupView buildingTowerPopupView)
        {
            LowerElementsParentStartPosition = levelPopupView.LowerElementsParent.transform.position;
            CurrenciesProviderService = currenciesProviderService;
            GameDataProviderAndSaverService = gameDataProviderAndSaverService;
            GameConfig = gameConfig;
            UIFactory = uiFactory;

            LowerElementsPositionOnBuildingPopupOpen = LowerElementsParentStartPosition +
                                                       Vector3.up * UITools.GetRectWorldSize(buildingTowerPopupView.BoardTransform)
                                                           .y;
        }

    }
}