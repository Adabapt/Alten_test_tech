using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Alten_test_tech.Back.Application.Request.v1;
using Alten_test_tech.Back.Application.ViewModels;

namespace Alten_test_tech.Back.Application.Services
{
    public interface IProductService
    {

        Task<IEnumerable<ProductViewModel>> GetProductAsync(GetProductRequest request);

        Task<ResponseViewModel> UpdateProductAsync(InsertProductRequest request);

        Task<ResponseViewModel> UpdateProductAsync(UpdateProductRequest request);

        Task<ResponseViewModel> DeleteProductAsync(DeleteProductRequest request);
    }
}
