using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CartoPrime.Models
{
    public class TimesBonsDeBico
    {
        [PrimaryKey]
        public int time_id { get; set; }
        public string nome { get; set; }
        public string nome_cartola { get; set; }
        public string slug { get; set; }
        //public string facebook_id { get; set; }
        public string url_escudo_png { get; set; }
        public string url_escudo_svg { get; set; }
        public string url_escudo_placeholder_png { get; set; }
        public string foto_perfil { get; set; }
        public bool assinante { get; set; }
        public String capitao_id { get; set; }

        public byte[] imageTime { get; set; }
        public int esquema_id { get; set; }
        public int clube_id { get; set; }
        public string clube_string { get; set; }
        public string rodada_time_id { get; set; }
        public int temporada_inicial { get; set; }
        public string pontos { get; set; }
        public double pontos_double { get; set; }

        public string CartoleiroEsquema { get; set; }
        public string TempoCartola { get; set; }
        public string ClubeString { get; set; }
        public string assinanteCaminho { get; set; }
        public string jogados { get; set; }
        public string posicao { get; set; }
        public int liga_id { get; set; }
        public string foto_clube { get; set; }
        public int jogados_cont { get; set; }

        public string valor_time { get; set; }

        public String patrimonio { get; set; }
        public String variacao_pontos { get; set; }
        public String variacao_patrimonio { get; set; }
    }

    public class TimesBonsDeBicoTela
    {
        public int time_id { get; set; }
        public string nome { get; set; }
        public string nome_cartola { get; set; }
        public string slug { get; set; }
        public string facebook_id { get; set; }
        public string url_escudo_png { get; set; }
        public string url_escudo_svg { get; set; }
        public string url_escudo_placeholder_png { get; set; }
        public string foto_perfil { get; set; }
        public bool assinante { get; set; }

        public byte[] imageTime { get; set; }
        public int esquema_id { get; set; }
        public int clube_id { get; set; }
        public string rodada_time_id { get; set; }
        public int temporada_inicial { get; set; }
        public string pontos { get; set; }
        public double pontos_double { get; set; }
        public string CartoleiroEsquema { get; set; }
        public string TempoCartola { get; set; }
        public string ClubeString { get; set; }
        public string assinanteCaminho { get; set; }
        public string jogados { get; set; }
        public int jogados_cont { get; set; }
        public string posicao { get; set; }
        public ImageSource foto_clube { get; set; }
        public String patrimonio { get; set; }
        public String variacao_pontos { get; set; }
        public String variacao_patrimonio { get; set; }
    }
}
