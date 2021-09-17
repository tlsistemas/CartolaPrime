using System;
using System.Collections.Generic;
using System.Text;

namespace CartoPrime.Models
{
    public class AthleteMarket
    {
        public string nome { get; set; }
        public string apelido { get; set; }
        public string foto { get; set; }
        public int atleta_id { get; set; }
        public int rodada_id { get; set; }
        public int clube_id { get; set; }
        public int posicao_id { get; set; }
        public string posicao_string { get; set; }
        public int status_id { get; set; }
        public string pontos_num { get; set; }
        public double pontos_double { get { return double.Parse(pontos_num.Replace(".", ",")); } }
        public string preco_num { get; set; }
        public double preco_double { get { return double.Parse(preco_num.Replace(".", ",")); } }
        public string variacao_num { get; set; }
        public double variacao_double { get { return double.Parse(variacao_num.Replace(".", ",")); } }
        public double media_num { get; set; }

        public int jogos_num { get; set; }
        public Scout scout { get; set; }
        public string escudo_clube { get; set; }
        public string img_status { get; set; }
        public string scouts { get; set; }
        public string variacoes { get; set; }
        public string Pontos { get; set; }
        public string scout_tela { get; set; }
        public string scout_tela_def { get; set; }
        public string scout_tela_basico { get; set; }
        public string pont_color { get; set; }
        public string msg_scout { get; set; }
        public string min_valorizacao { get; set; }

    }
}
