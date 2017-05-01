using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Core_Server.Models.Data
{
    public class SubCategory
    {
        [JsonProperty("id")]
        [Key]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("categoryID")]
        public string CategoryID { get; set; }

        [JsonProperty("previewImageID")]
        public string ImageID { get; set; }

        [JsonProperty("previewImage")]
        [ForeignKey("ImageID")]
        public virtual Image Image { get; set; }

        [JsonIgnoreAttribute]
        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }
    }
}