using System;
using System.Collections.Generic;
using System.Text;
using StoreApp.methods;

namespace StoreApp.methods
{
    public class Order
    {
        public int Id { get; set; }
        public Customer Customer { get; set; } = null!;
        public List<OrderItem> Items { get; set; } = new();
        public DateTime OrderDate { get; set; }
        public decimal Total => Items.Sum(i => i.Subtotal);

        public Order() { }
        public Order(int id, Customer customer, List<OrderItem> items, DateTime orderDate)
        {
            Id = id;
            Customer = customer;
            Items = items;
            OrderDate = orderDate;
        }
        public class OrderItem
        {
            public Product Product { get; set; } = null!;
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal Subtotal => UnitPrice * Quantity;

            public OrderItem() { }

            public OrderItem(Product product, int quantity, decimal unitPrice)
            {
                Product = product;
                Quantity = quantity;
                UnitPrice = unitPrice;
            }
        }
    }
}
