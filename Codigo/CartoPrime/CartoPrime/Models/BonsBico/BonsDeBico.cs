using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CartoPrime.Models.BonsBico
{
    public class BonsDeBico
    {
        public TimeBonsDB TimeA { get; set; }
        public TimeBonsDB TimeB { get; set; }
        public int Rodada { get; set; }
    }

    public class TimeBonsDB
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
        public string Color { get; set; } = "#ffffff";
        public List<Athlete> Atletas
        {
            get;
            set;
        }
        public List<Athlete> Reservas { get; set; }
    }
}
