using ECommerce.Search.Models;

namespace ECommerce.Search.Interfaces
{
    public interface IProductsService
    {
        Task<(bool IsSuccess, IEnumerable<Product>? Products, string? errorMsg)> GetProductsAsync();
    }
}
