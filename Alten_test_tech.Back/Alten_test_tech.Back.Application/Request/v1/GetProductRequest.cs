using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alten_test_tech.Back.Application.Request.v1
{
    public class GetProductRequest
    {
        [JsonProperty("IdProduct")]
        public int? IdProduct { get; set; }

    }
}
