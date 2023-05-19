using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Alten_test_tech.Back.Domain.Products.Entities;
using Alten_test_tech.Back.Domain.Products.Queries;


namespace Alten_test_tech.Back.Domain.Products.QueryHandlers
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, IReadOnlyCollection<Product>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductQueryHandler(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }

        public async Task<IReadOnlyCollection<Product>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            return await this._productRepository.GetProductAsync(request, cancellationToken);
        }
    }
}
