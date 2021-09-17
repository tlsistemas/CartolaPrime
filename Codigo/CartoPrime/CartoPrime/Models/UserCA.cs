using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CartoPrime.Models
{
    [Table("user_ca")]
    public class UserCA
    {
        [PrimaryKey, Column("id")]
        public int id { get; set; }
        //public string user { get; set; }
        //public string senha { get; set; }
        [Column("glbstring")]
        public string glbstring { get; set; }
        [Column("urlescudo")]
        public string url_escudo { get; set; }
        [Column("slug")]
        public string slug { get; set; }
        [Column("nometime")]
        public string NomeTime { get; set; }
        [Column("nomecartoleiro")]
        public string NomeCartoleiro { get; set; }
    }
}
