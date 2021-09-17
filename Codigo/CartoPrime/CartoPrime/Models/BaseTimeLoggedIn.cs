using System;
using System.Collections.Generic;
using System.Text;

namespace CartoPrime.Models
{
    public class BaseTimeLoggedIn
    {
        //public AtletasTimeLogado[] atletas { get; set; }
        //public List<Clube> clubes { get; set; }
        public TimeLoggedIn time { get; set; }
        public double patrimonio { get; set; }
        public int esquema_id { get; set; }
        public string pontos { get; set; }
        //public double valor_time { get; set; }
        public int rodada_atual { get; set; }
        //public double variacao_patrimonio { get; set; }
        //public double variacao_pontos { get; set; }
    }

    public class TimeLoggedIn
    {
        public int time_id { get; set; }
        public int clube_id { get; set; }
        public int esquema_id { get; set; }
        public int cadun_id { get; set; }
        public object facebook_id { get; set; }
        public string foto_perfil { get; set; }
        public string nome { get; set; }
        public string nome_cartola { get; set; }
        public string slug { get; set; }
        public int tipo_escudo { get; set; }
        public string cor_fundo_escudo { get; set; }
        public string cor_borda_escudo { get; set; }
        public string cor_primaria_estampa_escudo { get; set; }
        public string cor_secundaria_estampa_escudo { get; set; }
        public string url_escudo_svg { get; set; }
        public string url_escudo_png { get; set; }
        public string url_camisa_svg { get; set; }
        public string url_camisa_png { get; set; }
        public string url_escudo_placeholder_png { get; set; }
        public string url_camisa_placeholder_png { get; set; }
        public int tipo_estampa_escudo { get; set; }
        public int tipo_adorno { get; set; }
        public int tipo_camisa { get; set; }
        public int tipo_estampa_camisa { get; set; }
        public string cor_camisa { get; set; }
        public string cor_primaria_estampa_camisa { get; set; }
        public string cor_secundaria_estampa_camisa { get; set; }
        public string rodada_time_id { get; set; }
        public bool assinante { get; set; }
        public bool cadastro_completo { get; set; }
        public int patrocinador1_id { get; set; }
        public int patrocinador2_id { get; set; }
        public int temporada_inicial { get; set; }
        public bool simplificado { get; set; }
    }
}
