using System;
using System.Collections.Generic;
using System.Text;

namespace CartoPrime.Models
{
    public class Error
    {
        public String Message { get; set; }
        public Error(String message)
        {
            Message = message;
        }
    }
}
