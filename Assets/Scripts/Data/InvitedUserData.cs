using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class InvitedUserData
    {
        public Texture AvatarTexture;
        public string Nickname;
        public readonly string ID;
        public readonly string NumberGiftedCoins;
        public readonly string NumberGiftedBooli;
        
        [JsonConstructor]
        public InvitedUserData(string id, string numberGiftedCoins, string numberGiftedBooli)
        {
            ID = id;
            NumberGiftedCoins = numberGiftedCoins;
            NumberGiftedBooli = numberGiftedBooli;
        }
    }
}