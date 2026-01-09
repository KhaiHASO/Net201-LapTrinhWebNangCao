using Demo02.Models;
using Demo02.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Demo02.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] ProductQueryParameters queryParams)
        {
            // Call service to get filtered data
            var (items, totalCount) = await _productService.GetProductsAsync(queryParams);

            // Populate ViewBag for View rendering
            ViewBag.Categories = _productService.GetCategories();
            ViewBag.CurrentSort = queryParams.SortBy;
            ViewBag.CurrentSortAsc = queryParams.SortAscending;
            ViewBag.TotalCount = totalCount;
            ViewBag.PageNumber = queryParams.PageNumber;
            ViewBag.PageSize = queryParams.PageSize;
            ViewBag.TotalPages = (int)Math.Ceiling(totalCount / (double)queryParams.PageSize);
            
            // Pass query params back to view to maintain state in filter form
            ViewBag.CurrentSearch = queryParams.SearchTerm;
            ViewBag.CurrentCategory = queryParams.Category;

            return View(items);
        }
    }
}
