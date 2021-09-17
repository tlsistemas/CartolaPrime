using System;
using System.Collections.Generic;
using System.Text;

namespace CartoPrime.Domain.EntitiesCartola.Team
{
    public class TeamModel
    {
        //public List<Athlete> reservas { get; set; }
        public TeamInforsModel time { get; set; }
        public string mensagem { get; set; }
        public int rodada_atual { get; set; }
        public string pontos { get; set; }
        public string pontos_rodada
        {
            get
            {
                var _pont = pontos == null ? 0.00 : double.Parse(pontos.Replace(".",",").ToString());
                return _pont.ToString("F2").Replace(",", ".") + "pts"; ;

            }
            set { }
        }
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
