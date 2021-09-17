using System;
using System.Collections.Generic;
using System.Text;

namespace CartoPrime.Application.ViewModel
{
    public class UserTokenRequest
    {
        public String UserID { get; set; }
        public String AccessKey { get; set; }
        public String Nome { get; set; }
        public String Email { get; set; }
        public String CPF { get; set; }
    }
}
