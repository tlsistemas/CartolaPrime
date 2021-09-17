using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CartoPrime.Models
{
    [Table("mercado")]
    public class Market
    {
        [PrimaryKey]
        public int rodada_atual { get; set; }
        public int status_mercado { get; set; }
        public int esquema_default_id { get; set; }
        public int cartoleta_inicial { get; set; }
        public int max_ligas_free { get; set; }
        public int max_ligas_pro { get; set; }
        public int max_ligas_matamata_free { get; set; }
        public int max_ligas_matamata_pro { get; set; }
        public int max_ligas_patrocinadas_free { get; set; }
        public int max_ligas_patrocinadas_pro_num { get; set; }
        public bool game_over { get; set; }
        public int temporada { get; set; }
        public bool reativar { get; set; }
        public int times_escalados { get; set; }
        [Ignore]
        public Closure fechamento { get; set; }
        public int timestamp_fecha { get { return fechamento.timestamp; } }
        public bool mercado_pos_rodada { get; set; }
        public string aviso { get; set; }
    }

    public class Closure
    {
        public int dia { get; set; }
        public int mes { get; set; }
        public int ano { get; set; }
        public int hora { get; set; }
        public int minuto { get; set; }
        public int timestamp { get; set; }
    }
}
