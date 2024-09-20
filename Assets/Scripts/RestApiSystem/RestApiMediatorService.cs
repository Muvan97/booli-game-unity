using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Systems.UI.UpgradeTowerPopup.UpgradeTowerButton;
using Cysharp.Threading.Tasks;
using Data;
using Infrastructure.Services;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace RestApiSystem
{
    public class RestApiMediatorService : IService
    {
        private string URL
        {
            get
            {
                #if UNITY_EDITOR
                return "https://rest.booli.fun";
                #else 
                return "https://rest.booli.fun"; //"http://127.0.0.1:5000";
                #endif
                
            }
        }

        public async UniTask<Dictionary<string, string>> GetProcessedRequestResult(Request requestType,
            Dictionary<string, string> fields, List<string> requiredKeys = null, string url = null)
        {
            var errors = new List<string>();
            var result = new Dictionary<string, string>
                {{RestApiMediatorKeys.Error, JsonConvert.SerializeObject(errors)}};

            UnityWebRequest request;

            if (url == null)
                url = URL;

            switch (requestType)
            {
                case Request.Get:
                {
                    request = UnityWebRequest.Get(GetGetRequestUrl(fields, url));
                    break;
                }
                case Request.Post:
                {
                    request = UnityWebRequest.Post(url, GetFilledFormData(fields));
                    break;
                }
                default:
                {
                    AddErrorAndAppointToResult(errors, RestApiMediatorErrors.TypeOfRequestNotWritten,
                     result, true);
                    return result;
                }
            }

            var gameData = new GameData {TowerUpgradeData = new List<UpgradeTowerData>
            {
                new UpgradeTowerData(0, 0), 
                new UpgradeTowerData(1, 0),
                new UpgradeTowerData(2, 0),
                new UpgradeTowerData(3, 0)
            }, CurrenciesData = new CurrenciesData() {NumberBooli = "0", NumberCoins = "500"}, OpenLevelIndex = 0};

            var test = JsonConvert.SerializeObject(new Dictionary<string, string>
            {
                {"GameData", JsonConvert.SerializeObject(gameData)},
                {
                    "InvitedFriends", JsonConvert.SerializeObject(new Dictionary<string, string>
                    {
                        {
                            "1001", JsonConvert.SerializeObject(
                                new Dictionary<string, string> {{"NumberCoins", "0"}, {"NumberBoolie", "0"}})
                        }
                    })
                }
            });

            await request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                AddErrorAndAppointToResult(errors, request.downloadHandler.error, result, true);
                return result;
            }

            Debug.Log(request.downloadHandler.text);
            
            var downloadDictionary =
                JsonConvert.DeserializeObject<Dictionary<string, string>>(request.downloadHandler.text);

            if (downloadDictionary == null)
            {
                foreach (var key in requiredKeys)
                {
                    AddErrorAndAppointToResult(errors, RestApiMediatorErrors.NotContainsKey.Replace("KEY",
                        key), result);
                }
                
                errors.ForEach(Debug.LogError);
                return result;
            }

            var unregisteredKeys = downloadDictionary.Keys.ToList().FindAll(key => !RestApiMediatorKeys.Keys.Contains(key));
            var isResultHasUnregisteredKeys = unregisteredKeys.Count > 0;

            if (isResultHasUnregisteredKeys)
            {
                foreach (var key in unregisteredKeys)
                    AddErrorAndAppointToResult(errors, RestApiMediatorErrors.UnregisteredKey + ": " + key, result);   
                
                errors.ForEach(Debug.LogError);
                return result;
            }

            var missingKeys = requiredKeys?.FindAll(key => !downloadDictionary.ContainsKey(key));

            if (missingKeys?.Count > 0)
            {
                foreach (var key in missingKeys)
                {
                    AddErrorAndAppointToResult(errors, RestApiMediatorErrors.NotContainsKey.Replace("KEY",
                        key), result);
                }
            }

            foreach (var pair in downloadDictionary)
            {
                if (result.ContainsKey(pair.Key))
                {
                    if (pair.Key == RestApiMediatorKeys.Error)
                        AddErrorAndAppointToResult(errors, pair.Value, result);
                    else
                        result[pair.Key] = pair.Value;
                }
                else
                    result.Add(pair.Key, pair.Value);
            }
            
            errors.ForEach(Debug.LogError);

            return result;
        }

        private static void AddErrorAndAppointToResult(List<string> errors, string error,
            Dictionary<string, string> result, bool isDebugErrors = false)
        {
            errors.Add(error);
            
            if (isDebugErrors)
                errors.ForEach(Debug.LogError);
                
            result[RestApiMediatorKeys.Error] = JsonConvert.SerializeObject(errors);
        }

        private static WWWForm GetFilledFormData(Dictionary<string, string> fields)
        {
            var formData = new WWWForm();

            foreach (var field in fields)
                formData.AddField(field.Key, field.Value);
            return formData;
        }

        private static string GetGetRequestUrl(Dictionary<string, string> fields, string url)
        {
            if (fields.Count == 0)
                return url;
            
            var fieldsList = fields.ToList();

            url += $"?{fieldsList[0].Key}={fieldsList[0].Value}";

            for (var i = 1; i < fieldsList.Count; i++)
                url += $"&{fieldsList[i].Key}={fieldsList[i].Value}";
            return url;
        }
    }
}