using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Alten_test_tech.Back.Domain.Products.Entities;

namespace Alten_test_tech.Back.Domain.Products.Queries
{
    public class GetProductQuery : IRequest<IReadOnlyCollection<Product>>
    {

        public readonly int? IdProduct;

        public GetProductQuery(int? idProduct)
        {
            this.IdProduct = idProduct;
        }

    }
}
