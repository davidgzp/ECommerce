using AutoMapper;
using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Profiles;
using ECommerce.Api.Products.Providers;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Products.Tests
{
    public class ProductsServiceTest
    {
        [Fact]
        public async Task GetProductsReturnsAllProducts()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnsAllProducts))
                .Options;

            var dbContext = new ProductDbContext(options);
            CreateProductsDemoData(dbContext);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg=>cfg.AddProfile(productProfile)); 
            var mapper = new Mapper(configuration);

            var productsProvider = new ProductsProvider(dbContext, null, mapper);

            var products = await productsProvider.GetProductsAsync();

            Assert.True(products.IsSuccess);
            Assert.True(products.Products.Any());
            Assert.Null(products.ErrorMessage);

        }

        [Fact]
        public async Task GetProductsReturnsAllProductsValidId()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnsAllProductsValidId))
                .Options;

            var dbContext = new ProductDbContext(options);
            CreateProductsDemoData(dbContext);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);

            var productsProvider = new ProductsProvider(dbContext, null, mapper);

            var product = await productsProvider.GetProductAsync(1);

            Assert.True(product.IsSuccess);
            Assert.NotNull(product.product);
            Assert.True(product.product.Id == 1);
            Assert.Null(product.ErrorMessage);

        }


        [Fact]
        public async Task GetProductsReturnsAllProductsInvalidId()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnsAllProductsInvalidId))
                .Options;

            var dbContext = new ProductDbContext(options);
            CreateProductsDemoData(dbContext);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);

            var productsProvider = new ProductsProvider(dbContext, null, mapper);

            var product = await productsProvider.GetProductAsync(-1);

            Assert.False(product.IsSuccess);
            Assert.Null(product.product);
            Assert.NotNull(product.ErrorMessage);

        }

        private void CreateProductsDemoData(ProductDbContext dbContext)
        {
            for (int i = 1; i <= 10; i++)
            {
                dbContext.Products.Add(new Product()
                {
                    Id = i,
                    Name = Guid.NewGuid().ToString(),
                    Inventory = i + 10,
                    Price = (decimal)(i * 3.14)
                });
            }

            dbContext.SaveChanges();
        }
    }
}