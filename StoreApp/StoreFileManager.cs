using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.IO;
using StoreApp.models;

namespace StoreApp
{
    internal class StoreFileManager
    {
        private static readonly string FilePath = "store-data.json";
        public static void SaveData(List<Product> products, List<Customer> customers, List<Order> orders, List<Discount> discounts)
        {
            try
            {
                var storeData = new StoreData
                {
                    Products = products,
                    Customers = customers,
                    Orders = orders,
                    Discounts = discounts
                };
                string jsonString = JsonSerializer.Serialize(storeData, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(FilePath, jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data: {ex.Message}");
            }
        }
        public static StoreData LoadData()
        {
            if (!File.Exists(FilePath)) { return new StoreData(); }
            try
            {
                string jsonString = File.ReadAllText(FilePath);
                if (string.IsNullOrWhiteSpace(jsonString)) {return new StoreData();}
                var storeData = JsonSerializer.Deserialize<StoreData>(jsonString);
                if (storeData == null) return new StoreData();

                foreach(var order in storeData.Orders)
                {
                    if (order.Customer != null)
                    {
                        var realCustomer = storeData.Customers.FirstOrDefault(c => c.Id == order.Customer.Id);
                        if (realCustomer != null)
                        { order.Customer = realCustomer; }
                    }

                    if (order.Items != null)
                    {
                        foreach (var item in order.Items) {
                            if (item.Product != null)
                            {
                                var realProduct = storeData.Products.FirstOrDefault(p => p.Id == item.Product.Id);
                                if( realProduct!= null) { item.Product = realProduct; }
                            }
          
                        }
                    }
                }
                return storeData;
            }
            catch (Exception ex)when (ex is JsonException|| ex is IOException)
            {
                try
                {
                    if (File.Exists(FilePath))
                    {
                        string backupPath = FilePath + ".bak";
                        File.Copy(FilePath, backupPath, true);
                        Console.WriteLine($"Corrupted file backed up to {backupPath}");
                    }
                }
                catch { }
                return new StoreData();
            }
        }

    }
}
