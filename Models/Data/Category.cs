using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Core_Server.Models.Data {
    public class Category
    {
        public Category()
        {
            SubCategories = new List<SubCategory>();
        }

        [JsonProperty("id")]
        [Key]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("previewImageID")]
        public string ImageID { get; set; }


        //------------------------------
        [ForeignKeyAttribute("ImageID")]
        [JsonProperty("previewImage")]
        public virtual Image Image { get; set; }

        [JsonIgnore]
        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}