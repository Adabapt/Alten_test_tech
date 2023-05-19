using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Alten_test_tech.Back.Application.Request.v1;
using Alten_test_tech.Back.Application.ViewModels;
using Alten_test_tech.Back.Domain.Products.Entities;
using Alten_test_tech.Back.Domain.Products.Queries;
using Alten_test_tech.Back.Domain.Products.Commands;
using Alten_test_tech.Back.Domain.Responses;

namespace Alten_test_tech.Back.Application.Services
{
    public class ProductService : IProductService
    {

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductService(IMediator mediator, IMapper mapper)
        {
            this._mediator = mediator;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<ProductViewModel>> GetProductAsync(GetProductRequest request)
        {
            var query = new GetProductQuery(request.IdProduct);
            var result = await this._mediator.Send(query);
            return this._mapper.Map<IReadOnlyCollection<Product>, IEnumerable<ProductViewModel>>(result);
        }

        public async Task<ResponseViewModel> UpdateProductAsync(InsertProductRequest request)
        {
            Product product = new Product();
            product.Id = null;
            product.Code = request.Code;
            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.Quantity = request.Quantity;
            product.InventoryStatus = request.InventoryStatus;
            product.Category = request.Category;
            product.Image = request.Image;
            product.Rating = request.Rating;

            var query = new UpdateProductCommand(product);
            var result = await this._mediator.Send(query);
            return this._mapper.Map<ResponseViewModel>(result);
        }

        public async Task<ResponseViewModel> UpdateProductAsync(UpdateProductRequest request)
        {
            Product product = new Product();
            product.Id = request.Id;
            product.Code = request.Code;
            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.Quantity = request.Quantity;
            product.InventoryStatus = request.InventoryStatus;
            product.Category = request.Category;
            product.Image = request.Image;
            product.Rating = request.Rating;

            var query = new UpdateProductCommand(product);
            var result = await this._mediator.Send(query);
            return this._mapper.Map<ResponseViewModel>(result);
        }

        public async Task<ResponseViewModel> DeleteProductAsync(DeleteProductRequest request)
        {
            var query = new DeleteProductCommand(request.IdProduct);
            var result = await this._mediator.Send(query);
            return this._mapper.Map<ResponseViewModel>(result);
        }

    }
}
