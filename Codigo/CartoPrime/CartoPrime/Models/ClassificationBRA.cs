using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CartoPrime.Models
{
    public class ClassificationBRA
    {
        public string url { get; set; }
        public string nome_fase { get; set; }
        public ClassNames class_names { get; set; }
        public Edicao edicao { get; set; }
        public List<Classificacao> classificacao { get; set; }
        public Campeonato campeonato { get; set; }
        public string img_url { get; set; }
        public string css { get; set; }
    }

    public class ClassNames
    {
        public string santos { get; set; }
        public string coritiba { get; set; }
        public string atletico_pr { get; set; }
        public string gremio { get; set; }
        public string palmeiras { get; set; }
        public string vitoria { get; set; }
        public string vasco { get; set; }
        public string chapecoense { get; set; }
        public string ponte_preta { get; set; }
        public string botafogo { get; set; }
        public string sao_paulo { get; set; }
        public string cruzeiro { get; set; }
        public string fluminense { get; set; }
        public string avai { get; set; }
        public string atletico_go { get; set; }
        public string corinthians { get; set; }
        public string sport { get; set; }
        public string flamengo { get; set; }
        public string atletico_mg { get; set; }
        public string bahia { get; set; }
    }

    public class Edicao
    {
        public string slug { get; set; }
        public string nome { get; set; }
    }

    public class Escudos
    {
        [JsonProperty("60x60")]
        public string _60x60 { get; set; }
        [JsonProperty("30x30")]
        public string _30x30 { get; set; }
        [JsonProperty("45x45")]
        public string _45x45 { get; set; }
    }

    public class Cores
    {
        public string secundaria { get; set; }
        public string primaria { get; set; }
        public string terciaria { get; set; }
    }

    public class Equipe
    {
        public string genero { get; set; }
        public string nome { get; set; }
        public int equipe_id { get; set; }
        public Escudos escudos { get; set; }
        public string slug { get; set; }
        public Cores cores { get; set; }
        public string nome_popular { get; set; }
        public string sigla { get; set; }
        public string scudo_url { get; set; }
    }

    public class VencedorJogo
    {
        public int? equipe_id { get; set; }
        public string label { get; set; }
    }

    public class Tipo
    {
        public string tipo_id { get; set; }
        public string descricao { get; set; }
    }

    public class Formato
    {
        public int quantidade_de_jogos { get; set; }
        public int formato_id { get; set; }
        public string descricao { get; set; }
        public string label { get; set; }
    }

    public class Campeonato
    {
        public int campeonato_id { get; set; }
        public string genero { get; set; }
        public string slug { get; set; }
        public string nome { get; set; }
    }

    public class Edicao2
    {
        public int edicao_id { get; set; }
        public string nome { get; set; }
        public string slug { get; set; }
        public int campeonato_id { get; set; }
        public Campeonato campeonato { get; set; }
        public string slug_editorial { get; set; }
    }

    public class Fase
    {
        public int edicao_id { get; set; }
        public int ordem { get; set; }
        public string data_fim { get; set; }
        public Tipo tipo { get; set; }
        public string nome { get; set; }
        public int fase_id { get; set; }
        public Formato formato { get; set; }
        public string data_inicio { get; set; }
        public Edicao2 edicao { get; set; }
        public bool atual { get; set; }
        public string slug { get; set; }
        public string disclaimer { get; set; }
    }

    public class Escudos2
    {
        public string _60x60 { get; set; }
        public string _30x30 { get; set; }
        public string _45x45 { get; set; }
    }

    public class Cores2
    {
        public string secundaria { get; set; }
        public string primaria { get; set; }
        public string terciaria { get; set; }
    }

    public class EquipeVisitante
    {
        public string genero { get; set; }
        public string nome { get; set; }
        public int equipe_id { get; set; }
        public Escudos2 escudos { get; set; }
        public string slug { get; set; }
        public Cores2 cores { get; set; }
        public string nome_popular { get; set; }
        public string sigla { get; set; }
    }

    public class Tipo2
    {
        public string tipo_id { get; set; }
        public string descricao { get; set; }
    }

    public class Sede
    {
        public int sede_id { get; set; }
        public string nome_popular { get; set; }
        public Tipo2 tipo { get; set; }
        public string nome { get; set; }
    }

    public class Escudos3
    {
        public string __invalid_name__60x60 { get; set; }
        public string __invalid_name__30x30 { get; set; }
        public string __invalid_name__45x45 { get; set; }
    }

    public class Cores3
    {
        public string secundaria { get; set; }
        public string primaria { get; set; }
        public string terciaria { get; set; }
    }

    public class EquipeMandante
    {
        public string genero { get; set; }
        public string nome { get; set; }
        public int equipe_id { get; set; }
        public Escudos3 escudos { get; set; }
        public string slug { get; set; }
        public Cores3 cores { get; set; }
        public string nome_popular { get; set; }
        public string sigla { get; set; }
    }

    public class Jogo
    {
        public int escalacao_mandante_id { get; set; }
        public VencedorJogo vencedor_jogo { get; set; }
        public int escalacao_visitante_id { get; set; }
        public bool cancelado { get; set; }
        public bool suspenso { get; set; }
        public int equipe_mandante_id { get; set; }
        public int rodada { get; set; }
        public bool decisivo { get; set; }
        public object placar_penaltis_mandante { get; set; }
        public int fase_id { get; set; }
        public Fase fase { get; set; }
        public EquipeVisitante equipe_visitante { get; set; }
        public bool wo { get; set; }
        public string hora_realizacao { get; set; }
        public object placar_penaltis_visitante { get; set; }
        public Sede sede { get; set; }
        public EquipeMandante equipe_mandante { get; set; }
        public int placar_oficial_visitante { get; set; }
        public int equipe_visitante_id { get; set; }
        public int jogo_id { get; set; }
        public int sede_id { get; set; }
        public int placar_oficial_mandante { get; set; }
        public string data_realizacao { get; set; }
    }

    public class AproveitamentoUltimosJogo
    {
        public string aproveitamento { get; set; }
        public Jogo jogo { get; set; }
    }

    public class Classificacao
    {
        public int variacao { get; set; }
        public string variacao_color { get; set; }
        public string variacao_string { get; set; }
        public int ordem { get; set; }
        public string equipe_url { get; set; }
        public int saldo_gols { get; set; }
        public int empates { get; set; }
        public int pontos { get; set; }
        public int derrotas { get; set; }
        public string variacao_label { get; set; }
        public Equipe equipe { get; set; }
        public List<AproveitamentoUltimosJogo> aproveitamento_ultimos_jogos { get; set; }
        public int vitorias { get; set; }
        public int jogos { get; set; }
    }

    public class Campeonato2
    {
        public string slug { get; set; }
        public string nome { get; set; }
    }
}
