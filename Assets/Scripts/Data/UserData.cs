using UnityEngine;

namespace Data
{
    public class UserData
    {
        public readonly Texture AvatarTexture;
        public readonly string Nickname;

        public UserData(Texture avatarTexture, string nickname)
        {
            AvatarTexture = avatarTexture;
            Nickname = nickname;
        }
    }
}