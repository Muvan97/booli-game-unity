using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.UI.FriendsPopup
{
    public class FriendsPopupView : BasePopup
    {
        [field: SerializeField] public List<TMP_Text> RewardTextsForInvitedFriends { get; private set; }
        [field: SerializeField] public Transform FriendsImagesViewsParent { get; private set; } 
        [field: SerializeField] public Button InviteFriendButton { get; private set; }
        [field: SerializeField] public Button CopyInviteLinkButton { get; private set; }
    }
}