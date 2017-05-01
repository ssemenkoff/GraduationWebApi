using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;


namespace Core_Server.Models.Data
{
    public class Product
    {
        public Product () {
            Images = new List<Image>();
            Orders = new HashSet<OrderProduct>();
        }

        [JsonProperty("id")]
        [Key]
        public string Id { get; set; }

        [JsonProperty("categoryId")]
        public string CategoryId { get; set; }

        [JsonProperty("subCategoryId")]
        public string SubCategoryId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("previewImageId")]
        public string PreviewImageId { get; set; }

        [JsonProperty("shortDescription")]
        public string ShortDescription { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }
        
        [ForeignKeyAttribute("CategoryId")]
        [JsonIgnoreAttribute]
        public virtual Category Category { get; set; }

        [ForeignKeyAttribute("SubCategoryId")]
        [JsonIgnoreAttribute]
        public virtual SubCategory SubCategory { get; set; }

        [JsonProperty("images")]
        public virtual ICollection<Image> Images { get; set; }

        [ForeignKeyAttribute("PreviewImageId")]
        [JsonProperty("previewImage")]
        public virtual Image PreviewImage { get; set; }

        [JsonIgnore]
        public virtual ICollection<OrderProduct> Orders { get; set; }
    }
}