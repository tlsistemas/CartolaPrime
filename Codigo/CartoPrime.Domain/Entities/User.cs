using System;
using System.Collections.Generic;
using System.Text;
using TM.Utils.Bases;

namespace CartoPrime.Domain.Entities
{
    public class User : EntityBase
    {
        public String UserName { get; set; }
        public String Password { get; set; }
        public String Token { get; set; }
        public String Nome { get; set; }
        public String Email { get; set; }
        public String CPF { get; set; }
        public int Tipo { get; set; }
        public String TipoPrint 
        { 
            get 
            {

                return Tipo == 1 ? "Administrador" : "Operador";
            }
        }
    }
}