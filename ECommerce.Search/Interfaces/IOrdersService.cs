using ECommerce.Search.Models;

namespace ECommerce.Search.Interfaces
{
    public interface IOrdersService
    {
        Task<(bool IsSuccess, IEnumerable<Order> Orders, string errorMsg)> GetOrdersAsync(int customerId);
    }
}
