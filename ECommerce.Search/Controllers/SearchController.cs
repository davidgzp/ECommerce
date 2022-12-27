using ECommerce.Search.Interfaces;
using ECommerce.Search.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace ECommerce.Search.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController:ControllerBase
    {
        private readonly ISearchService searchService;

        public SearchController(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        [HttpPost]
        public async Task<IActionResult> SearchAsync(SearchTerm term)
        {
            var result = await searchService.SearchAsync(term.CustomerId);
            if (result.IsSuccess)
                return Ok(result.SearchResults);

            return NotFound();
        }
    }
}
