using System;
using System.Collections.Generic;
using System.Text;

namespace CartoPrime.Data.CA.Models
{
    public class AtletaTime
    {
        public String variacao_pontos { get; set; }
        public String variacao_patrimonio { get; set; }
        public String capitao_id { get; set; }
        public List<Athlete> atletas
        {
            get;
            set;
        }

        public List<Athlete> reservas { get; set; }

        public string esquema_id
        {
            get;
            set;
        }

        public string patrimonio
        {
            get;
            set;
        }

        public string pontos
        {
            get;
            set;
        }

        public int rodada_atual { get; set; }

        public int total_ligas { get; set; }
        public int total_ligas_matamata { get; set; }
        public List<object> servicos { get; set; }

        public Time time
        {
            get;
            set;
        }

        public string valor_time
        {
            get;
            set;
        }

        public AtletaTime()
        {
        }
    }
}
