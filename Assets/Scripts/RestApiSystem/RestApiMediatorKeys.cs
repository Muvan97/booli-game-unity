namespace RestApiSystem
{

    public static class RestApiMediatorKeys
    {
        public const string Error = "Error";
        public const string GameData = "GameData";
        private const string IsRegistered = "IsRegistered";
        public const string InvitedPlayers = "InvitedPlayers";
        public const string InvoiceLink = "InvoiceLink";
        private const string IsSaved = "IsSaved";
        private const string Avatar = "Avatar";
        private const string Nickname = "Nickname";
        public const string IsPlayerSentBooli = "IsPlayerSentBooli";


        public static readonly string[] Keys =
        {
            Error, IsRegistered, GameData,
            IsSaved, InvitedPlayers, Avatar, Nickname,
            InvoiceLink, IsPlayerSentBooli
        };
    }
}