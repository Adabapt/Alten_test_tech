using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Alten_test_tech.Back.Domain.Products.Commands;
using Alten_test_tech.Back.Domain.Responses;

namespace Alten_test_tech.Back.Domain.Products.CommandHandlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Response>
    {

        private readonly IProductRepository _productRepository;

        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }

        public async Task<Response> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var response = await this._productRepository.DeleteProductAsync(request, cancellationToken);
            return response;
        }

    }
}
