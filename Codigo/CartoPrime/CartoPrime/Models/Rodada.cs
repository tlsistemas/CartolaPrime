using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CartoPrime.Models
{
    public class Rodada
    {
        public ObservableCollection<Partida> Partidas
        {
            get;
            set;
        }

        [JsonProperty("rodada")]
        public int RodadaAtual
        {
            get;
            set;
        }

        public string rodada_string { get; set; }
    }
}
