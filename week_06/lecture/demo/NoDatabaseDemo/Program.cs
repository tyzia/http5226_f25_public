using System;
using System.Collections.Generic;

public class Program
{
    private static List<Product> _products = new List<Product>();
    private static int _nextId = 1;

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\nChoose an option:");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. View All Products");
            Console.WriteLine("3. Exit");
            Console.Write("Enter choice (1, 2 or 3): ");

            string choice = Console.ReadLine() ?? "4";

            switch (choice)
            {
                case "1":
                    AddProduct();
                    break;
                case "2":
                    DisplayCurrentData();
                    break;
                case "3":
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }
        }
    }



    static void AddProduct()
    {
        Console.Write("Enter product name: ");
        string name = Console.ReadLine() ?? string.Empty;

        Console.Write("Enter product price: ");
        decimal price = decimal.Parse(Console.ReadLine() ?? "0");

        Product product = new Product();
        product.Id = _nextId++;
        product.Name = name;
        product.Price = price;

        _products.Add(product);

        Console.WriteLine($"✅ Added: ID {product.Id} - {product.Name} - {product.Price}");
        Console.WriteLine($"Total products in memory: {_products.Count}");
    }

    public List<Product> GetAllProducts() => _products;

    static void DisplayCurrentData()
    {
        Console.WriteLine("CURRENT PRODUCTS IN MEMORY:");
        Console.WriteLine("===============================");

        if (_products.Count == 0)
        {
            Console.WriteLine("No products stored in memory.");
        }
        else
        {
            foreach (var product in _products)
            {
                Console.WriteLine($"ID: {product.Id} | Name: {product.Name} | Price: {product.Price}");
            }
            Console.WriteLine($"Total: {_products.Count} products");
        }
    }
}