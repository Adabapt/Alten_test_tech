using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Alten_test_tech.Back.Domain.Responses;

namespace Alten_test_tech.Back.Domain.Products.Commands
{
    public class DeleteProductCommand : IRequest<Response>
    {

        public readonly int IdProduct;

        public DeleteProductCommand(int idProduct)
        {
            this.IdProduct = idProduct;
        }

    }
}
