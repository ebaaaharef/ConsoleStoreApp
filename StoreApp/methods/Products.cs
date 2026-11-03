using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.methods
{
    
        public class Product
        {
            public Guid Id { get; set; } = Guid.NewGuid();
            public string Name { get; set; } = string.Empty;
            public decimal Price { get; set; }
            public int Quantity { get; set; }

            public Product() { }

            public Product(string name, decimal price, int quantity)
            {
                Name = name;
                Price = price;
                Quantity = quantity;
            }
        }

    
}
