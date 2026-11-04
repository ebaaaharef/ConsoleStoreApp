using System;
using StoreApp;
using StoreApp.models;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.InteropServices;



class Program 
{

    static void Main(string[] args)
    {
        // sorry for the misdirecting, I got confused between level 7 & 10 
        //Products = StoreFileManager.LoadProducts();
        //Customers = StoreFileManager.LoadCustomers();
        //Orders = StoreFileManager.LoadOrders();

        MenuSwitchRun();

    
    }

    static void MenuDisplay()
    {
        Console.WriteLine("===My Store ===");
        Console.WriteLine("________________");
        Console.WriteLine("1.Products");
        Console.WriteLine("2.Customers");
        Console.WriteLine("3.Orders");
        Console.WriteLine("4.Discounts");// level 8
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
                    ManageOrders();
                    break;
                case "4":
                    ManageDiscount();
                    break;

                case "0":
                    //StoreFileManager.saveProducts(Products);
                    //StoreFileManager.saveCustomers(Customers);
                    //StoreFileManager.saveOrders(Orders);
                    Console.WriteLine("Thank you for using MyStore waiting for you once again!");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again");
                    break;

            }


        }
    }
    

    //=============== manage products methods =====================

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
            Console.WriteLine("7. Filter products by category");
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
                case "7":
                    FilterByCategory();
                    break;
                case "0":
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again");
                    break;

            }

        }
    }

    //!!!ConsoleUtils  used
    static void AddProduct()
    {
        string productName = InputHelper.ReadNonEmptyString("Enter product name:");
        decimal productPrice = InputHelper.ReadDecimal("Enter product price:");
        int quantity = InputHelper.ReadInt("Enter product quantity:");

        var categories = Enum.GetValues<Category>();
        Console.WriteLine("Select Category:");
        for (int i = 0; i < categories.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {categories[i]}");
        }

        int choice;
        while (true)
        {
            choice = InputHelper.ReadInt("Choose category number: ", 1);

            if (choice <= categories.Length)
            {
                break; 
            }

            Console.WriteLine($"Invalid choice. Please enter a number between 1 and {categories.Length}.");
        }

        Category selectedCategory = categories[choice - 1];

        Product newProduct = new Product(productName, productPrice, quantity, selectedCategory);
        Products.Add(newProduct);

       // StoreFileManager.saveProducts(Products);
    }

    static void ListProducts()
    {
        if (Products.Count == 0)
        {
            Console.WriteLine("No products available.");
            return;
        }
         
        decimal totalInventory = 0;
        Console.WriteLine("\n-------------------------------------------------------------------------");
        Console.WriteLine($"{"ID (Short)",-10} | {"Name",-15} | {"Category",-12} | {"Price",10} | {"Quantity",8} | {"Total",10}");
        Console.WriteLine("-------------------------------------------------------------------------");
        foreach (var p in Products)
        {
            decimal totalItemValue = p.Price * p.Quantity;
            

            string shortId = p.Id.ToString().Substring(0, 8);
            string displayName = p.Name.Length > 12 ? p.Name.Substring(0, 9) + "..." : p.Name;

            Console.WriteLine($"{shortId,-10} | {displayName,-15} | {p.Category,-12} | {p.Price,8:C} | {p.Quantity,8} | {totalItemValue,8:C}");
        }

        Console.WriteLine("Inventory Breakdown by Category:");
        var categoryBreakdown = Products
            .GroupBy(p => p.Category)
            .Select(g => new { Category = g.Key, TotalValue = g.Sum(p => p.Price * p.Quantity) });

        foreach (var item in categoryBreakdown)
        {
            Console.WriteLine($"- {item.Category,-12}: {item.TotalValue,10:C}");
        }

        
        totalInventory = Products.Sum(p => p.Price * p.Quantity);
        Console.WriteLine("-------------------------------------------------------------------------");
        Console.WriteLine($"Total Inventory Value: {totalInventory:C}");
        Console.WriteLine("-------------------------------------------------------------------------");
    }
    //price update method
    //!!!ConsoleUtils  used
    public static void PriceUpdate()
    {
        if (Products.Count == 0)
        {
            Console.WriteLine("No products available.");
            return;
        }

        Console.WriteLine("\n-------------------------------------------------------------------------");
        Console.WriteLine($"{"ID (Short)",-10} | {"Name",-15} | {"Category",-12} | {"Price",10} | {"Quantity",8} | {"Total",10}");
        Console.WriteLine("-------------------------------------------------------------------------");

        foreach (var p in Products)
        {
            decimal totalItemValue = p.Price * p.Quantity;
            string shortId = p.Id.ToString().Substring(0, Math.Min(8, p.Id.ToString().Length));
            string displayName = p.Name.Length > 12 ? p.Name.Substring(0, 9) + "..." : p.Name;

            Console.WriteLine($"{shortId,-10} | {displayName,-15} | {p.Category,-12} | {p.Price,8:C} | {p.Quantity,8} | {totalItemValue,8:C}");
        }

        Console.WriteLine("-------------------------------------------------------------------------");

        Console.WriteLine("Inventory Breakdown by Category:");
        var categoryBreakdown = Products
            .GroupBy(p => p.Category)
            .Select(g => new { Category = g.Key, TotalValue = g.Sum(p => p.Price * p.Quantity) });

        foreach (var item in categoryBreakdown)
        {
            Console.WriteLine($"- {item.Category,-12}: {item.TotalValue,10:C}");
        }

        decimal overallTotal = Products.Sum(p => p.Price * p.Quantity);
        Console.WriteLine("-------------------------------------------------------------------------");
        Console.WriteLine($"Total Inventory Value: {overallTotal:C}");
        Console.WriteLine("-------------------------------------------------------------------------");
        //StoreFileManager.saveProducts(Products);
    }

    // Update product quantity
    //!!!ConsoleUtils  used
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

        int newQuantity = InputHelper.ReadInt($"Current Quantity of {inputproduct.Name} is {inputproduct.Quantity},\nEnter new quantity:");

        inputproduct.Quantity = newQuantity;
        Console.WriteLine($"Quantity updated for {inputproduct.Name} to {inputproduct.Quantity}");
        //StoreFileManager.saveProducts(Products);
    }

    // Method to delete a product
    //!!!ConsoleUtils  used
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

        bool confirm = InputHelper.ReadYesNo($"Are you sure you want to delete {inputproduct.Name}?");
        if (confirm)
        {
            Products.Remove(inputproduct);
            Console.WriteLine("Product has been deleted successfully.");
        }
        else
        {
            Console.WriteLine("Deletion cancelled.");
        }
        //StoreFileManager.saveProducts(Products);
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
    // Method to filter products by category
    static void FilterByCategory()
    {
        if (Products.Count == 0)
        {
            Console.WriteLine("No products available yet.");
            return;
        }

        var categories = Enum.GetValues<Category>();
        Console.WriteLine("Select Category to Filter:");
        for (int i = 0; i < categories.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {categories[i]}");
        }

        int choice;
        while (true)
        {
            choice = InputHelper.ReadInt("Choose category number: ", 1);
            if (choice <= categories.Length)
            {
                break;
            }
            Console.WriteLine($"Invalid choice. Please enter a number between 1 and {categories.Length}.");
        }

        Category selectedCategory = categories[choice - 1];
        var filteredProducts = Products.Where(p => p.Category == selectedCategory).ToList();

        if (filteredProducts.Count == 0)
        {
            Console.WriteLine($"No products found in category '{selectedCategory}'.");
            return;
        }

        Console.WriteLine($"\n--- Products in Category: {selectedCategory} ({filteredProducts.Count} found) ---");
        Console.WriteLine("-------------------------------------------------------------------------");
        Console.WriteLine($"{"ID (Short)",-10} | {"Name",-15} | {"Category",-12} | {"Price",10} | {"Quantity",8} | {"Total",10}");
        Console.WriteLine("-------------------------------------------------------------------------");

        foreach (var p in filteredProducts)
        {
            decimal totalItemValue = p.Price * p.Quantity;
            string shortId = p.Id.ToString().Substring(0, Math.Min(8, p.Id.ToString().Length));
            string displayName = p.Name.Length > 12 ? p.Name.Substring(0, 9) + "..." : p.Name;

            Console.WriteLine($"{shortId,-10} | {displayName,-15} | {p.Category,-12} | {p.Price,8:C} | {p.Quantity,8} | {totalItemValue,8:C}");
        }
        Console.WriteLine("-------------------------------------------------------------------------");
    }

    //================ manage customers methods =====================


    static List<Customer> Customers = new List<Customer>();

    static void ManageCustomer()
    {
        string userChoice = "";
        while (userChoice != "0")
        {
            Console.WriteLine("===CustomerMenu ===");
            Console.WriteLine("________________");
            Console.WriteLine("1. Add Customer");
            Console.WriteLine("2. List all customers");
            Console.WriteLine("3. Find by name");
            Console.WriteLine("0. Back to the main menu");
            Console.WriteLine("Choose an option");

            userChoice = Console.ReadLine()?.Trim() ?? string.Empty;

            switch (userChoice)
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
        string customerName = InputHelper.ReadNonEmptyString("Enter customer name:");

        while (customerName.All(char.IsDigit))
        {
            Console.WriteLine("Invalid name. Name cannot consist of numbers only. Please enter a valid customer name:");
            customerName = InputHelper.ReadNonEmptyString("Enter customer name:");
        }

        string customerPhone = InputHelper.ReadPhoneNumber("Enter customer phone number:");

        if (Customers.Any(Cp => Cp.PhoneNumber == customerPhone))
        {
            Console.WriteLine("A customer with this phone number already exists.");
            return;
        }


        Customer newCustomer = new Customer(customerName, customerPhone);
        Customers.Add(newCustomer);
        //StoreFileManager.saveCustomers(Customers);
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


        //==============order section=========================


 
    static List<Order> Orders = new List<Order>();
    static int nextOrderId = 1;

    static void ManageOrders()
    {
        string userChoice = "";
        while (userChoice != "0")
        {
            Console.WriteLine("===Order Menu ===");
            Console.WriteLine("________________");
            Console.WriteLine("1. Create Order");
            Console.WriteLine("2. List orders");
            Console.WriteLine("3. View order details");
            Console.WriteLine("0. Back to the main menu");
            Console.WriteLine("Choose an option:");

           userChoice = Console.ReadLine()?.Trim() ?? string.Empty;

            switch (userChoice)
            {
                case "1":
                    CreateOrder();
                    break;
                case "2":
                    ListOrders();
                    break;
                case "3":
                    ViewOrderDetails();
                    break;
                case "0":
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again");
                    break;
            }

        }
    }
    //!!!ConsoleUtils  used
    public static void CreateOrder()
    {
        if(Customers.Count ==0)
        {
            Console.WriteLine("No customers available. Please add a customer first");
            return;
        }
        if (Products.Count == 0)
        {
            Console.WriteLine("No Product available");
            return;
        }


        Console.WriteLine("Enter customer ID for the order or '0' to cansle the order");
        string customerIdInput = Console.ReadLine()?.Trim() ?? string.Empty;

        if (customerIdInput == "0")
        {
            Console.WriteLine("Order creation cancelled.");
            return;
        }

         var customer= Customers.FirstOrDefault(c=>c.Id.ToString().Equals(customerIdInput, StringComparison.OrdinalIgnoreCase)||
            c.Id.ToString().Substring(0,8).Equals(customerIdInput, StringComparison.OrdinalIgnoreCase));
        if (customer == null)
        {
            Console.WriteLine($"No customer found with ID: {customerIdInput}");
            return;
        }

        Console.WriteLine($"selected customer: {customer.Name}");

        List<OrderItem> tempItems = new List<OrderItem>();
        string productIdInput = "";
        while (true)
        {
            Console.WriteLine("Enter product ID to add to order or '0' to finish adding products");
            productIdInput = Console.ReadLine()?.Trim() ?? string.Empty;
            if (productIdInput == "0")
            {
                break;
            }
            var product = Products.FirstOrDefault(p => p.Id.ToString().Equals(productIdInput, StringComparison.OrdinalIgnoreCase)||
            p.Id.ToString().Substring(0,8).Equals(productIdInput, StringComparison.OrdinalIgnoreCase));// preventing parial ID match 
            if (product == null)
            {
                Console.WriteLine($"No product found with ID: {productIdInput}");
                continue;
            }

            int quantityAvailable = InputHelper.ReadInt($"Enter quantity for {product.Name} (Available: {product.Quantity}):", 1);

            var existingItem = tempItems.FirstOrDefault(i => i.Product.Id == product.Id);
            int orderedQuantity = existingItem?.Quantity ?? 0;
            int requestedQuantity = orderedQuantity + quantityAvailable;

            if (requestedQuantity > product.Quantity)
            {
                Console.WriteLine($"Insufficient stock for {product.Name}. Available quantity: {product.Quantity - orderedQuantity}");
                continue;
            }

            if (existingItem != null)
            {
                existingItem.Quantity += quantityAvailable;
            }
            else
            {
                tempItems.Add(new OrderItem(product, quantityAvailable, product.Price));
            }

            Console.WriteLine($"Added {quantityAvailable} of {product.Name} to the order.");
        }

        if (tempItems.Count == 0)
        { Console.WriteLine("No items were added. Order cancelled."); return;}

        // Discount application
        string? appliedDiscountCode = null;
        decimal discountPercentage = 0m;

        while (true)
        {
            Console.WriteLine("Do you have a discount code? press enter to skip or type the code:");
            string inputCode = Console.ReadLine()?.Trim() ?? string.Empty;

            if (string.IsNullOrEmpty(inputCode))
            {
                break; 
            }

            var discountinputcode= Discounts.FirstOrDefault(d => d.Code.Equals(inputCode, StringComparison.OrdinalIgnoreCase));

            if (discountinputcode == null)
            {
                Console.WriteLine($"Unknown discount code '{inputCode}'. Please try again or leave blank to skip.");
                continue;
            }

            if (!discountinputcode.IsActive)
            {
                Console.WriteLine($"Discount code '{discountinputcode.Code}' is inactive. Please try another code or press enter to skip.");
                continue;
            }

            appliedDiscountCode = discountinputcode.Code;
            discountPercentage = discountinputcode.Percentage;
            Console.WriteLine($"Discount applied successfully: {discountPercentage}% off.");
            break;
        }

        decimal subtotal = tempItems.Sum(i => i.Subtotal);
        decimal discountAmount = Math.Round(subtotal * (discountPercentage / 100m), 2);
        decimal finalTotal = subtotal - discountAmount;

        Console.WriteLine($"\n--- Order Summary ---");
        Console.WriteLine($"Customer: {customer.Name}");
        Console.WriteLine($"Subtotal: {subtotal:C}");
        if (!string.IsNullOrEmpty(appliedDiscountCode))
        {
            Console.WriteLine($"Discount: {appliedDiscountCode} (-{discountPercentage}%) -{discountAmount:C}");
        }
        Console.WriteLine($"Final Total: {finalTotal:C}");

        bool confirm = InputHelper.ReadYesNo("Are you sure you want to confirm this order?");

        if (confirm)
        {
            foreach (var item in tempItems)
            {
                item.Product.Quantity -= item.Quantity;
            }

            int newOrderId = Orders.Count > 0 ? Orders.Max(o => o.Id) + 1 : 1;

            Order newOrder = new Order
            {
                Id = newOrderId,
                Customer = customer,
                Items = tempItems,
                OrderDate = DateTime.Now,
                DiscountCode = appliedDiscountCode,
                DiscountPercentage = discountPercentage
            };

            Orders.Add(newOrder);
            Console.WriteLine($"\nOrder #{newOrder.Id} creation completed successfully!");
        }
        else
        {
            Console.WriteLine("Order cancelled. Stock remains the same.");
        }

        // StoreFileManager.saveOrders(Orders);
        // StoreFileManager.saveProducts(Products);
    }

    public static void ListOrders()
    {
        if (Orders.Count == 0)
        {
            Console.WriteLine("No orders have been created yet");
            return;
        }
        Console.WriteLine("\n--------------------------------------------------------------------------------");
        Console.WriteLine($"{" order ID",-6} | {"Customer Name",-20} | {"Date",-20}| {"Paid Amount",15}");
        Console.WriteLine("----------------------------------------------------------------------------------");

        foreach (var order in Orders)
        {
            Console.WriteLine($"{order.Id,-6} | {order.Customer.Name,-20} | {order.OrderDate,-20:yyyy-MM-dd HH:mm} | {order.FinalTotal,15:C}");

        }
        Console.WriteLine("\n--------------------------------------------------------------------------------");



    }

    public static void ViewOrderDetails()
    {
        string orderIdInput = InputHelper.ReadNonEmptyString("Enter order ID to view details or '0' to cancel:");

        if (orderIdInput == "0") return;

        var order = Orders.FirstOrDefault(o => o.Id.ToString() == orderIdInput);

        if (order == null)
        {
            Console.WriteLine($"No order found with ID: {orderIdInput}");
            return;
        }

        Console.WriteLine("\n================================================");
        Console.WriteLine($"                    RECEIPT                     ");
        Console.WriteLine("================================================");
        Console.WriteLine($"Order ID     : #{order.Id}");
        Console.WriteLine($"Customer     : {order.Customer.Name} (Phone: {order.Customer.PhoneNumber})");
        Console.WriteLine($"Date & Time  : {order.OrderDate:yyyy-MM-dd HH:mm:ss}");
        Console.WriteLine("------------------------------------------------");
        Console.WriteLine($"{"Product",-15} | {"Qty",-5} | {"Unit Price",-12} | {"Subtotal",-10}");
        Console.WriteLine("------------------------------------------------");

        foreach (var item in order.Items)
        {
            Console.WriteLine($"{item.Product.Name,-15} | {item.Quantity,-5} | {item.UnitPrice,-12:C} | {item.Subtotal,-10:C}");
        }

        Console.WriteLine("------------------------------------------------");
        Console.WriteLine($"Subtotal     : {order.Total:C}");

        if (!string.IsNullOrEmpty(order.DiscountCode))
        {
            Console.WriteLine($"Discount     : {order.DiscountCode} (-{order.DiscountPercentage}%) -{order.DiscountAmount:C}");
        }

        Console.WriteLine($"TOTAL        : {order.FinalTotal:C}");
        Console.WriteLine("================================================");
    }

    // ==================== DISCOUNT SYSTEM (LEVEL 8) ====================
    static List<Discount> Discounts = new List<Discount>();
    public static void ManageDiscount()
    {  string discountChoice="";
        while (discountChoice != "0")
        {
            Console.WriteLine("===Discount Menu ===");
            Console.WriteLine("________________");
            Console.WriteLine("1. Add code");
            Console.WriteLine("2. List codes");
            Console.WriteLine("3. Activate/Deactivate code");
            Console.WriteLine("0. Back to the main menu");
            Console.WriteLine("Choose an option:");
            discountChoice = Console.ReadLine()?.Trim() ?? string.Empty;
            switch (discountChoice)
            {
                case "1":
                    AddDiscountCode();
                    break;
                case "2":
                    ListDiscountCodes();
                    break;
                case "3":
                    activateOrDeactivate();
                    break;
                case "0":
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again");
                    break;
            }
        }
   
    }

    public static void AddDiscountCode()
    {
        string code = InputHelper.ReadNonEmptyString("Enter discount code:").ToUpper();

        if (Discounts.Any(d=> d.Code.Equals(code, StringComparison.OrdinalIgnoreCase))) 
        {
            Console.WriteLine("A discount code with this name already exists.");
            return;
        }

        decimal percentage = InputHelper.ReadDecimal("Enter dicount percentage (1 to 100)",1,100);

        Discounts.Add(new Discount
        {  
            Code = code,
            Percentage = percentage,
            IsActive = true
        });

        Console.WriteLine($"Discount code '{code}' added successfully!");

    }

    public static void ListDiscountCodes()
    {
        if (Discounts.Count == 0)
        {
            Console.WriteLine("No discount codes available.");
            return;
        }

            Console.WriteLine("\n--------------------------------------------------");
            Console.WriteLine($"{"ID",-5} | {"Code",-12} | {"Percentage",-10} | {"Status",-8}");
            Console.WriteLine("--------------------------------------------------");
            foreach (var d in Discounts)
            {
                string shortId = d.Id.ToString().Substring(0, Math.Min(8, d.Id.ToString().Length));
                string status = d.IsActive ? "Active" : "Inactive";
                Console.WriteLine($"{shortId,-5} | {d.Code,-12} | {d.Percentage,-8}% | {status,-8}");
            }
            Console.WriteLine("--------------------------------------------------");
     }
    

    public static void activateOrDeactivate()
    {
        string inputId = InputHelper.ReadNonEmptyString("Enter Dicount ID to toggle status");
        var discount = Discounts.FirstOrDefault(d =>
        d.Id.ToString().Equals(inputId, StringComparison.OrdinalIgnoreCase) ||
        d.Id.ToString().Substring(0, Math.Min(8, d.Id.ToString().Length)).Equals(inputId, StringComparison.OrdinalIgnoreCase));

        if (discount == null)
        {
            Console.WriteLine($"Discount with ID {inputId} was not found.");
            return;
        }

        discount.IsActive = !discount.IsActive;
        string stateStr = discount.IsActive ? "activated" : "deactivated";
        Console.WriteLine($"Discount code '{discount.Code}' has been {stateStr}.");
    }

}










