using Demo02.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo02.Services
{
    public class ProductService
    {
        private readonly List<Product> _products;

        public ProductService()
        {
            // Seed data
            _products = new List<Product>
            {
                new Product { Id = 1, Name = "Laptop Dell XPS", Category = "Electronics", Price = 1500 },
                new Product { Id = 2, Name = "iPhone 15", Category = "Electronics", Price = 1200 },
                new Product { Id = 3, Name = "Samsung Galaxy S24", Category = "Electronics", Price = 1100 },
                new Product { Id = 4, Name = "Nike Air Max", Category = "Fashion", Price = 120 },
                new Product { Id = 5, Name = "Adidas Ultraboost", Category = "Fashion", Price = 140 },
                new Product { Id = 6, Name = "Harry Potter Book", Category = "Books", Price = 20 },
                new Product { Id = 7, Name = "Clean Code", Category = "Books", Price = 35 },
                new Product { Id = 8, Name = "Gaming Chair", Category = "Furniture", Price = 200 },
                new Product { Id = 9, Name = "Office Desk", Category = "Furniture", Price = 150 },
                new Product { Id = 10, Name = "Mechanical Keyboard", Category = "Electronics", Price = 100 }
            };
        }

        public Task<(List<Product>, int)> GetProductsAsync(ProductQueryParameters queryParams)
        {
            var query = _products.AsQueryable();

            // 1. Filtering
            if (!string.IsNullOrEmpty(queryParams.SearchTerm))
            {
                query = query.Where(p => p.Name.Contains(queryParams.SearchTerm, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(queryParams.Category))
            {
                query = query.Where(p => p.Category.Equals(queryParams.Category, StringComparison.OrdinalIgnoreCase));
            }

            // 2. Sorting
            if (!string.IsNullOrEmpty(queryParams.SortBy))
            {
                switch (queryParams.SortBy.ToLower())
                {
                    case "name":
                        query = queryParams.SortAscending ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name);
                        break;
                    case "price":
                        query = queryParams.SortAscending ? query.OrderBy(p => p.Price) : query.OrderByDescending(p => p.Price);
                        break;
                }
            }

            // Total count for paging
            int totalCount = query.Count();

            // 3. Paging
            var items = query
                .Skip((queryParams.PageNumber - 1) * queryParams.PageSize)
                .Take(queryParams.PageSize)
                .ToList();

            return Task.FromResult((items, totalCount));
        }

        public List<string> GetCategories()
        {
            return _products.Select(p => p.Category).Distinct().ToList();
        }
    }
}
