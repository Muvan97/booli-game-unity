using System;
using System.Collections.Generic;
using System.Threading;
using Systems.UI.SettingsPopup;
using Configs;
using Cysharp.Threading.Tasks;
using Data;
using Infrastructure.Services;
using Infrastructure.Services.StaticData;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestApiSystem;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Tools
{
    public static class DownloadTools
    {
        public static async UniTask<Texture> DownloadAvatarTexture(string url)
        {
            var request = UnityWebRequestTexture.GetTexture(url);
            await request.SendWebRequest();

            return request.result != UnityWebRequest.Result.Success
                ? null
                : ((DownloadHandlerTexture) request.downloadHandler).texture;
        }
        
        public static async UniTask<UserData> DownloadUserData(string id, RestApiMediatorService restApiMediatorService)
        {
            var fields = new Dictionary<string, string>
            {
                {RequestFieldNames.Identifier, id},
                {RequestFieldNames.IsGetOnlyAvatarAndNickname, "true"}
            };
            
            var result = await restApiMediatorService.GetProcessedRequestResult(Request.Post, fields);
            
            var avatarData = Convert.FromBase64String(result["Avatar"]);
            var avatarTexture = new Texture2D(0, 0);
            avatarTexture.LoadImage(avatarData);
            
            return new UserData(avatarTexture, result["Nickname"]);
        }
    }
}