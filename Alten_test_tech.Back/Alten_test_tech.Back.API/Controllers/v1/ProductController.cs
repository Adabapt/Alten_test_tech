using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Alten_test_tech.Back.Application.Request.v1;
using Alten_test_tech.Back.Application.Services;
using Alten_test_tech.Back.Application.ViewModels;


namespace Alten_test_tech.Back.API.Controllers.v1
{
    /// <summary>
    /// Controller
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ProductController : ControllerBase
    {

        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            this._productService = productService;
        }


        /// <summary>
        /// [BASE DE DONNEES]
        /// Requete SELECT qui recupere tous les products.
        /// </summary>
        [HttpGet("Get/Products", Name = nameof(GetAllProduct))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> GetAllProduct()
        {
            GetProductRequest request = new GetProductRequest();
            request.IdProduct = null;
            var result = await this._productService.GetProductAsync(request);
            return this.Ok(result);
        }


        /// <summary>
        /// [BASE DE DONNEES]
        /// Requete SELECT qui recupere un produit en fonction de son id
        /// </summary>
        [HttpGet("Get/Products/Id", Name = nameof(GetProduct))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> GetProduct(int idProduct)
        {
            GetProductRequest request = new GetProductRequest();
            request.IdProduct = idProduct;
            var result = await this._productService.GetProductAsync(request);
            return this.Ok(result);
        }

        /// <summary>
        /// [BASE DE DONNEES]
        /// Requete INSERT qui ajoute un product à la base
        /// </summary>
        [HttpPost("Post/Products", Name = nameof(PostProduct))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<ResponseViewModel>>> PostProduct(InsertProductRequest request)
        {
            var result = await this._productService.UpdateProductAsync(request);
            return this.Ok(result);
        }

        /// <summary>
        /// [BASE DE DONNEES]
        /// Requete UPDATE qui met a jour un product de la base
        /// </summary>
        [HttpPatch("Patch/Products", Name = nameof(PatchProduct))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ResponseViewModel>> PatchProduct(UpdateProductRequest request)
        {
            var result = await this._productService.UpdateProductAsync(request);
            return this.Ok(result);
        }

        /// <summary>
        /// [BASE DE DONNEES]
        /// Requete DELETE qui supprime un product de la base
        /// </summary>
        [HttpDelete("Delete/Products/Id", Name = nameof(DeleteProduct))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ResponseViewModel>> DeleteProduct(int idProduct)
        {
            DeleteProductRequest request = new DeleteProductRequest();
            request.IdProduct = idProduct;
            var result = await this._productService.DeleteProductAsync(request);
            return this.Ok(result);
        }
    }
}
