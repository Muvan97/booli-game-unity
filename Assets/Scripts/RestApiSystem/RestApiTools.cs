using System.Collections.Generic;
using Newtonsoft.Json;

namespace RestApiSystem
{
    public static class RestApiTools
    {
        public static bool IsRequestResultHasErrors(Dictionary<string, string> result) 
            => GetErrors(result)?.Count != 0;

        public static List<string> GetErrors(Dictionary<string, string> result) 
            => JsonConvert.DeserializeObject<List<string>>(result[RestApiMediatorKeys.Error]);
    }
}