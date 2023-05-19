using System;
using System.Collections.Generic;
using System.Text;

namespace Alten_test_tech.Back.Domain.Products.Entities
{
    public partial class Product
    {

        public int? Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        public int Quantity { get; set; }

        public string InventoryStatus { get; set; }

        public string Category { get; set; }

        public string? Image { get; set; }

        public double? Rating { get; set; }

    }

}
