namespace RestApiSystem
{
    public static class RestApiMediatorErrors
    {
        public const string ConnectionError = "ConnectionError";
        public const string IdentifierOrHisTableNameIsEmpty = "IdentifierIsEmpty";
        public const string EmptyFields = "EmptyFields";
        public const string TypeOfRequestNotWritten = "TypeOfRequestNotWrittenInsideMethod";
        public const string UnregisteredKey = "UnregisteredKey";
        public const string NotContainsKey = "NotContainsKey: KEY";
        public const string UnprocessedRequest = "UnprocessedRequest";
    }
}