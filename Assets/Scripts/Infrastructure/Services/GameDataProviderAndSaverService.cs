using System;
using System.Collections.Generic;
using System.IO;
using Configs;
using Cysharp.Threading.Tasks;
using Data;
using Infrastructure.Services.StaticData;
using Newtonsoft.Json;
using RestApiSystem;
using Tools;
using UnityEngine;

namespace Infrastructure.Services
{
    public class GameDataProviderAndSaverService : IService
    {
        public Texture PlayerAvatarTexture { get; private set; }
        public GameData GameData { get; private set; }

        public List<InvitedUserData> InvitedPlayers = new List<InvitedUserData>();
        public string PlayerID;
        public string RefferalPlayerID;
        public string PlayerNickname;
        public bool IsPlayerSentBooli;
        private readonly IStaticDataProviderService _staticDataProviderService;
        private readonly RestApiMediatorService _restApiMediatorService;

        private static string FilePath => Application.dataPath + "/Saves/GameData.json";

        public GameDataProviderAndSaverService(IStaticDataProviderService staticDataProviderService, 
            RestApiMediatorService restApiMediatorService)
        {
            _staticDataProviderService = staticDataProviderService;
            _restApiMediatorService = restApiMediatorService;
        }

        public async UniTask TryDownloadPlayerAvatar()
        {
            if (PlayerAvatarTexture != null)
                return;
            
            var userData = await DownloadTools.DownloadUserData(PlayerID, _restApiMediatorService);
            PlayerAvatarTexture = userData.AvatarTexture;
        }

        public void UpdateGameData(GameData gameData) => GameData = gameData;

        public void SaveData()
        {
            switch (_staticDataProviderService.GameConfig.LoadSDKType)
            {
                case SDK.Yandex:
                    //YandexGame.SaveProgress();
                    break;
                case SDK.Local:
                    File.WriteAllText(FilePath, JsonUtility.ToJson(GameData));
                    break;
                case SDK.Telegram:
                    SaveTelegramData();
                    break;
            }
        }

        private void SaveTelegramData()
        {
            var fields = new Dictionary<string, string>
            {
                {RequestFieldNames.PlayerAccountData, JsonConvert.SerializeObject(new Dictionary<string, string> {{
                            RequestFieldNames.GameData, JsonConvert.SerializeObject(GameData)}})},
                {RequestFieldNames.Identifier, PlayerID},
                {RequestFieldNames.IsOnlySaveData, "true"}
            };
            
            _restApiMediatorService.GetProcessedRequestResult(Request.Post, fields).Forget();
        }
    }
}