using System;

namespace CartoPrime.Application.ViewModel
{
    public class UserResponse
    {
        public int Id { get; set; }
        public String Key { get; set; }
        public string UserName { get; set; }
        public String Password { get; set; }
        public String Token { get; set; }
        public String Nome { get; set; }
        public String Email { get; set; }
        public String CPF { get; set; }
        public String TipoPrint { get; set; }
        public int Tipo { get; set; }
    }
}
