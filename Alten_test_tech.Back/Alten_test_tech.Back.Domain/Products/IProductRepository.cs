using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Alten_test_tech.Back.Domain.Products.Commands;
using Alten_test_tech.Back.Domain.Products.Entities;
using Alten_test_tech.Back.Domain.Products.Queries;
using Alten_test_tech.Back.Domain.Responses;

namespace Alten_test_tech.Back.Domain.Products
{
    public interface IProductRepository
    {

        Task<IReadOnlyCollection<Product>> GetProductAsync(GetProductQuery request, CancellationToken cancellationToken);

        Task<Response> UpdateProductAsync(UpdateProductCommand request, CancellationToken cancellationToken);

        Task<Response> DeleteProductAsync(DeleteProductCommand request, CancellationToken cancellationToken);
    }
}
