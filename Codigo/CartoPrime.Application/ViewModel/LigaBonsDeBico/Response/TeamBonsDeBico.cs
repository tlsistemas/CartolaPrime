using CartoPrime.Data.CA.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CartoPrime.Application.ViewModel
{
    public class TeamBonsDeBico
    {
        public int Id { get; set; }
        public string Escudo { get; set; }
        public String Name { get; set; }
        public String Slug { get; set; }
        public String Parcial { get; set; }
        public Double ParcialDouble { get; set; }
        public String NameCartoleiro { get; set; }
        public String Jogados { get; set; }
        public Int32 JogadosCont { get; set; }
        public string Color { get; set; }
        public List<Athlete> Atletas
        {
            get;
            set;
        }
        public List<Athlete> Reservas { get; set; }
    }
}
