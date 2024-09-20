using Configs;
using Infrastructure.Factory;
using Infrastructure.Services;
using RestApiSystem;

namespace Systems.UI.FriendsPopup
{
    public class FriendsPopupModel
    {
        public string ShareLink => $"https://t.me/share?url={RefferalLink}";
        private string RefferalLink => $"http://t.me/TDBooli_bot/BooliTD?startapp={GameDataProviderAndSaverService.PlayerID}";
        public readonly IUIFactory UIFactory;
        public readonly GameConfig GameConfig;
        public readonly GameDataProviderAndSaverService GameDataProviderAndSaverService;
        public readonly RestApiMediatorService RestApiMediatorService;

        public FriendsPopupModel(GameDataProviderAndSaverService gameDataProviderAndSaverService, IUIFactory uiFactory, 
            RestApiMediatorService restApiMediatorService, GameConfig gameConfig)
        {
            GameDataProviderAndSaverService = gameDataProviderAndSaverService;
            UIFactory = uiFactory;
            RestApiMediatorService = restApiMediatorService;
            GameConfig = gameConfig;
        }
    }
}