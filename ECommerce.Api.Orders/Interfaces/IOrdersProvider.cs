using ECommerce.Api.Orders.Models;

namespace ECommerce.Api.Orders.Interfaces
{
    public interface IOrdersProvider
    {
        Task<(bool IsSuccess, IEnumerable<OrderDto>? Orders, string? errorMsg)> GetOrdersAsync(int customerId);
    }
}
