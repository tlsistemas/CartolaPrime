using System;
using System.Collections.Generic;
using System.Text;

namespace CartoPrime.Data.CA.Models
{
    public class BaseAthletePunctuated
    {
        public String apelido
        {
            get;
            set;
        }

        public string clube_id
        {
            get;
            set;
        }

        public string foto
        {
            get;
            set;
        }

        public int id_jogador
        {
            get;
            set;
        }

        public string pontuacao
        {
            get;
            set;
        }

        public double pontuacao_dou
        {
            get;
            set;
        }

        public string posicao_id
        {
            get;
            set;
        }

        public Scout scout
        {
            get;
            set;
        }

        public string scout_tela { get; set; }
        public string scout_tela_def { get; set; }

        public List<Clube> clubes
        {
            get; set;
        }

        public int rodada
        {
            get;
            set;
        }

        public double variacao_num { get; set; }
        public double media_num { get; set; }
        public double preco_double { get; set; }
        public string min_valorizacao { get; set; }
        public Partida Partida { get; set; }
    }
}
