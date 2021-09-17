using System;
using System.Collections.Generic;
using System.Text;

namespace CartoPrime.Data.CA.Models
{
    public class TeamSlug
    {
        public List<Athlete> atletas
        {
            get;
            set;
        }
        public List<Athlete> reservas { get; set; }
        public TeamFull time { get; set; }
        public string mensagem { get; set; }
        public int rodada_atual { get; set; }
        public string pontos { get; set; }
        public String patrimonio { get; set; }
        public String variacao_pontos { get; set; }
        public double? variacao_patrimonio { get; set; }
        public String capitao_id { get; set; }
        public int esquema_id { get; set; }
        public int GCount { get; set; }
        public int ACount { get; set; }
        public int DSCount { get; set; }
        public int DECount { get; set; }
        public int SGCount { get; set; }
        public string ScoutString { get { return $"DS {DSCount} | DE {DECount} | SG {SGCount}"; } }
    }
}
