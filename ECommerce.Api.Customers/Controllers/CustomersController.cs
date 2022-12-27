using ECommerce.Api.Customers.Interfaces;
using ECommerce.Api.Customers.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ECommerce.Api.Customers.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController:ControllerBase
    {
        private readonly ICustomersProvider provider;

        public CustomersController(ICustomersProvider provider)
        {
            this.provider = provider;
        }


        [HttpGet]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var result = await provider.GetCustomersAsync();

            if(result.IsSuccess)
            {
                return Ok(result.customers);
            }

            return NotFound();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var result = await provider.GetCustomerAsync(id);
            if(result.IsSuccess)
            {
                return Ok(result.customer);
            }

            return NotFound();
        }
    }
}
