using Newtonsoft.Json;

namespace Core_Server.Models.Client.Authentication {
    public class AuthResponce {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }
    }
}