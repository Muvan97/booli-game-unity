using System.Collections.Generic;
using System.Linq;
using Infrastructure.Factory;
using UnityEngine.UI;

namespace Systems.UI.MainMenuWindow
{
    public class MainMenuWindowController
    {
        public MainMenuWindowController(MainMenuWindowView windowView, IUIFactory uiFactory) =>
            Subscribe(windowView, uiFactory);

        private void Subscribe(MainMenuWindowView windowView, IUIFactory uiFactory)
        {
            var openPopupButtons = new Dictionary<Button, BasePopup>
                {
                    {windowView.OpenTowerUpgradePopupButton, uiFactory.GetUpgradeTowerPopup()}, 
                    {windowView.OpenShopPopupButton, uiFactory.GetShopPopup()}, 
                    {windowView.OpenDesktopPopupButton, uiFactory.GetDesktopPopup()}
                };
            
            var popups = openPopupButtons.Values.ToList();

            foreach (var openPopupButton in openPopupButtons)
                openPopupButton.Key.onClick.AddListener(() => OnClickOpenButton(popups, openPopupButton.Value));
        }

        private void OnClickOpenButton(List<BasePopup> allPopups, BasePopup popupForOpen)
        {
            popupForOpen.OpenPopup();
            //allPopups.Where(popup => popup != popupForOpen).ToList().ForEach(popup => popup.ClosePopup());
        }
    }
}