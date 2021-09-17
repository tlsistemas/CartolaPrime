using AutoMapper;
using CartoPrime.Application.Interfaces;
using CartoPrime.Application.ViewModel;
using CartoPrime.Data.CA.Http.Interfaces;
using CartoPrime.Data.CA.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TM.Utils.Events;
using TM.Utils.Http.Response;

namespace CartoPrime.Application.Application
{
    public class BonsDeBicoApplication : IBonsDeBicoApplication
    {
        private readonly ILogger<BonsDeBicoApplication> _logger;
        private readonly ITeamService _teamService;
        private readonly IAthleteService _athleteService;
        private readonly IMarketService _marketService;
        public BonsDeBicoApplication(ILogger<BonsDeBicoApplication> logger,
            ITeamService teamService,
            IAthleteService athleteService,
            IMarketService marketService)
        {
            this._logger = logger;
            this._teamService = teamService;
            this._athleteService = athleteService;
            this._marketService = marketService;
        }

        public async Task<BaseResponse<IEnumerable<MatchBonsDeBico>>> ListMatchs()
        {
            var response = new BasePaginationResponse<IEnumerable<MatchBonsDeBico>>();
            try
            {
                string ladoA = "4953515;747529;3299511;4926245;397575;942719;12255141;16704764;19435336;18234957";
                string ladoB = "12443034;961332;5781967;3348591;7355350;364309;1484400;2774316;13924668;644367";
                string[] vetorA = ladoA.Split(';');
                string[] vetorB = ladoB.Split(';');
                var confrontos = new List<MatchBonsDeBico>();

                var maket = await _marketService.Information();

                if (maket.status_mercado == (int)EMarket.Closed || maket.status_mercado == (int)EMarket.Support)
                    response.Data = await GetMarketCloseAsync(vetorA, vetorB);
                else
                    response.Data = await GetMarketOpenAsync(vetorA, vetorB);


            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddError(ex.Message);
                response.AddError(ex.StackTrace);
                _logger.LogCritical(string.Format(Events.SYSTEM_ERROR_NOT_HANDLED.Message, ""), ex);
                response.Error = true;
                response.SetStatusCode(HttpStatusCode.InternalServerError);
            }

            return response;
        }

        public async Task<BaseResponse<IEnumerable<MatchBonsDeBico>>> ListMatchsB()
        {
            var response = new BasePaginationResponse<IEnumerable<MatchBonsDeBico>>();
            try
            {
                string ladoA = "13924668";
                string ladoB = "13924668";
                string[] vetorA = ladoA.Split(';');
                string[] vetorB = ladoB.Split(';');
                var confrontos = new List<MatchBonsDeBico>();

                var maket = await _marketService.Information();

                if (maket.status_mercado == (int)EMarket.Closed || maket.status_mercado == (int)EMarket.Support)
                    response.Data = await GetMarketCloseAsync(vetorA, vetorB);
                else
                    response.Data = await GetMarketOpenAsync(vetorA, vetorB);


            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddError(ex.Message);
                response.AddError(ex.StackTrace);
                _logger.LogCritical(string.Format(Events.SYSTEM_ERROR_NOT_HANDLED.Message, ""), ex);
                response.Error = true;
                response.SetStatusCode(HttpStatusCode.InternalServerError);
            }

            return response;
        }

        public async Task<BaseResponse<MatchBonsDeBico>> ListMatchs(int idA, int idB)
        {
            var response = new BasePaginationResponse<MatchBonsDeBico>();
            try
            {
                var maket = await _marketService.Information();

                if (maket.status_mercado == (int)EMarket.Closed || maket.status_mercado == (int)EMarket.Support)
                    response.Data = await GetMarketCloseAthletesAsync(idA, idB);
                else
                    response.Data = await GetMarketCloseAthletesAsync(idA, idB);


            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddError(ex.Message);
                response.AddError(ex.StackTrace);
                _logger.LogCritical(string.Format(Events.SYSTEM_ERROR_NOT_HANDLED.Message, ""), ex);
                response.Error = true;
                response.SetStatusCode(HttpStatusCode.InternalServerError);
            }

            return response;
        }

