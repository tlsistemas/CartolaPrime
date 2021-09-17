using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CartoPrime.Data.CA.Models
{
    public class AthleteClimbed
    {
        CultureInfo elGR = CultureInfo.CurrentCulture;
        public Athlete Atleta { get; set; }
        public int escalacoes { get; set; }
        public string escalacoesString
        {
            get
            {
                return string.Format(elGR, "{0:0,0}", this.escalacoes).Replace(",", ".") + " Escalações";
            }
        }
        public string clube { get; set; }
        public string clube_nome { get; set; }
        public int clube_id { get; set; }
        public string escudo_clube { get; set; }
        public string posicao { get; set; }
        public string posicao_abreviacao { get; set; }
        public int Count { get; set; }
    }
}
