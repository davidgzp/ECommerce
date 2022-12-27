using ECommerce.Search.Interfaces;
using ECommerce.Search.Models;
using System.Text.Json;

namespace ECommerce.Search.Services
{
    public class ProductsService: IProductsService
    {
        IHttpClientFactory httpClientFactory;
        ILogger<ProductsService> logger;

        public ProductsService(IHttpClientFactory httpClientFactory, ILogger<ProductsService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;   
        }

        async Task<(bool IsSuccess, IEnumerable<Product>? Products, string? errorMsg)> IProductsService.GetProductsAsync()
        {
            try
            {
                var client = httpClientFactory.CreateClient("ProductsService");
                var response = await client.GetAsync($"api/products");

                if(response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<IEnumerable<Product>>(content, options);

                    return (true, result, null);
                }
                return (false, null, response.ReasonPhrase); 
            }
            catch(Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
