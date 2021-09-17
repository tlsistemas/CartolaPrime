using CartoPrime.Helpers;
using MonkeyCache.FileStore;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CartoPrime.Models
{

    public class BaseTeamLeague
    {
        public Destaques destaques { get; set; }
        public Liga liga { get; set; }
        public bool membro { get; set; }
        public int pagina { get; set; }
        public TimeDono time_dono { get; set; }
        public TimeUsuario time_usuario { get; set; }
        public List<Time> times { get; set; }
    }
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Lanterninha
    {
        public bool assinante { get; set; }
        public bool cadastro_completo { get; set; }
        public bool simplificado { get; set; }
        public int clube_id { get; set; }
        public int esquema_id { get; set; }
        public int tipo_adorno { get; set; }
        public int tipo_camisa { get; set; }
        public int tipo_escudo { get; set; }
        public int tipo_estampa_camisa { get; set; }
        public int tipo_estampa_escudo { get; set; }
        public int patrocinador1_id { get; set; }
        public int patrocinador2_id { get; set; }
        public Int64? time_id { get; set; }
        public string cor_borda_escudo { get; set; }
        public string cor_camisa { get; set; }
        public string cor_fundo_escudo { get; set; }
        public string foto_perfil { get; set; }
        public string globo_id { get; set; }
        public string nome { get; set; }
        public string nome_cartola { get; set; }
        public string slug { get; set; }
        public string url_camisa_png { get; set; }
        public string url_camisa_svg { get; set; }
        public string url_escudo_png { get; set; }
        public string url_escudo_svg { get; set; }
        public string facebook_id { get; set; }
        public string rodada_time_id { get; set; }
        public string cor_primaria_estampa_camisa { get; set; }
        public string cor_primaria_estampa_escudo { get; set; }
        public string cor_secundaria_estampa_camisa { get; set; }
        public string cor_secundaria_estampa_escudo { get; set; }
        public object sorteio_pro_num { get; set; }
        public int temporada_inicial { get; set; }
    }

    public class Patrimonio
    {
        public bool assinante { get; set; }
        public bool cadastro_completo { get; set; }
        public bool simplificado { get; set; }
        public int clube_id { get; set; }
        public int esquema_id { get; set; }
        public int tipo_adorno { get; set; }
        public int tipo_camisa { get; set; }
        public int tipo_escudo { get; set; }
        public int tipo_estampa_camisa { get; set; }
        public int tipo_estampa_escudo { get; set; }
        public int patrocinador1_id { get; set; }
        public int patrocinador2_id { get; set; }
        public int time_id { get; set; }
        public string cor_borda_escudo { get; set; }
        public string cor_camisa { get; set; }
        public string cor_fundo_escudo { get; set; }
        public string foto_perfil { get; set; }
        public string globo_id { get; set; }
        public string nome { get; set; }
        public string nome_cartola { get; set; }
        public string slug { get; set; }
        public string url_camisa_png { get; set; }
        public string url_camisa_svg { get; set; }
        public string url_escudo_png { get; set; }
        public string url_escudo_svg { get; set; }
        public string facebook_id { get; set; }
        public int rodada_time_id { get; set; }
        public string cor_primaria_estampa_camisa { get; set; }
        public string cor_primaria_estampa_escudo { get; set; }
        public string cor_secundaria_estampa_camisa { get; set; }
        public string cor_secundaria_estampa_escudo { get; set; }
        public object sorteio_pro_num { get; set; }
        public int temporada_inicial { get; set; }
    }

    [JsonObject("rodada")]
    public class RodadaLeague
    {
        public bool assinante { get; set; }
        public bool cadastro_completo { get; set; }
        public bool simplificado { get; set; }
        public int clube_id { get; set; }
        public int esquema_id { get; set; }
        public int tipo_adorno { get; set; }
        public int tipo_camisa { get; set; }
        public int tipo_escudo { get; set; }
        public int tipo_estampa_camisa { get; set; }
        public int tipo_estampa_escudo { get; set; }
        public int patrocinador1_id { get; set; }
        public int patrocinador2_id { get; set; }
        public int time_id { get; set; }
        public string cor_borda_escudo { get; set; }
        public string cor_camisa { get; set; }
        public string cor_fundo_escudo { get; set; }
        public string foto_perfil { get; set; }
        public string globo_id { get; set; }
        public string nome { get; set; }
        public string nome_cartola { get; set; }
        public string slug { get; set; }
        public string url_camisa_png { get; set; }
        public string url_camisa_svg { get; set; }
        public string url_escudo_png { get; set; }
        public string url_escudo_svg { get; set; }
        public string facebook_id { get; set; }
        public int rodada_time_id { get; set; }
        public string cor_primaria_estampa_camisa { get; set; }
        public string cor_primaria_estampa_escudo { get; set; }
        public string cor_secundaria_estampa_camisa { get; set; }
        public string cor_secundaria_estampa_escudo { get; set; }
        public object sorteio_pro_num { get; set; }
        public int temporada_inicial { get; set; }
    }

    public class Destaques
    {
        public Lanterninha lanterninha { get; set; }
        public Patrimonio patrimonio { get; set; }
        public Rodada rodada { get; set; }
    }

    public class Liga
    {
        public int liga_id { get; set; }
        public Int64? time_dono_id { get; set; }
        public object clube_id { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public string slug { get; set; }
        public string tipo { get; set; }
        public bool mata_mata { get; set; }
        public bool editorial { get; set; }
        public bool patrocinador { get; set; }
        public string criacao { get; set; }
        public bool sem_capitao { get; set; }
        public string cor_primaria_estampa_flamula { get; set; }
        public string cor_secundaria_estampa_flamula { get; set; }
        public string cor_borda_flamula { get; set; }
        public string cor_fundo_flamula { get; set; }
        public string url_flamula_svg { get; set; }
        public string url_flamula_png { get; set; }
        public int inicio_rodada { get; set; }
        public bool sorteada { get; set; }
        public string imagem { get; set; }
        public int total_times_liga { get; set; }
        public int total_amigos_na_liga { get; set; }
    }

    public class TimeDono
    {
        public bool assinante { get; set; }
        public int time_id { get; set; }
        public string foto_perfil { get; set; }
        public string nome { get; set; }
        public string nome_cartola { get; set; }
        public string slug { get; set; }
        public string url_escudo_png { get; set; }
        public string url_escudo_svg { get; set; }
        public string facebook_id { get; set; }
    }

    public class Ranking
    {
        public int? rodada { get; set; }
        public int? mes { get; set; }
        public int? turno { get; set; }
        public int? campeonato { get; set; }
        public int? patrimonio { get; set; }
        public object capitao { get; set; }
    }

    public class Pontos
    {
        public double? rodada { get; set; }
        public double? mes { get; set; }
        public double? turno { get; set; }
        public double? campeonato { get; set; }
        public string rodada_str { get { return String.Format(@"{0:n}", rodada.ToString() + "pts"); } set { } }
        public string campeonato_str { get { return String.Format(@"{0:n}", campeonato.ToString() + "pts"); } set { } }

        public int capitao { get; set; }
    }

    public class Variacao
    {
        public int? mes { get; set; }
        public int? turno { get; set; }
        public int? campeonato { get; set; }
        public int? patrimonio { get; set; }
        public object capitao { get; set; }
    }

    public class TimeUsuario
    {
        public string url_escudo_png { get; set; }
        public string url_escudo_svg { get; set; }
        public bool assinante { get; set; }
        public int time_id { get; set; }
        public string foto_perfil { get; set; }
        public string nome { get; set; }
        public string nome_cartola { get; set; }
        public string slug { get; set; }
        public string facebook_id { get; set; }
        public double patrimonio { get; set; }
        public Ranking? ranking { get; set; }
        public Pontos pontos { get; set; }
        public Variacao variacao { get; set; }
    }

    public class Ranking2
    {
        public int? rodada { get; set; }
        public int? mes { get; set; }
        public int? turno { get; set; }
        public int? campeonato { get; set; }
        public int? patrimonio { get; set; }
        public object capitao { get; set; }
    }

    public class Pontos2
    {
        public double? rodada { get; set; }
        public double? mes { get; set; }
        public double? turno { get; set; }
        public double? campeonato { get; set; }
        public int capitao { get; set; }
        public string rodada_str { get { return String.Format(@"{0:n}", rodada).Replace(",", ".") + "pts"; } set { } }
        public string campeonato_str { get { return String.Format(@"{0:n}", campeonato).Replace(",", ".") + "pts"; } set { } }
    }

    public class Variacao2
    {
        public int? mes { get; set; }
        public int? turno { get; set; }
        public int? campeonato { get; set; }
        public int? patrimonio { get; set; }
        public object capitao { get; set; }
    }

    public class Time
    {
        public string url_escudo_png { get; set; }
        public string url_escudo_svg { get; set; }
        public bool assinante { get; set; }
        public string assinanteCaminho { get { if (assinante) return "selo_pro.png"; else return ""; } }
        public int time_id { get; set; }
        public string time_dono_color
        {
            get
            {
                if (time_id == Barrel.Current.Get<UserCA>(key: UrlCartola._timeAuth).id)
                {
                    return "#ffcdd2";
                }
                else
                    return "#22323b";
            }
        }
        public string foto_perfil { get; set; }
        public string nome { get; set; }
        public string nome_cartola { get; set; }
        public int esquema_id { get; set; }
        [Ignore]
        public string cartoleiro_esquema { get { return $"{nome_cartola} - {Schemes.GetSchemes(esquema_id)}"; } }
        public string slug { get; set; }
        public string facebook_id { get; set; }
        public double patrimonio { get; set; }
        public String patrimonioString { get { return "C$ " + patrimonio; } }
        public Ranking2? ranking { get; set; }
        public Pontos2 pontos { get; set; }
        public Variacao2 variacao { get; set; }
        public int jogados_cont { get; set; }
        public string jogados { get { return jogados_cont.ToString() + "\n12"; } set { } }
    }


}
