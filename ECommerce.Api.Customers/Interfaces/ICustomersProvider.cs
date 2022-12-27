using ECommerce.Api.Customers.Models;

namespace ECommerce.Api.Customers.Interfaces
{
    public interface ICustomersProvider
    {
        Task<(bool IsSuccess, IEnumerable<CustomerDto>? customers, string? errorMsg)> GetCustomersAsync();

        Task<(bool IsSuccess, CustomerDto? customer, string? errorMsg)> GetCustomerAsync(int id);  
    }
}
