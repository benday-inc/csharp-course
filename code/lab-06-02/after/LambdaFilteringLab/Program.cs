using System;
using System.Collections.Generic;
using System.Linq;

namespace LambdaFilteringLab
{
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Product> products = new List<Product>
            {
                new Product { Name = "Laptop", Price = 999.99m, Category = "Electronics" },
                new Product { Name = "Desk", Price = 150.00m, Category = "Furniture" },
                new Product { Name = "Headphones", Price = 89.99m, Category = "Electronics" },
                new Product { Name = "Coffee Mug", Price = 12.50m, Category = "Kitchen" },
            };

            var electronics = FilterProducts(products, IsElectronics);
            Console.WriteLine("Electronics:");
            electronics.ForEach(p => Console.WriteLine(p.Name));

            var cheapItems = FilterProducts(products, p => p.Price < 100);
            Console.WriteLine("\nCheap Items:");
            cheapItems.ForEach(p => Console.WriteLine(p.Name));

            var sortedByPrice = SortProducts(products, p => p.Price);
            Console.WriteLine("\nSorted by Price:");
            sortedByPrice.ForEach(p => Console.WriteLine($"\"{p.Name}\": ${p.Price}"));
        }

        static List<Product> FilterProducts(List<Product> items, Func<Product, bool> predicate)
        {
            return items.Where(predicate).ToList();
        }

        static bool IsElectronics(Product p) => p.Category == "Electronics";

        static List<Product> SortProducts(List<Product> items, Func<Product, object> keySelector)
        {
            return items.OrderBy(keySelector).ToList();
        }
    }
}
