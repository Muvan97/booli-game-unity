using System.Collections.Generic;
using Systems.Enemy;
using Systems.Level.LevelPopup;
using Systems.Level.LevelWaves;
using Systems.UI;
using Systems.UI.BuildingTowerPopup;
using Systems.UI.BuildingTowerPopup.BuildingButton;
using Systems.UI.DefeatPopup;
using Systems.UI.DesktopPopup;
using Systems.UI.FriendsPopup;
using Systems.UI.MainMenuWindow;
using Systems.UI.SettingsPopup;
using Systems.UI.ShopPopup;
using Systems.UI.UpgradeTowerPopup;
using Systems.UI.UpgradeTowerPopup.UpgradeTowerButton;
using Systems.UI.Win;
using Infrastructure.Services;
using Infrastructure.States;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.Factory
{
    public interface IUIFactory : IService
    {
        TextMeshPro GetTextOverEnemy(Vector2 startPosition);
        MoneyCounterPopup GetMoneyCounterPopup();
        MainMenuWindowView GetMainMenuWindow();
        SettingsPopupView GetSettingsPopup(Button openButton);
        WinPopup GetWinPopup();
        LevelPopupView GetLevelPopup();
        BuildingButtonView GetBuildingButtonView(BuildingButtonConfig data, Transform parent);
        BuildingTowerPopupView GetBuildingTowerPopup();
        TaskPopup GetTaskPopup();
        ShopPopupView GetShopPopup();
        UpgradeTowerPopupView GetUpgradeTowerPopup();
        FriendsPopupView GetFriendsPopup();
        DesktopPopupView GetDesktopPopup();
        UpgradeTowerButtonView GetUpgradeTowerButtonView(Transform parent);
        FriendImageView GetFriendImageView(Transform parent);
        DefeatPopupView GetDefeatPopup(GameStateMachine gameStateMachine);
        TonWalletWindowView GetTonWalletWindowView(Transform parent);
    }
}