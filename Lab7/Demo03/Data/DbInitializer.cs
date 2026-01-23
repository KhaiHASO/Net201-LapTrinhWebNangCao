using Demo03.Data;
using Demo03.Models;

namespace Demo03.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            // Check if any products exist
            if (context.Products.Any())
            {
                return;   // DB has been seeded
            }

            var categories = new Category[]
            {
                new Category { CategoryName = "Electronics" },
                new Category { CategoryName = "Books" },
                new Category { CategoryName = "Clothing" }
            };

            context.Categories.AddRange(categories);
            context.SaveChanges();

            var products = new Product[]
            {
                // Electronics
                new Product { ProductName = "Smartphone X", Price = 900, CategoryId = categories[0].CategoryId },
                new Product { ProductName = "Laptop Pro", Price = 1500, CategoryId = categories[0].CategoryId },
                new Product { ProductName = "Headphones", Price = 200, CategoryId = categories[0].CategoryId },
                new Product { ProductName = "Smart Watch", Price = 300, CategoryId = categories[0].CategoryId },
                new Product { ProductName = "Camera", Price = 800, CategoryId = categories[0].CategoryId },

                // Books
                new Product { ProductName = "The Great Gatsby", Price = 20, CategoryId = categories[1].CategoryId },
                new Product { ProductName = "C# Programming", Price = 50, CategoryId = categories[1].CategoryId },
                new Product { ProductName = "Design Patterns", Price = 60, CategoryId = categories[1].CategoryId },
                new Product { ProductName = "History of Art", Price = 45, CategoryId = categories[1].CategoryId },
                
                // Clothing
                new Product { ProductName = "T-Shirt", Price = 15, CategoryId = categories[2].CategoryId },
                new Product { ProductName = "Jeans", Price = 40, CategoryId = categories[2].CategoryId },
                new Product { ProductName = "Jacket", Price = 120, CategoryId = categories[2].CategoryId },
                new Product { ProductName = "Sneakers", Price = 80, CategoryId = categories[2].CategoryId },
                new Product { ProductName = "Hat", Price = 20, CategoryId = categories[2].CategoryId },
                new Product { ProductName = "Socks", Price = 5, CategoryId = categories[2].CategoryId }
            };

            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}
