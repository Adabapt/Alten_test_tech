using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alten_test_tech.Back.Application.Request.v1
{
    public class InsertProductRequest
    {
        [JsonProperty("CodeProduct")]
        public string Code { get; set; }
        
        [JsonProperty("NameProduct")]
        public string Name { get; set; }

        [JsonProperty("DescriptionProduct")]
        public string Description { get; set; }

        [JsonProperty("PriceProduct")]
        public float Price { get; set; }

        [JsonProperty("QuantityProduct")]
        public int Quantity { get; set; }

        [JsonProperty("InventoryStatusProduct")]
        public string InventoryStatus { get; set; }

        [JsonProperty("CategoryProduct")]
        public string Category { get; set; }

        [JsonProperty("ImageProduct")]
        public string? Image { get; set; }

        [JsonProperty("RatingProduct")]
        public float? Rating { get; set; }

    }
}
