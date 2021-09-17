using System;
using System.Collections.Generic;
using System.Text;

namespace CartoPrime.Models.Atletas
{
    public class Substituicoes
    {
        public Entrou entrou { get; set; }
        public Saiu saiu { get; set; }
        public int posicao_id { get; set; }
    }

    public class Entrou
    {
        public object scout { get; set; }
        public int atleta_id { get; set; }
        public int rodada_id { get; set; }
        public int clube_id { get; set; }
        public int posicao_id { get; set; }
        public int status_id { get; set; }
        public int pontos_num { get; set; }
        public double preco_num { get; set; }
        public double variacao_num { get; set; }
        public double media_num { get; set; }
        public int jogos_num { get; set; }
        public string slug { get; set; }
        public string apelido { get; set; }
        public string apelido_abreviado { get; set; }
        public string nome { get; set; }
        public string foto { get; set; }
    }

    public class Saiu
    {
        public object scout { get; set; }
        public int atleta_id { get; set; }
        public int rodada_id { get; set; }
        public int clube_id { get; set; }
        public int posicao_id { get; set; }
        public int status_id { get; set; }
        public int pontos_num { get; set; }
        public double preco_num { get; set; }
        public double variacao_num { get; set; }
        public double media_num { get; set; }
        public int jogos_num { get; set; }
        public string slug { get; set; }
        public string apelido { get; set; }
        public string apelido_abreviado { get; set; }
        public string nome { get; set; }
        public string foto { get; set; }
    }
}
