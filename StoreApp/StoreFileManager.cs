using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.IO;
using StoreApp.methods;

namespace StoreApp
{
    internal class StoreFileManager
    {
        private static readonly string ProductsFile = "products.json";
        private static readonly string CustomersFile = "customers.json";
        private static readonly string OrdersFile = "orders.json";

        // ====save and load products=====
        public static void saveProducts(List<Product> products)
        { 
            string jsonString = JsonSerializer.Serialize(products,new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(ProductsFile, jsonString);
        }

        public static List<Product> LoadProducts()
        {
            if (!File.Exists(ProductsFile)) return new List<Product>();
            string jsonString = File.ReadAllText(ProductsFile);
            return JsonSerializer.Deserialize<List<Product>>(jsonString) ?? new List<Product>();
        }

        // ====save and load customers=====
        public static void saveCustomers(List<Customer> customers)
        {
            string jsonString = JsonSerializer.Serialize(customers , new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(CustomersFile, jsonString);
        }

        public static List<Customer> LoadCustomers()
        {
            if (!File.Exists(CustomersFile)) return new List<Customer>();
            string jsonString = File.ReadAllText(CustomersFile);
            return JsonSerializer.Deserialize<List<Customer>>(jsonString) ?? new List<Customer >();
        }

        // ====save and load orders=====

        public static void saveOrders(List<Order> orders) {
            string jsonString = JsonSerializer.Serialize(orders, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(OrdersFile, jsonString);
        }
        public static List<Order> LoadOrders()
        {
            if (!File.Exists(OrdersFile)) return new List<Order>();
            string jsonString = File.ReadAllText(OrdersFile);
            return JsonSerializer.Deserialize<List<Order>>(jsonString) ?? new List<Order>();  
        }
    }
}
