using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.models
{
    internal class StoreData
    {
        public List<Product> Products { get; set; } = new();
        public List<Customer> Customers { get; set; } = new();
        public List<Order> Orders { get; set; } = new();
        public List<Discount> Discounts { get; set; } = new();




    }
}
