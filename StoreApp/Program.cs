using System;


class Program
{

    static void Main(string[] args)
    {

        MenuSwitchRun();

    }

    static void MenuDisplay()
    {
        Console.WriteLine("===My Store ===");
        Console.WriteLine("________________");
        Console.WriteLine("1.Products");
        Console.WriteLine("2.Customers");
        Console.WriteLine("3.Orders");
        Console.WriteLine("0.Exit");


    }

    static void MenuSwitchRun()
    {
        string userChoice = "";


        while (userChoice != "0")
        {
            MenuDisplay();
            Console.WriteLine("Choose an option :");
            userChoice = Console.ReadLine()?.Trim() ?? "empty";

            switch (userChoice)
            {
                case "1":
                    ManageProducts();
                    break;
                case "2":
                    Console.WriteLine("Coming soon");
                    break;
                case "3":
                    Console.WriteLine("Coming soon");
                    break;
                case "0":
                    Console.WriteLine("Thank you for using MyStore waiti ng for you once again!");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again");
                    break;

            }


        }
    }

    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid(); 
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public Product(string name, decimal price, int quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }
    }

    static List<Product> Products = new List<Product>();


    static void ManageProducts()
    {
        string productChoice = "";
        while (productChoice != "0")
        {
            Console.WriteLine("===Products Menu ===");
            Console.WriteLine("________________");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. List all products");
            Console.WriteLine("0. Back to the main menu");
            Console.WriteLine("Choose an option");

            productChoice = Console.ReadLine()?.Trim() ?? string.Empty;

            switch (productChoice)
            {
                case "1":
                    AddProduct();
                    break;
                case "2":
                    ListProducts();
                    break;
                case "0":
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again");
                    break;

            }

        }
    }

    static void AddProduct()
    {
        Console.WriteLine("Enter product name");
        string productName = Console.ReadLine()?.Trim() ?? string.Empty;
        while (string.IsNullOrWhiteSpace(productName))
        {
            Console.WriteLine("Product name cannot be empty. Please enter a valid product name:");
            productName = Console.ReadLine()?.Trim() ?? string.Empty;
        }
         

        decimal productPrice;
        Console.WriteLine("Enter product price");
        while (!decimal.TryParse(Console.ReadLine(), out productPrice)|| productPrice<=0)
        {
            Console.WriteLine("Invalid price. Enter positive price.");
        }

        int quantity;
        Console.WriteLine("Enter product quantity");
        while (!int.TryParse(Console.ReadLine(), out quantity) || quantity < 0)
        {
            Console.WriteLine("Invalid quantity. Enter a non-negative number or zero.");
        }

        Product newProduct = new Product(productName, productPrice, quantity);
        Products.Add(newProduct);
    }

    static void ListProducts()
    {
        if (Products.Count == 0)
        {
            Console.WriteLine("No products available.");
            return;
        }
         
        decimal totalInventory = 0;
        Console.WriteLine("\n------------------------------------------------");
        Console.WriteLine($"{"ID (Short)",-12} | {"Name",-15} | {"Price",10} | {"Quantity",10} | {"Total Value",12}"); Console.WriteLine("------------------------------------------------");

        foreach (var p in Products)
        {
            decimal totalItemValue = p.Price * p.Quantity;
            totalInventory += totalItemValue;

            string shortId = p.Id.ToString().Substring(0, 8);
            string displayName = p.Name.Length > 12 ? p.Name.Substring(0, 9) + "..." : p.Name;

            Console.WriteLine($"{shortId,-10} | {displayName,-15} | {p.Price,8:C} | {p.Quantity,8} | {totalItemValue,10:C}");
        }

        Console.WriteLine("------------------------------------------------");  
        Console.WriteLine($"Total Inventory Value: {totalInventory:C}");
    }

}

