using AutoMapper;
using ECommerce.Api.Customers.Db;
using ECommerce.Api.Customers.Interfaces;
using ECommerce.Api.Customers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Customers.Providers
{
    public class CustomersProvider : ICustomersProvider
    {

        CustomerDbContext dbContext;
        ILogger<CustomersProvider> logger;
        IMapper mapper;     

        public CustomersProvider(CustomerDbContext dbContext, ILogger<CustomersProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.Customers.Any())
            {
                dbContext.Customers.Add(new Customer() { Id = 1, Name = "Tom Seet", Address = "123 NoName Street, Ontario, Canada" });
                dbContext.Customers.Add(new Customer() { Id = 2, Name = "Jerry Slote", Address = "456 Main Street, Ontario, Canada" });
                dbContext.Customers.Add(new Customer() { Id = 3, Name = "Ava Bettle", Address = "454545 Center Street, Ontario, Canada" });
                dbContext.Customers.Add(new Customer() { Id = 4, Name = "Brian Avenu", Address = "5 King Street, Ontario, Canada" });

                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<CustomerDto> customers, string errorMsg)> GetCustomersAsync()
        {
            try
            {
                var customers = await dbContext.Customers.ToListAsync();

                if (customers != null && customers.Any())
                {
                    var result = mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDto>>(customers);

                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch(Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, CustomerDto customer, string errorMsg)> GetCustomerAsync(int id)
        {
            try
            {
                var customer = await dbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);

                if(customer != null)
                {
                    var result = mapper.Map<Customer, CustomerDto>(customer);

                    return (true, result, null);
                }

                return (false, null, "Not found");
            }
            catch(Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

    }
}
