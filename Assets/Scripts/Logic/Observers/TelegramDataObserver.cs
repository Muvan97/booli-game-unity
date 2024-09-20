using TMPro;
using Tools;
using UnityEngine;

namespace Logic.Observers
{
    public class TelegramDataObserver : MonoBehaviour
    {
#if UNITY_EDITOR 
        [field: SerializeField]
#endif   
        public string ID { get; private set; } = "1";
#if UNITY_EDITOR 
        [field: SerializeField]
#endif   
        public string Nickname { get; private set; } = "";
#if UNITY_EDITOR 
        [field: SerializeField]
#endif        
        public string RefferalPlayerID { get; private set; } = "";

        public void GetNickname(string nickname)
        {
            Nickname = nickname;
            //NicknameText.text = nickname;
        }

        public void GetUserID(string id)
        {
            ID = id;
            //UserIDText.text = id;
        }

        public void GetRefferalPlayerID(string id)
        {
            RefferalPlayerID =
                id.IsNullOrEmptyOrWhitespace() || id == "null" || id == ID
                    ? "null"
                    : id;
            //RefferalPlayerIDText.text = RefferalPlayerID;
        }
    }
}