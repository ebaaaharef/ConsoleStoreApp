using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.models
{
    public class Customer
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public Customer() { }
        public Customer(string name, string phoneNum)
        {
            Name = name;
            PhoneNumber = phoneNum;
        }
    }
}
