using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Core_Server.Models.Data {
    public class Image
    {
        [JsonProperty("id")]
        [Key]
        public string Id { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonIgnoreAttribute]
        public virtual Product Product { get; set; }
    }
}