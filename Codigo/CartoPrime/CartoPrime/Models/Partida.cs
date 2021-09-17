using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CartoPrime.Models
{
    public class Partida
    {
        public List<string> aproveitamento_mandante
        {
            get;
            set;
        }

        public List<string> aproveitamento_visitante
        {
            get;
            set;
        }

        public string clube_casa_nome { get; set; }
        public string clube_casa_escudo { get; set; }
        public int clube_casa_id
        {
            get;
            set;
        }

        public int clube_casa_posicao
        {
            get;
            set;
        }

        public string clube_visitante_nome { get; set; }
        public string clube_visitante_escudo { get; set; }
        public int clube_visitante_id
        {
            get;
            set;
        }

        public int clube_visitante_posicao
        {
            get;
            set;
        }

        public string local
        {
            get;
            set;
        }

        public DateTime partida_data
        {
            get;
            set;
        }

        public object placar_oficial_mandante
        {
            get;
            set;
        }

        public object placar_oficial_visitante
        {
            get;
            set;
        }

        public string url_confronto
        {
            get;
            set;
        }

        public string url_transmissao
        {
            get;
            set;
        }

        public bool valida
        {
            get;
            set;
        }

        public string valida_string
        {
            get
            {
                if (!valida)
                    return "ESTA PARTIDA NÃO É VALIDA PARA A RODADA";
                else return "";
            }
        }

        [JsonIgnore]
        public string valida_color
        {
            get
            {
                if (!valida)
                    return "#b55b49";
                else return "#7eaf7e";
            }
        }

        public DateTime dt_partida { get; set; }
        public string local_confronto { get; set; }

    }
}
