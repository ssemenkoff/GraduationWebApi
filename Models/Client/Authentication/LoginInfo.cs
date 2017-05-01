using Newtonsoft.Json;

namespace Core_Server.Models.Client.Authentication {
    public class LoginInfo
    {
        [JsonProperty("username")]
        public string Login { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}