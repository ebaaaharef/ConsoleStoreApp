using StoreApp.models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreApp
{
    public class Order
    {
        public int Id { get; set; }
        public Customer Customer { get; set; } = null!;
        public List<OrderItem> Items { get; set; } = new();
        public DateTime OrderDate { get; set; }
        // Discount properties
        public string? DiscountCode { get; set; }
        public decimal DiscountPercentage { get; set; }

        public decimal Total => Items.Sum(i => i.Subtotal);

        public decimal DiscountAmount => Math.Round(Total * (DiscountPercentage / 100m), 2);

        public decimal FinalTotal => Total - DiscountAmount;




        public Order() { }

        public Order(int id, Customer customer, List<OrderItem> items, DateTime orderDate)
        {
            Id = id;
            Customer = customer;
            Items = items;
            OrderDate = orderDate;
        }
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