        private async Task<IEnumerable<MatchBonsDeBico>> GetMarketOpenAsync(string[] vetorA, string[] vetorB)
        {
            try
            {
                var confrontos = new List<MatchBonsDeBico>();
                for (int i = 0; i < vetorA.Length; i++)
                {
                    var a = await _teamService.GetTeamsCAIdAsync(int.Parse(vetorA[i]));
                    var b = await _teamService.GetTeamsCAIdAsync(int.Parse(vetorB[i]));

                    var confr = new MatchBonsDeBico();
                    int jogadosA = a.atletas == null ? 0 : a.atletas.FindAll(x => x.pontos_num != 0).Count;
                    var substituicoes = await _teamService.GetSubtituicoesIdAsync(a.time.time_id);
                    if (substituicoes != null && substituicoes.Count > 0)
                    {
                        foreach (var item in substituicoes)
                        {
                            a.atletas.RemoveAll(x => x.atleta_id == item.saiu.atleta_id);
                            a.atletas.Add(a.reservas.Find(x => x.atleta_id == item.entrou.atleta_id));
                            if (a.capitao_id == item.saiu.atleta_id.ToString())
                                a.capitao_id = item.entrou.atleta_id.ToString();
                        }
                    }
                    var timeA = new TeamBonsDeBico
                    {
                        Name = a.time.nome,
                        NameCartoleiro = a.time.nome_cartola,
                        Escudo = a.time.url_escudo_png,
                        Slug = a.time.slug,
                        Parcial = a.pontos,
                        Jogados = jogadosA.ToString(),
                        ParcialDouble = a.pontos == null ? 0 : double.Parse(a.pontos.Replace(".", ","))
                    };

                    int jogadosB = b.atletas == null ? 0 : b.atletas.FindAll(x => x.pontos_num != 0).Count;
                    var substituicoesB = await _teamService.GetSubtituicoesIdAsync(b.time.time_id);
                    if (substituicoesB != null && substituicoesB.Count > 0)
                    {
                        foreach (var item in substituicoesB)
                        {
                            b.atletas.RemoveAll(x => x.atleta_id == item.saiu.atleta_id);
                            b.atletas.Add(b.reservas.Find(x => x.atleta_id == item.entrou.atleta_id));
                            if (b.capitao_id == item.saiu.atleta_id.ToString())
                                b.capitao_id = item.entrou.atleta_id.ToString();
                        }
                    }
                    var timeB = new TeamBonsDeBico
                    {
                        Name = b.time.nome,
                        NameCartoleiro = b.time.nome_cartola,
                        Escudo = b.time.url_escudo_png,
                        Slug = b.time.slug,
                        Parcial = b.pontos,
                        Jogados = jogadosB.ToString(),
                        ParcialDouble = b.pontos == null ? 0 : double.Parse(b.pontos.Replace(".", ","))

                    };

                    if (timeA.ParcialDouble > timeB.ParcialDouble)
                    {
                        timeA.Color = Color.Green.Name;
                        timeB.Color = Color.Red.Name;
                    }
                    else if (timeA.ParcialDouble < timeB.ParcialDouble)
                    {
                        timeA.Color = Color.Red.Name;
                        timeB.Color = Color.Green.Name;
                    }
                    else
                    {
                        timeA.Color = Color.WhiteSmoke.Name;
                        timeB.Color = Color.WhiteSmoke.Name;
                    }

                    confr.TimeA = timeA;
                    confr.TimeB = timeB;
                    confr.Rodada = a.rodada_atual;
                    confrontos.Add(confr);
                }
                return confrontos;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        private async Task<IEnumerable<MatchBonsDeBico>> GetMarketCloseAsync(string[] vetorA, string[] vetorB)
        {
            try
            {
                var pontuados = await _athleteService.GetAthleteScoredAsync();
                var confrontos = new List<MatchBonsDeBico>();
                for (int i = 0; i < vetorA.Length; i++)
                {
                    var a = await _teamService.GetTeamsCAIdAsync(int.Parse(vetorA[i]));
                    var b = await _teamService.GetTeamsCAIdAsync(int.Parse(vetorB[i]));

                    var confr = new MatchBonsDeBico();

                    var timeA = new TeamBonsDeBico();
                    int jogadosA = 0;
                    timeA.Id = a.time.time_id;
                    timeA.Name = a.time.nome;
                    timeA.NameCartoleiro = a.time.nome_cartola;
                    timeA.Escudo = a.time.url_escudo_png;
                    timeA.Slug = a.time.slug;

                    var substituicoes = await _teamService.GetSubtituicoesIdAsync(a.time.time_id);
                    if (substituicoes != null && substituicoes.Count > 0)
                    {
                        foreach (var item in substituicoes)
                        {
                            a.atletas.RemoveAll(x => x.atleta_id == item.saiu.atleta_id);
                            a.atletas.Add(a.reservas.Find(x => x.atleta_id == item.entrou.atleta_id));
                            if (a.capitao_id == item.saiu.atleta_id.ToString())
                                a.capitao_id = item.entrou.atleta_id.ToString();
                        }
                    }

                    if (a.atletas != null)
                    {
                        foreach (var item in a.atletas)
                        {
                            var ponto = pontuados.Find(x => x.id_jogador == item.atleta_id);
                            if (ponto != null)
                            {
                                timeA.ParcialDouble += ponto.pontuacao_dou;
                                if (ponto.pontuacao_dou != 0)
                                {
                                    jogadosA = jogadosA + 1;
                                }
                            }
                        }
                        timeA.Jogados = jogadosA.ToString();
                        var capitao = pontuados.Find(x => x.id_jogador == int.Parse(a.capitao_id));
                        if (capitao != null)
                        {
                            timeA.ParcialDouble += capitao.pontuacao_dou;
                        }
                    }
                    timeA.Parcial = timeA.ParcialDouble.ToString("N2").Replace(",", ".") + "pts";


                    var timeB = new TeamBonsDeBico();
                    int jogadosB = 0;
                    timeB.Id = b.time.time_id;
                    timeB.Name = b.time.nome;
                    timeB.NameCartoleiro = b.time.nome_cartola;
                    timeB.Escudo = b.time.url_escudo_png;
                    timeB.Slug = b.time.slug;

                    var substituicoesB = await _teamService.GetSubtituicoesIdAsync(b.time.time_id);
                    if (substituicoesB != null && substituicoesB.Count > 0)
                    {
                        foreach (var item in substituicoesB)
                        {
                            b.atletas.RemoveAll(x => x.atleta_id == item.saiu.atleta_id);
                            b.atletas.Add(b.reservas.Find(x => x.atleta_id == item.entrou.atleta_id));
                            if (b.capitao_id == item.saiu.atleta_id.ToString())
                                b.capitao_id = item.entrou.atleta_id.ToString();
                        }
                    }

                    if (b.atletas != null)
                    {
                        foreach (var item in b.atletas)
                        {
                            var ponto = pontuados.Find(x => x.id_jogador == item.atleta_id);
                            if (ponto != null)
                            {
                                timeB.ParcialDouble += ponto.pontuacao_dou;
                                if (ponto.pontuacao_dou != 0)
                                {
                                    jogadosB = jogadosB + 1;
                                }
                            }
                        }
                        timeB.Jogados = jogadosB.ToString();
                        var capitao = pontuados.Find(x => x.id_jogador == int.Parse(b.capitao_id));
                        if (capitao != null)
                        {
                            timeB.ParcialDouble += capitao.pontuacao_dou;
                        }
                    }
                    timeB.Parcial = timeB.ParcialDouble.ToString("N2").Replace(",", ".") + "pts";

                    if (timeA.ParcialDouble > timeB.ParcialDouble)
                    {
                        timeA.Color = Color.Green.Name;
                        timeB.Color = Color.Red.Name;
                    }
                    else if (timeA.ParcialDouble < timeB.ParcialDouble)
                    {
                        timeA.Color = Color.Red.Name;
                        timeB.Color = Color.Green.Name;
                    }
                    else
                    {
                        timeA.Color = Color.WhiteSmoke.Name;
                        timeB.Color = Color.WhiteSmoke.Name;
                    }

                    confr.TimeA = timeA;
                    confr.TimeB = timeB;

                    confr.Rodada = a.rodada_atual;
                    confrontos.Add(confr);
                }
                return confrontos;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        private async Task<MatchBonsDeBico> GetMarketCloseAthletesAsync(int idA, int idB)
        {
            try
            {
                var pontuados = await _athleteService.GetAthleteScoredAsync();

                var a = await _teamService.GetTeamsCAIdAsync(idA);
                var b = await _teamService.GetTeamsCAIdAsync(idB);

                var confr = new MatchBonsDeBico();

                var timeA = new TeamBonsDeBico();
                int jogadosA = 0;
                timeA.Id = a.time.time_id;
                timeA.Name = a.time.nome;
                timeA.NameCartoleiro = a.time.nome_cartola;
                timeA.Escudo = a.time.url_escudo_png;
                timeA.Slug = a.time.slug;


                var substituicoes = await _teamService.GetSubtituicoesIdAsync(a.time.time_id);
                if (substituicoes != null && substituicoes.Count > 0)
                {
                    foreach (var item in substituicoes)
                    {
                        a.atletas.RemoveAll(x => x.atleta_id == item.saiu.atleta_id);
                        a.atletas.Add(a.reservas.Find(x => x.atleta_id == item.entrou.atleta_id));
                        if (a.capitao_id == item.saiu.atleta_id.ToString())
                            a.capitao_id = item.entrou.atleta_id.ToString();
                    }
                }

                if (a.atletas != null)
                {
                    for (int i = 0; i < a.atletas.Count; i++)
                    {
                        var ponto = pontuados.Find(x => x.id_jogador == a.atletas[i].atleta_id);
                        if (ponto != null)
                        {
                            a.atletas[i].Pontos = ponto.pontuacao_dou.ToString("N2").Replace(",", ".") + "pts";
                            timeA.ParcialDouble += ponto.pontuacao_dou;
                            if (ponto.pontuacao_dou != 0)
                            {
                                jogadosA = jogadosA + 1;
                            }
                        }
                    }
                    timeA.Jogados = jogadosA.ToString();
                    var capitao = pontuados.Find(x => x.id_jogador == int.Parse(a.capitao_id));
                    if (capitao != null)
                    {
                        timeA.ParcialDouble += capitao.pontuacao_dou;
                    }
                }
                timeA.Parcial = timeA.ParcialDouble.ToString("N2").Replace(",", ".") + "pts";
                timeA.Atletas = a.atletas;
                timeA.Reservas = a.reservas;

                var timeB = new TeamBonsDeBico();
                int jogadosB = 0;
                timeB.Id = b.time.time_id;
                timeB.Name = b.time.nome;
                timeB.NameCartoleiro = b.time.nome_cartola;
                timeB.Escudo = b.time.url_escudo_png;
                timeB.Slug = b.time.slug;


                var substituicoesB = await _teamService.GetSubtituicoesIdAsync(b.time.time_id);
                if (substituicoesB != null && substituicoesB.Count > 0)
                {
                    foreach (var item in substituicoesB)
                    {
                        b.atletas.RemoveAll(x => x.atleta_id == item.saiu.atleta_id);
                        b.atletas.Add(b.reservas.Find(x => x.atleta_id == item.entrou.atleta_id));
                        if (b.capitao_id == item.saiu.atleta_id.ToString())
                            b.capitao_id = item.entrou.atleta_id.ToString();
                    }
                }

                if (b.atletas != null)
                {
                    for (int i = 0; i < b.atletas.Count; i++)
                    {
                        var ponto = pontuados.Find(x => x.id_jogador == b.atletas[i].atleta_id);
                        if (ponto != null)
                        {
                            b.atletas[i].Pontos = ponto.pontuacao_dou.ToString("N2").Replace(",", ".") + "pts";
                            timeB.ParcialDouble += ponto.pontuacao_dou;
                            if (ponto.pontuacao_dou != 0)
                            {
                                jogadosB = jogadosB + 1;
                            }
                        }
                    }
                    timeB.Jogados = jogadosB.ToString();
                    var capitao = pontuados.Find(x => x.id_jogador == int.Parse(b.capitao_id));
                    if (capitao != null)
                    {
                        timeB.ParcialDouble += capitao.pontuacao_dou;
                    }
                }
                timeB.Parcial = timeB.ParcialDouble.ToString("N2").Replace(",", ".") + "pts";
                timeB.Atletas = b.atletas;
                timeB.Reservas = b.reservas;

                if (timeA.ParcialDouble > timeB.ParcialDouble)
                {
                    timeA.Color = Color.Green.Name;
                    timeB.Color = Color.Red.Name;
                }
                else if (timeA.ParcialDouble < timeB.ParcialDouble)
                {
                    timeA.Color = Color.Red.Name;
                    timeB.Color = Color.Green.Name;
                }
                else
                {
                    timeA.Color = Color.WhiteSmoke.Name;
                    timeB.Color = Color.WhiteSmoke.Name;
                }

                confr.TimeA = timeA;
                confr.TimeB = timeB;

                confr.Rodada = a.rodada_atual;

                return confr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}
