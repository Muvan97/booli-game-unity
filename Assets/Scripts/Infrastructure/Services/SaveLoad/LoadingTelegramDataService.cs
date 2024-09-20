using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Data;
using Infrastructure.Factory;
using Infrastructure.Services.StaticData;
using Logic.UI;
using Newtonsoft.Json;
using RestApiSystem;
using Tools;
using UnityEngine;
using UnityEngine.Networking;

namespace Infrastructure.Services.SaveLoad
{
    public class LoadingTelegramDataService : LoadingDataService
    {
        private readonly IGameFactory _gameFactory;
        private readonly RestApiMediatorService _restApiMediatorService;

        public LoadingTelegramDataService(IStaticDataProviderService staticDataService,
            GameDataProviderAndSaverService gameDataProviderAndSaverService,
            CurrenciesProviderService currenciesProviderService, IGameFactory gameFactory,
            RestApiMediatorService restApiMediatorService)
            : base(staticDataService, gameDataProviderAndSaverService, currenciesProviderService)
        {
            _gameFactory = gameFactory;
            _restApiMediatorService = restApiMediatorService;
        }

        protected override async UniTask LoadGameData()
        {
            var observer = _gameFactory.FindTelegramDataObserver();

            await UniTask.WaitUntil(() => !observer.ID.IsNullOrEmptyOrWhitespace() && !observer.RefferalPlayerID.IsNullOrEmptyOrWhitespace()
            && !observer.Nickname.IsNullOrEmptyOrWhitespace());

            gameDataProviderAndSaverService.PlayerID = observer.ID;
            gameDataProviderAndSaverService.RefferalPlayerID = observer.RefferalPlayerID;
            gameDataProviderAndSaverService.PlayerNickname = observer.Nickname;

            var rewardsConfig = staticDataService.GameConfig.RewardsForRefferalsConfig;
            var fields = new Dictionary<string, string>
            {
                {RequestFieldNames.Identifier, gameDataProviderAndSaverService.PlayerID},
                {RequestFieldNames.BonusInBooliForRefferal, rewardsConfig.BonusInBooli.ToString()},
                {RequestFieldNames.BonusInCoinsForRefferal, rewardsConfig.BonusInCoins.ToString()},
                {RequestFieldNames.NumberOpenedLevelsForGiveBonusForRefferal, rewardsConfig
                    .NumberOpenedLevelsForGiveBonusForRefferal.ToString()},
                {
                    RequestFieldNames.PlayerAccountData, JsonConvert.SerializeObject(new Dictionary<string, string>
                    {
                        {RequestFieldNames.RefferalIdentifier, gameDataProviderAndSaverService.RefferalPlayerID},
                        {RequestFieldNames.GameData, JsonConvert.SerializeObject(new GameData())}
                    })
                }
            };

            var result = await _restApiMediatorService.GetProcessedRequestResult(Request.Post, fields);

            if (result.TryGetValue(RestApiMediatorKeys.IsPlayerSentBooli, out var isPlayerSentBooli))
                gameDataProviderAndSaverService.IsPlayerSentBooli = bool.Parse(isPlayerSentBooli);

            if (result.TryGetValue(RestApiMediatorKeys.GameData, out var gameDataString) && gameDataString != null)
                gameDataProviderAndSaverService.UpdateGameData(JsonConvert.DeserializeObject<GameData>(gameDataString));

            if (result.TryGetValue(RestApiMediatorKeys.InvitedPlayers, out var invitedPlayersString) &&
                invitedPlayersString != null)
            {
                gameDataProviderAndSaverService.InvitedPlayers = JsonConvert.DeserializeObject<List<InvitedUserData>>(invitedPlayersString);
            }
        }
        
    }
}