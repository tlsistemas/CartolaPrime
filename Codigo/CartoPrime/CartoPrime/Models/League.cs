using CartoPrime.Helpers;
using MonkeyCache.FileStore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CartoPrime.Models
{

    public class League
    {
        public int liga_id { get; set; }
        public int? time_dono_id { get; set; }
        public int? clube_id { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public string slug { get; set; }
        public string tipo { get; set; }
        public bool mata_mata { get; set; }
        public bool editorial { get; set; }
        public bool patrocinador { get; set; }
        public string criacao { get; set; }
        public DateTime criacao_date { get { return DateTime.Parse(criacao); } }
        public bool sem_capitao { get; set; }
        public int tipo_flamula { get; set; }
        public int? tipo_estampa_flamula { get; set; }
        public int? tipo_adorno_flamula { get; set; }
        public string cor_primaria_estampa_flamula { get; set; }
        public string cor_secundaria_estampa_flamula { get; set; }
        public string cor_borda_flamula { get; set; }
        public string cor_fundo_flamula { get; set; }
        public string url_flamula_svg { get; set; }
        public string url_flamula_png { get; set; }
        public int? tipo_trofeu { get; set; }
        public int? cor_trofeu { get; set; }
        public string url_trofeu_svg { get; set; }
        public string url_trofeu_png { get; set; }
        public int inicio_rodada { get; set; }
        public object fim_rodada { get; set; }
        public object quantidade_times { get; set; }
        public bool sorteada { get; set; }
        public string imagem { get; set; }
        public int? mes_ranking_num { get; set; }
        public int? mes_variacao_num { get; set; }
        public int? camp_ranking_num { get; set; }
        public int? camp_variacao_num { get; set; }
        public object capitao_ranking_num { get; set; }
        public object capitao_variacao_num { get; set; }
        public int? total_times_liga { get; set; }
        public object vagas_restantes { get; set; }
        public int? total_amigos_na_liga { get; set; }
        public string type_league
        {
            get
            {
                if (mata_mata)
                    return "Mata-Mata";
                else
                    return "Clássica";
            }
        }
        public string My { 
            get 
            {
                var UserCA = Barrel.Current.Get<UserCA>(key: UrlCartola._timeAuth);
                if (UserCA.id == time_dono_id)
                    return "Privada";
                return "";
            } 
        }
    }

    public class BaseLeague
    {
        public List<League> ligas { get; set; }
    }
}
