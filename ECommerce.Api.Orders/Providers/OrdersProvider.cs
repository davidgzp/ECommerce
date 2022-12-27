﻿using AutoMapper;
using ECommerce.Api.Orders.Db;
using ECommerce.Api.Orders.Interfaces;
using ECommerce.Api.Orders.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ECommerce.Api.Orders.Providers
{
    public class OrdersProvider : IOrdersProvider
    {
        private readonly OrdersDbContext dbContext;

        private readonly ILogger<OrdersProvider> logger;

        private readonly IMapper mapper;

        public OrdersProvider(OrdersDbContext dbContext, ILogger<OrdersProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if(!dbContext.Orders.Any())
            {
                dbContext.Orders.Add(new Order()
                {
                    Id = 1,
                    CustomerId = 1,
                    Items = new List<OrderItem>()
                    {
                        new OrderItem(){OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice=10},
                        new OrderItem(){OrderId = 1, ProductId = 2, Quantity = 10, UnitPrice=10},
                        new OrderItem(){OrderId = 1, ProductId = 3, Quantity = 10, UnitPrice=10},
                        new OrderItem(){OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice=10},
                        new OrderItem(){OrderId = 2, ProductId = 2, Quantity = 10, UnitPrice=10},
                        new OrderItem(){OrderId = 3, ProductId = 3, Quantity = 1, UnitPrice=100},
                    },
                    Total=100
                }) ;

                dbContext.Orders.Add(new Order()
                {
                    Id = 2,
                    CustomerId = 1,
                    Items = new List<OrderItem>()
                    {
                        new OrderItem(){OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice=10},
                        new OrderItem(){OrderId = 1, ProductId = 2, Quantity = 20, UnitPrice=10},
                        new OrderItem(){OrderId = 2, ProductId = 2, Quantity = 10, UnitPrice=10},
                        new OrderItem(){OrderId = 3, ProductId = 3, Quantity = 1, UnitPrice=100},
                    },
                    Total=100
                });

                dbContext.Orders.Add(new Order()
                {
                    Id = 3,
                    CustomerId = 2,
                    Items = new List<OrderItem>()
                    {
                        new OrderItem(){OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice=10},
                        new OrderItem(){OrderId = 2, ProductId = 2, Quantity = 20, UnitPrice=10},
                        new OrderItem(){OrderId = 3, ProductId = 3, Quantity = 10, UnitPrice=100},
                    },
                    Total = 100
                });

                dbContext.SaveChanges();
            }
        }


        public async Task<(bool IsSuccess, IEnumerable<OrderDto>? Orders, string? errorMsg)> GetOrdersAsync(int customerId)
        {
            try
            {
                var orders = await dbContext.Orders
                    .Where(o => o.CustomerId == customerId)
                    .Include(o => o.Items)
                    .ToListAsync();

                if(orders != null && orders.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Order>, IEnumerable<Models.OrderDto>>(orders);

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
