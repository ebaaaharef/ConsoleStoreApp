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
                    ManageCustomer();
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
    //=============== manage products methods =====================
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
            Console.WriteLine("3. Search products");
            Console.WriteLine("4. Update product price");
            Console.WriteLine("5. Update product quantity");
            Console.WriteLine("6. Delete product");
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
                case "3":
                    ProductsSearch();
                    break;
                case "4":
                    PriceUpdate();
                    break;
                case "5":
                    quantityUpdate();
                    break;
                case "6":
                    DeleteProduct();
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
    //price update method
    public static void PriceUpdate()
    {if (Products.Count == 0)
        {  Console.WriteLine("No products available yet");
            return;
        }

        Console.WriteLine("Enter product ID to update price:");
        string inputId = Console.ReadLine()?.Trim() ?? string.Empty;

        var inputproduct = Products.FirstOrDefault(p => p.Id.ToString().StartsWith(inputId, StringComparison.OrdinalIgnoreCase));

        if (inputproduct == null)
        { Console.WriteLine($"No product found with ID:{inputId}"); return; }

        decimal newPrice;
        Console.WriteLine($"Current price of {inputproduct.Name} is {inputproduct.Price:C},\n Enter new price:");
        while (!decimal.TryParse(Console.ReadLine(), out newPrice) || newPrice <= 0)
        { Console.WriteLine("Invalid price"); }

        inputproduct.Price = newPrice;
        Console.WriteLine($"Price updated for {inputproduct.Name} to {inputproduct.Price:C}");
    }

    // Update product quantity
    public static void quantityUpdate()
    {
        if (Products.Count == 0)
        {
            Console.WriteLine("No products available yet");
            return;
        }

        Console.WriteLine("Enter product ID to update quantity:");
        string inputId = Console.ReadLine()?.Trim() ?? string.Empty;

        var inputproduct = Products.FirstOrDefault(p => p.Id.ToString().StartsWith(inputId, StringComparison.OrdinalIgnoreCase));

        if (inputproduct == null)
        { Console.WriteLine($"No product found with ID:{inputId}"); return; }

        int newQuantity;
        Console.WriteLine($"Current Quantity of {inputproduct.Name} is {inputproduct.Quantity},\n Enter new quantity:");
        while (!int.TryParse(Console.ReadLine(), out newQuantity) || newQuantity < 0)
        { Console.WriteLine("Invalid Quantity"); }

        inputproduct.Quantity = newQuantity;
        Console.WriteLine($"Quantity updated for {inputproduct.Name} to {inputproduct.Quantity}");
    }

    // Method to delete a product
    public static void DeleteProduct()
    {
        if (Products.Count == 0)
        {
            Console.WriteLine("No products available yet");
            return;
        }

        Console.WriteLine("Enter product ID to update quantity:");
        string inputId = Console.ReadLine()?.Trim() ?? string.Empty;

        var inputproduct = Products.FirstOrDefault(p => p.Id.ToString().StartsWith(inputId, StringComparison.OrdinalIgnoreCase));

        if (inputproduct == null)
        { Console.WriteLine($"No product found with ID:{inputId}"); return; }

       Console.WriteLine($"Are you sure you want to delete {inputproduct.Name}? (y/n)");
        string confirmanswar = Console.ReadLine()?.Trim().ToLower() ?? string.Empty;
        if (confirmanswar == "y")
        {
            Products.Remove(inputproduct);
            Console.WriteLine($"product has been deleted");
        }
        else
        {
            Console.WriteLine("Deletion cancelled");
        }
    }

    // Method to search for products by name

    public static void ProductsSearch()
    {
        Console.WriteLine("Enter name of product you are searching for");
        string searchKeyword = Console.ReadLine()?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(searchKeyword)) 
        {Console.WriteLine("Search keyword cannot be empty"); return; }


        var matchedproducts = Products.
            Where(p => p.Name.Contains(searchKeyword, StringComparison.OrdinalIgnoreCase)).ToList();

        if (matchedproducts.Count == 0)
        {
            Console.WriteLine($"No products found matching '{searchKeyword}'");
            return;
        }

        Console.WriteLine($"\n--- Search Results ({matchedproducts.Count} found) ---");
        Console.WriteLine("------------------------------------------------");
        Console.WriteLine($"{"ID (Short)",-12} | {"Name",-15} | {"Price",10} | {"Quantity",10} | {"Total Value",12}");
        Console.WriteLine("------------------------------------------------");

        foreach (var p in matchedproducts)
        {
            decimal totalItemValue = p.Price * p.Quantity;
            string shortId = p.Id.ToString().Substring(0, 8);
            string displayName = p.Name.Length > 12 ? p.Name.Substring(0, 9) + "..." : p.Name;

            Console.WriteLine($"{shortId,-10} | {displayName,-15} | {p.Price,8:C} | {p.Quantity,8} | {totalItemValue,10:C}");
        }
        Console.WriteLine("------------------------------------------------");






    }

    //================ manage customers methods =====================
     
    public class Customer
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public Customer(string name, string phoneNum)
        {
            Name = name;
            PhoneNumber = phoneNum;
        }
    }
    static List<Customer> Customers = new List<Customer>();

    static void ManageCustomer()
    {
        string productChoice = "";
        while (productChoice != "0")
        {
            Console.WriteLine("===CustomerMenu ===");
            Console.WriteLine("________________");
            Console.WriteLine("1. Add Customer");
            Console.WriteLine("2. List all customers");
            Console.WriteLine("3. Find by name");
            Console.WriteLine("0. Back to the main menu");
            Console.WriteLine("Choose an option");

            productChoice = Console.ReadLine()?.Trim() ?? string.Empty;

            switch (productChoice)
            {
                case "1":
                    AddCustomer();
                    break;
                case "2":
                    ListCustomers();
                    break;
                case "3":
                    CustomerFindByName();
                    break;
                case "0":
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again");
                    break;
            }

        }

        

    }


    static void AddCustomer()
    {
        Console.WriteLine("Enter customer name");
        string customerName = Console.ReadLine()?.Trim() ?? string.Empty;
        while (string.IsNullOrWhiteSpace(customerName) || customerName.All(char.IsDigit))
        {
            Console.WriteLine("Invalid name. Name cannot be empty or consist of numbers only. Please enter a valid customer name:");
            customerName = Console.ReadLine()?.Trim() ?? string.Empty;
        }

        Console.WriteLine("Enter customer phone number");
        string customerPhone = Console.ReadLine()?.Trim() ?? string.Empty;
        while (string.IsNullOrWhiteSpace(customerPhone) ||!customerPhone.All(char.IsDigit) || customerPhone.Length < 9 || customerPhone.Length > 15)
        {
            Console.WriteLine("Invalid phone number.\n it must be digits only and between 9 and 15 digits in format 09~ or +218~.\n Please enter again:");
            customerPhone = Console.ReadLine()?.Trim()?? string.Empty;
        }
         if(Customers.Any(Cp=> Cp.PhoneNumber== customerPhone))
        {
            Console.WriteLine("A customer with this phone number already exists.");
            return;
        }
        

        Customer newCustomer = new Customer(customerName, customerPhone);
        Customers.Add(newCustomer);
    }

    static void ListCustomers()
    {
        if (Customers.Count == 0)
        {
            Console.WriteLine("No customers available yet");
            return;
        }

       
        Console.WriteLine("\n------------------------------------------------");
        Console.WriteLine($"{"ID (Short)",-12} | {"Name",-20} | {"Phone number",15}");
        Console.WriteLine("------------------------------------------------");

        foreach (var c in Customers)
        {
            string shortId = c.Id.ToString().Substring(0, 8);
            string displayName = c.Name.Length > 12 ? c.Name.Substring(0, 9) + "..." : c.Name;

            Console.WriteLine($"{shortId,-10} | {displayName,-20} | {c.PhoneNumber,15}");
        }

        Console.WriteLine("------------------------------------------------");
        
    }


    public static void CustomerFindByName()
    {
        Console.WriteLine("Enter name of customer you want to find");
        string customerName = Console.ReadLine()?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(customerName))
        { Console.WriteLine("Name cannot be empty"); return; }


        var matchedcustomers = Customers.
            Where(c => c.Name.Contains(customerName, StringComparison.OrdinalIgnoreCase)).ToList();

        if (matchedcustomers.Count == 0)
        {
            Console.WriteLine($"No customer with '{customerName}' is found");
            return;
        }

        Console.WriteLine($"\n--- Search Results ({matchedcustomers.Count} found) ---");

        Console.WriteLine("\n------------------------------------------------");
        Console.WriteLine($"{"ID (Short)",-12} | {"Name",-20} | {"Phone number",15}");
        Console.WriteLine("------------------------------------------------");

        foreach (var c in matchedcustomers)
        {
            string shortId = c.Id.ToString().Substring(0, 8);
            string displayName = c.Name.Length > 12 ? c.Name.Substring(0, 9) + "..." : c.Name;

            Console.WriteLine($"{shortId,-10} | {displayName,-20} | {c.PhoneNumber,15}");
        }
            Console.WriteLine("------------------------------------------------");




    }

}








