using Configs;
using Infrastructure.Services;
using TMPro;
using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.UI.FriendsPopup
{
    public class FriendImageView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nicknameText;
        //[SerializeField] private TMP_Text _shortNicknameText;
        [SerializeField] private TMP_Text _numberGiftedCoinsNumber;
        [SerializeField] private TMP_Text _numberGiftedBooliNumber;
        [SerializeField] private RawImage _avatarRawImage;

        public void Initialize(string nickname, decimal numberGiftedCoins, decimal numberGiftedBooli, Texture avatarTexture)
        {
            _nicknameText.text = nickname;
            _numberGiftedCoinsNumber.text = "<sprite=0>" + numberGiftedCoins.ToKMBString();
            _numberGiftedBooliNumber.text = "<sprite=0>" + numberGiftedBooli.ToKMBString();
            _avatarRawImage.texture = avatarTexture;
            UITools.InitializeUserAvatarRawImage(avatarTexture, _avatarRawImage);
        }
    }
}