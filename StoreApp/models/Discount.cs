using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.models
{
    public class Discount
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; } = string.Empty;
        public decimal Percentage { get; set; } 
        public bool IsActive { get; set; } = true;

        public Discount() { }

    }
    
}
