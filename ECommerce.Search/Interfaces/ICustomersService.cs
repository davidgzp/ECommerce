using ECommerce.Search.Models;

namespace ECommerce.Search.Interfaces
{
    public interface ICustomersService
    {
        Task<(bool IsSuccess, dynamic Customer, string ErrorMsg)> GetCustomerAsync(int id);
    }
}
