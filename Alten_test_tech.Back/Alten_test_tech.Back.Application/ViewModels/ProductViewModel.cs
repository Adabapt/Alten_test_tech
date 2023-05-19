using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alten_test_tech.Back.Application.ViewModels
{
    public class ProductViewModel
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

        public float? Rating { get; set; }
    }
}
