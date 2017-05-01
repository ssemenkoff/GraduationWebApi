using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Core_Server.Models.Data {
    public class Order
    {
        public Order() {
            this.Products = new HashSet<OrderProduct>();
        }

        [JsonProperty("id")]
        [Key]
        public string Id { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonPropertyAttribute("products")]
        public virtual ICollection<OrderProduct> Products { get; set; }

        [JsonPropertyAttribute("accepted")]
        public bool Accepted { get; set; }

        [JsonPropertyAttribute("completed")]
        public bool Completed { get; set; }
    }

    public class OrderProduct {
        [KeyAttribute]
        public string Id { get; set; }

        public string ProductID { get; set; }

        [ForeignKeyAttribute("ProductId")]
        public Product Product { get; set; }
                
        public string OrderID { get; set; }

        [ForeignKeyAttribute("OrderID")]
        public Order Order { get; set; }
    }
}