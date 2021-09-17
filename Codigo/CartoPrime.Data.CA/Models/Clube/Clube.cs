using System;
using System.Collections.Generic;
using System.Text;

namespace CartoPrime.Data.CA.Models
{
    public class Clube
    {
        public string _30x30
        {
            get;
            set;
        }

        public string _45x45
        {
            get;
            set;
        }

        public string _60x60
        {
            get;
            set;
        }

        public string abreviacao
        {
            get;
            set;
        }

        public int id
        {
            get;
            set;
        }

        public byte[] imageClube
        {
            get;
            set;
        }

        public string nome
        {
            get;
            set;
        }

        public int posicao { get; set; }

        public object escudos { get; set; }

        public Clube()
        {
        }
    }
}
