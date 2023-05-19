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
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Response>
    {

        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }

        public async Task<Response> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var response = await this._productRepository.UpdateProductAsync(request, cancellationToken);
            return response;
        }

    }
}
