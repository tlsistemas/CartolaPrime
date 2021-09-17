using System;
using System.Collections.Generic;
using System.Text;

namespace CartoPrime.Data.CA.Models
{
    public class Athlete
    {
        public int atleta_id { get; set; }
        public string nome { get; set; }
        public string apelido { get; set; }
        public string foto { get; set; }
        public string foto140 { get { return foto.Replace("FORMATO", "140x140"); } }
        public string preco_editorial { get; set; }
        public double? variacao_num { get; set; }
        public Scout scout { get; set; }
        public double pontos_num { get; set; }
        public string Pontos { get { return pontos_num.ToString("F2").Replace(",", ".") + "pts"; } set { } }
        public string preco_num { get; set; }
        public double preco_double { get; set; }
        public double? media_num { get; set; }
        public int jogos_num { get; set; }
        public int posicao_id { get; set; }
        public string posicao { get { return Positions.GetPosition(posicao_id); } }
        public int status_id { get; set; }
        public string status { get; set; }
        public int clube_id { get; set; }
        public bool capita { get; set; }
        public double pontos_capita { get; set; }
        public String pontos_capita_string { get; set; }

        public string min_valorizacao { get; set; }
        public Partida Partida { get; set; }

    }
}
