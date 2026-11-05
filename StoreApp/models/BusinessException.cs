using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.models
{
    public class BusinessException:Exception
    {
        public BusinessException(string message) : base(message) { }


    }
}
