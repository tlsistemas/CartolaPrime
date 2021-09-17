using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace CartoPrime.Models
{
    public class Athlete
    {
        public int atleta_id { get; set; }
        public string nome { get; set; }
        public string apelido { get; set; }
        public string foto { get; set; }
        public string foto140 { get { return foto.Replace("FORMATO", "140x140"); } }
        public string preco_editorial { get; set; }
        public double? variacao_num { get; set; }
        public Scout scout { get; set; }
        public double pontos_num { get; set; }
        [Ignore]
        public string Pontos { get { return pontos_num.ToString("F2").Replace(",", ".") + "pts"; } set { } }
        public string preco_num { get; set; }
        public double preco_double { get; set; }
        public double? media_num { get; set; }
        public int jogos_num { get; set; }
        public int posicao_id { get; set; }
        public string posicao { get { return Positions.GetPosition(posicao_id); } }
        public int status_id { get; set; }
        public string status { get; set; }
        public int clube_id { get; set; }
        public bool capita { get; set; }
        public double pontos_capita { get; set; }
        public String pontos_capita_string { get; set; }

        public string min_valorizacao { get; set; }
        public Partida Partida { get; set; }

    }

    [Table("atletas_ca")]
    public class AthleteCA
    {
        [PrimaryKey]
        public int IdAleta { get; set; }
        public int IdTime { get; set; }
        public string NomeTime { get; set; }
        public string CartoleiroEsquema { get; set; }
        public string min_valorizacao { get; set; }
        public double ParcialTimeCount { get; set; }
        public string FotoAtleta { get; set; }
        [Ignore]
        public string ParcialTime { get { return ParcialTimeCount.ToString("F2").Replace(",", ".") + "pts"; } set { } }
        [Ignore]
        public string ParcialTimeColor
        {
            get
            {
                if (ParcialTimeCount < 0)
                {
                    return "#b55b49";
                }
                else if (ParcialTimeCount > 0)
                {
                    return "#7eaf7e";
                }
                else
                {
                    return "Gray";
                }
            }

        }
        public string Patrimonio { get; set; }
        public string NomeAtleta { get; set; }
        public int Posicao { get; set; }
        [Ignore]
        public string PosicaoAtleta { get { return Positions.GetPosition(Posicao); } }
        public string posicao_min { get { return Positions.GetPositionMin(Posicao); } }
        public string Scouts { get; set; }
        public string ScoutsDef { get; set; }
        public double PontosCout { get; set; }
        [Ignore]
        public string Pontos { get { return PontosCout.ToString("F2").Replace(",", ".") + "pts"; } set { } }
        public string escudo_clube { get; set; }
        public string nome_clube { get; set; }
        public string escudo_time { get; set; }
        public string pont_color { get; set; }
        public bool capita { get; set; }
        public double pontos_capita { get; set; }
        public String pontos_capita_string { get; set; }
        public string foto_timeCA { get; set; }
        public int GCount { get; set; }
        public int ACount { get; set; }
        public int DSCount { get; set; }
        public int DECount { get; set; }
        public int SGCount { get; set; }


        public string variacao_num { get; set; }
        public string media_num { get; set; }
        public double preco_double { get; set; }
        [Ignore]
        public Partida Partida { get; set; }
        
        public string ValorizacaoAtleta { get; set; }
    }

    public class BaseAthleteCA
    {
        public List<AthleteCA> Athletes { get; set; }
        public List<AthleteCA> Reservas { get; set; }
        public TeamCA Time { get; set; }
        public int GCount { get; set; }
        public int ACount { get; set; }
        public int DSCount { get; set; }
        public int DECount { get; set; }
        public int SGCount { get; set; }
        public string ScoutString { get { return $"DS {DSCount} | DE {DECount} | SG {SGCount}"; } }
    }
}
