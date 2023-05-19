using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Alten_test_tech.Back.Domain.Responses;
using Alten_test_tech.Back.Domain.Products.Entities;

namespace Alten_test_tech.Back.Domain.Products.Commands
{
    public class UpdateProductCommand : IRequest<Response>
    {

        public readonly Product product;

        public UpdateProductCommand(Product product)
        {
            this.product = product;
        }

    }
}
