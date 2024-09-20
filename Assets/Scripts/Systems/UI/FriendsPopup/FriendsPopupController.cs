using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Data;
using Holders;
using Tools;
using UnityEngine;

namespace Systems.UI.FriendsPopup
{
    public class FriendsPopupController
    {
        public FriendsPopupController(FriendsPopupView friendsPopupView, FriendsPopupModel friendsPopupModel)
        {
            FillRewardsTexts(friendsPopupView, friendsPopupModel).Forget();
            
            friendsPopupView.CopyInviteLinkButton.onClick.AddListener(() => CopyLink(friendsPopupModel));
            friendsPopupView.InviteFriendButton.onClick.AddListener(() => OpenShareTelegramWindow(friendsPopupModel));

            if (friendsPopupModel.GameDataProviderAndSaverService.InvitedPlayers == null)
                return;

            CreateAndFillAllFriendsImages(friendsPopupView, friendsPopupModel).Forget();
        }

        private async UniTask FillRewardsTexts(FriendsPopupView friendsPopupView, FriendsPopupModel friendsPopupModel)
        {
            var bonusText =
                await LocalizationTools.GetLocalizedString(LocalizationKeysHolder.Bonus,
                    LocalizationTable.FriendsPopup);

            for (var i = 0; i < friendsPopupView.RewardTextsForInvitedFriends.Count; i++)
            {
                var index = i;
                
                friendsPopupView.RewardTextsForInvitedFriends[i].text =
                    bonusText + $" {i + 1}: " + (i >= friendsPopupModel.GameConfig.RewardsForRefferalsConfig.Bonuses.Count
                        ? ""
                        : friendsPopupModel.GameConfig.RewardsForRefferalsConfig.Bonuses[i].ToKMBString() +  "<sprite=0> " + 
                          await LocalizationTools.GetLocalizedString(LocalizationKeysHolder.BonusNTitle.Replace("N", (index + 1).ToString()),
                              LocalizationTable.FriendsPopup));
            }
        }

        private async UniTask CreateAndFillAllFriendsImages(FriendsPopupView friendsPopupView,
            FriendsPopupModel friendsPopupModel)
        {
            var token = friendsPopupView.destroyCancellationToken;
            
            foreach (var friendData in friendsPopupModel.GameDataProviderAndSaverService.InvitedPlayers)
            {
                var instance = friendsPopupModel.UIFactory.GetFriendImageView(friendsPopupView.FriendsImagesViewsParent);

                if (friendData.AvatarTexture == null || friendData.Nickname == null)
                {
                    var userData = await DownloadTools.DownloadUserData(friendData.ID, friendsPopupModel.RestApiMediatorService);
                    friendData.Nickname = userData.Nickname;
                    friendData.AvatarTexture = userData.AvatarTexture;
                }
                
                if (token.IsCancellationRequested)
                    return;

                instance.Initialize(friendData?.Nickname, Convert.ToDecimal(friendData.NumberGiftedCoins), 
                    Convert.ToDecimal(friendData.NumberGiftedBooli), friendData?.AvatarTexture);
            }
        }
        

        private void OpenShareTelegramWindow(FriendsPopupModel friendsPopupModel) =>
            Application.OpenURL(friendsPopupModel.ShareLink);

        private void CopyLink(FriendsPopupModel friendsPopupModel) => GUIUtility.systemCopyBuffer = friendsPopupModel.ShareLink;
    }
}