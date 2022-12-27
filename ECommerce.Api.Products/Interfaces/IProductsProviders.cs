using ECommerce.Api.Products.Models;
using System.Collections;

namespace ECommerce.Api.Products.Interfaces
{
    public interface IProductsProviders
    {
        Task<(bool IsSuccess, IEnumerable<ProductDto> Products, string ErrorMessage)> GetProductsAsync();
        Task<(bool IsSuccess, ProductDto product, string ErrorMessage)> GetProductAsync(int id);
    }
}
