using System.Collections.Generic;
using Newtonsoft.Json;

namespace Core_Server.Models.Client.Data
{
    public class Order
    {
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonPropertyAttribute("products")]
        public virtual List<string> Products { get; set; }
    }
}