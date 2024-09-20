using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Configs;
using Cysharp.Threading.Tasks;
using Infrastructure.Services;
using Logic.Other;
using Logic.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Tools
{
    public static class UITools
    {
        public static List<Button> FindAllButtons() => Object.FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();
        //public static bool IsUseMobileUI(GameConfig config) =>  Application.isMobilePlatform || Application.isEditor && config.IsTestMobileUI;

        public static Vector2 GetRectWorldSize(RectTransform transform)
        {
            var borders = GetRectWorldBorders(transform);
            return new Vector2(Mathf.Abs(borders.RightBorder - borders.LeftBorder),
                Mathf.Abs(borders.TopBorder - borders.BottomBorder));
        }

        public static void InitializeCoinsCounterView(CounterView counterView, Currency currency)
        {
            counterView.UpdateText(currency.Number);
            currency.NumberChanged += counterView.UpdateText;
        }

        public static Rectangle GetRectWorldBorders(RectTransform transform)
        {
            var rect = transform.rect;
            var minBorders = transform.TransformPoint(rect.min);
            var maxBorders = transform.TransformPoint(rect.max);
            return new Rectangle(minBorders.x, maxBorders.x, maxBorders.y, minBorders.y);
        }
        public static async UniTask UpdateShortNicknameTextAndAvatarRawImage(RawImage rawImage,
            GameDataProviderAndSaverService gameDataProviderAndSaverService, 
            TMP_Text userShortNicknameText, CancellationToken token)
        {
            await UniTask.WaitUntil(() => gameDataProviderAndSaverService.PlayerNickname != null, cancellationToken: token);
            await UniTask.WaitUntil(() => gameDataProviderAndSaverService.PlayerAvatarTexture != null, cancellationToken: token);
            InitializeUserAvatarRawImage(gameDataProviderAndSaverService.PlayerAvatarTexture, rawImage);
            userShortNicknameText.gameObject.SetActive(false);
        }
        public static void InitializeUserAvatarRawImage(Texture playerAvatarTexture, RawImage rawImage)
        {
            if (playerAvatarTexture != null)
                rawImage.texture = playerAvatarTexture;
            else
                rawImage.gameObject.SetActive(false);
        }

        public static void InitializeUserNicknameText(TMP_Text text, string nickname, int nicknameLength) 
            => text.text = nickname.TruncateString(nicknameLength);
    }
}