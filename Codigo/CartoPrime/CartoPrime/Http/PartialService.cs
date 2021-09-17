using CartoPrime.Helpers;
using CartoPrime.Http.Interfaces;
using CartoPrime.Models;
using MonkeyCache.FileStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CartoPrime.Http
{
    public class PartialService : IPartialService
    {
        IApiService api;
        private ITeamService _teamService;
        private IAthleteService _athleteService;
        private IClubService _clubService;

        public PartialService()
        {
            api = DependencyService.Get<IApiService>();
            _teamService = DependencyService.Get<ITeamService>();
            _athleteService = DependencyService.Get<IAthleteService>();
            _clubService = DependencyService.Get<IClubService>();
        }

        public async Task<List<TeamCA>> GetMarketClose()
        {
            try
            {
                var TeamsUp = await App.Database.GetTeamCAsAsync();
                var scores = await _athleteService.GetAthleteAsync();
                for (int i = 0; i < TeamsUp.Count; i++)
                {
                    if (scores != null)
                    {
                        //var cap = await _teamService.GetTeamsCAIdAsync(TeamsUp[i].time_id);
                        TeamsUp[i].pontos_count = 0;
                        TeamsUp[i].jogados_cont = 0;
                        TeamsUp[i].GCount = 0;
                        TeamsUp[i].ACount = 0;
                        TeamsUp[i].DSCount = 0;
                        TeamsUp[i].SGCount = 0;
                        TeamsUp[i].DECount = 0;
                        TeamsUp[i].variacao_patrimonio_count = 0;
                        //TeamsUp[i].patrimonio = athlets.patrimonio;
                        var athlets = await _teamService.GetTeamsCAIdAsync(TeamsUp[i].time_id);
                        var substituicoes = await _teamService.GetSubtituicoesIdAsync(TeamsUp[i].time_id);

                        if (substituicoes != null && substituicoes.Count > 0)
                        {
                            foreach (var item in substituicoes)
                            {
                                athlets.atletas.RemoveAll(x => x.atleta_id == item.saiu.atleta_id);
                                athlets.atletas.Add(athlets.reservas.Find(x => x.atleta_id == item.entrou.atleta_id));
                                if (athlets.capitao_id == item.saiu.atleta_id.ToString())
                                    athlets.capitao_id = item.entrou.atleta_id.ToString();
                            }
                        }

                        for (int a = 0; a < athlets.atletas.Count; a++)
                        {
                            var jog = scores.Find(x => x.id_jogador == athlets.atletas[a].atleta_id);
                            if (jog != null)
                            {
                                if (jog.pontuacao_dou != 0)
                                    TeamsUp[i].jogados_cont++;
                                TeamsUp[i].pontos_count += jog.pontuacao_dou;
                                TeamsUp[i].variacao_patrimonio_count += athlets.atletas[a].variacao_num;
                                if (jog.scout != null)
                                {
                                    TeamsUp[i].GCount += jog.scout.G;
                                    TeamsUp[i].ACount += jog.scout.A;
                                    TeamsUp[i].DSCount += jog.scout.DS;
                                    TeamsUp[i].SGCount += jog.scout.SG;
                                    TeamsUp[i].DECount += jog.scout.DD;
                                }
                                if (jog.id_jogador.ToString().Equals(athlets.capitao_id))
                                    TeamsUp[i].pontos_count += jog.pontuacao_dou;
                            }
                        }
                    }
                    await App.Database.SaveTeamCAAsync(TeamsUp[i]);
                }
                TeamsUp = TeamsUp.OrderByDescending(x => x.pontos_count).ToList();
                return TeamsUp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Time>> GetLeagueMarketClose(List<Time> TeamsUp)
        {
            try
            {
                var scores = await _athleteService.GetAthleteAsync().ConfigureAwait(true);
                for (int i = 0; i < TeamsUp.Count; i++)
                {
                    var cap = await _teamService.GetTeamsCAIdAsync(TeamsUp[i].time_id).ConfigureAwait(true);
                    TeamsUp[i].pontos.rodada = 0;
                    TeamsUp[i].jogados_cont = 0;
                    var athlets = await _teamService.GetTeamsCAIdAsync(TeamsUp[i].time_id).ConfigureAwait(true);

                    if (athlets.atletas != null)
                    {
                        var substituicoes = await _teamService.GetSubtituicoesIdAsync(athlets.time.time_id).ConfigureAwait(true);

                        if (substituicoes != null && substituicoes.Count > 0)
                        {
                            foreach (var item in substituicoes)
                            {
                                athlets.atletas.RemoveAll(x => x.atleta_id == item.saiu.atleta_id);
                                athlets.atletas.Add(athlets.reservas.Find(x => x.atleta_id == item.entrou.atleta_id));
                                if (athlets.capitao_id == item.saiu.atleta_id.ToString())
                                    athlets.capitao_id = item.entrou.atleta_id.ToString();
                            }
                        }


                        for (int a = 0; a < athlets.atletas.Count; a++)
                        {
                            var jog = scores.Find(x => x.id_jogador == athlets.atletas[a].atleta_id);
                            if (jog != null)
                            {
                                if (jog.pontuacao_dou != 0)
                                    TeamsUp[i].jogados_cont++;
                                TeamsUp[i].pontos.rodada += jog.pontuacao_dou;

                                if (jog.id_jogador.ToString() == cap.capitao_id)
                                {
                                    TeamsUp[i].pontos.rodada += jog.pontuacao_dou;

                                }
                            }
                        }

                    }

                }

                TeamsUp = TeamsUp.OrderByDescending(x => x.pontos.rodada).ToList();

                return TeamsUp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<TeamCA>> GetMarketOpen(int round)
        {
            try
            {
                var TeamsUp = await App.Database.GetTeamCAsAsync();
                for (int i = 0; i < TeamsUp.Count; i++)
                {
                    var athlets = await _teamService.GetTeamsCAIdAsync(TeamsUp[i].time_id, round);
                    TeamsUp[i].pontos_count = 0;
                    TeamsUp[i].jogados_cont = 0;
                    TeamsUp[i].GCount = 0;
                    TeamsUp[i].ACount = 0;
                    TeamsUp[i].DSCount = 0;
                    TeamsUp[i].SGCount = 0;
                    TeamsUp[i].DECount = 0;
                    TeamsUp[i].variacao_patrimonio_count = 0;
                    TeamsUp[i].patrimonio = athlets.patrimonio;

                    var substituicoes = await _teamService.GetSubtituicoesIdAsync(TeamsUp[i].time_id);

                    if (substituicoes != null && substituicoes.Count > 0)
                    {
                        foreach (var item in substituicoes)
                        {
                            athlets.atletas.RemoveAll(x => x.atleta_id == item.saiu.atleta_id);
                            athlets.atletas.Add(athlets.reservas.Find(x => x.atleta_id == item.entrou.atleta_id));
                            if (athlets.capitao_id == item.saiu.atleta_id.ToString())
                                athlets.capitao_id = item.entrou.atleta_id.ToString();
                        }
                    }

                    for (int a = 0; a < athlets.atletas.Count; a++)
                    {
                        TeamsUp[i].jogados_cont++;
                        TeamsUp[i].pontos_count += athlets.atletas[a].pontos_num;
                        TeamsUp[i].variacao_patrimonio_count += athlets.atletas[a].variacao_num;
                        if (athlets.atletas[a].scout != null)
                        {
                            TeamsUp[i].GCount += athlets.atletas[a].scout.G;
                            TeamsUp[i].ACount += athlets.atletas[a].scout.A;
                            TeamsUp[i].DSCount += athlets.atletas[a].scout.DS;
                            TeamsUp[i].SGCount += athlets.atletas[a].scout.SG;
                            TeamsUp[i].DECount += athlets.atletas[a].scout.DD;
                        }
                        if (athlets.atletas[a].atleta_id.ToString().Equals(TeamsUp[i].capitao_id))
                            TeamsUp[i].pontos_count += athlets.atletas[a].pontos_num;

                    }

                    await App.Database.SaveTeamCAAsync(TeamsUp[i]);

                }
                TeamsUp = TeamsUp.OrderByDescending(x => x.pontos_count).ToList();
                for (int i = 0; i < TeamsUp.Count; i++)
                {
                    TeamsUp[i].posicao = (i + 1).ToString();
                }

                return TeamsUp;
            }
            catch (Exception ex)
            {
                return await App.Database.GetTeamCAsAsync();
            }

        }

        public async Task<BaseAthleteCA> GetAthleteMarketOpen(int timeId, int round)
        {
            try
            {
                var market = Barrel.Current.Get<Market>(key: UrlCartola._mercadoStatus);

                var partidas = await _clubService.GetConfrontos(market.rodada_atual.ToString()).ConfigureAwait(true);
                var lstResult = new List<AthleteCA>();
                var lstResultReservas = new List<AthleteCA>();
                var clubes = await _clubService.ListClubs();
                var athlets = await _teamService.GetTeamsCAIdAsync(timeId, round); ;
                int GCount = 0;
                int ACount = 0;
                int DSCount = 0;
                int SGCount = 0;
                int DECount = 0;

                var substituicoes = await _teamService.GetSubtituicoesIdAsync(athlets.time.time_id);

                if (substituicoes != null && substituicoes.Count > 0)
                {
                    foreach (var item in substituicoes)
                    {
                        athlets.atletas.RemoveAll(x => x.atleta_id == item.saiu.atleta_id);
                        athlets.atletas.Add(athlets.reservas.Find(x => x.atleta_id == item.entrou.atleta_id));
                        if (athlets.capitao_id == item.saiu.atleta_id.ToString())
                            athlets.capitao_id = item.entrou.atleta_id.ToString();
                    }
                }

                for (int i = 0; i < athlets.atletas.Count; i++)
                {
                    var _atletaTela = new AthleteCA();
                    var scouts = Scout.OrganizarScouts(athlets.atletas[i].scout);
                    var scoutsDef = Scout.OrganizarScoutsDef(athlets.atletas[i].scout);


                    _atletaTela.FotoAtleta = athlets.atletas[i].foto140;
                    _atletaTela.Posicao = athlets.atletas[i].posicao_id;
                    try
                    {
                        var ponto = athlets.atletas[i].pontos_num.ToString();
                        _atletaTela.PontosCout = double.Parse(ponto);
                    }
                    catch (Exception)
                    {
                        _atletaTela.PontosCout = 0;
                    }
                    if (athlets.atletas[i].scout != null)
                    {
                        GCount += athlets.atletas[i].scout.G;
                        ACount += athlets.atletas[i].scout.A;
                        DSCount += athlets.atletas[i].scout.DS;
                        SGCount += athlets.atletas[i].scout.SG;
                        DECount += athlets.atletas[i].scout.DE;
                    }
                    _atletaTela.NomeAtleta = athlets.atletas[i].apelido;
                    _atletaTela.Scouts = scouts;
                    _atletaTela.ScoutsDef = scoutsDef;
                    var clube = clubes.Find(x => x.id == athlets.atletas[i].clube_id);
                    _atletaTela.escudo_clube = clube._30x30;
                    _atletaTela.nome_clube = clube.abreviacao;
                    _atletaTela.capita = false;


                    _atletaTela.NomeTime = athlets.time.nome;
                    _atletaTela.CartoleiroEsquema = athlets.time.nome_cartola + " - " + Schemes.GetSchemes(athlets.time.esquema_id);
                    _atletaTela.escudo_time = athlets.time.url_escudo_png;
                    _atletaTela.IdTime = athlets.time.time_id;
                    if (_atletaTela.PontosCout < 0)
                    {
                        _atletaTela.pont_color = "#b55b49";
                    }
                    else if (_atletaTela.PontosCout > 0)
                    {
                        _atletaTela.pont_color = "#7eaf7e";
                    }
                    else
                    {
                        _atletaTela.pont_color = "Gray";
                    }

                    if (athlets.atletas[i].atleta_id == int.Parse(athlets.capitao_id))
                    {
                        _atletaTela.capita = true;
                        _atletaTela.pontos_capita += _atletaTela.PontosCout;
                        _atletaTela.PontosCout += _atletaTela.pontos_capita;
                        _atletaTela.pontos_capita_string = "2x " + _atletaTela.pontos_capita.ToString("F2").Replace(",", ".");
                    }

                    _atletaTela.IdAleta = athlets.atletas[i].atleta_id;
                    _atletaTela.IdTime = athlets.time.time_id;
                    _atletaTela.foto_timeCA = athlets.time.url_escudo_png;
                    _atletaTela.NomeTime = athlets.time.nome;
                    _atletaTela.CartoleiroEsquema = athlets.time.nome_cartola + " - " + Schemes.GetSchemes(athlets.esquema_id);

                    _atletaTela.variacao_num = athlets.atletas[i].variacao_num == null ? "0.00"
                        : athlets.atletas[i].variacao_num.ToString().Replace(",", ".");
                    _atletaTela.media_num = athlets.atletas[i].media_num == null ? "0.00" : athlets.atletas[i].media_num.ToString().Replace(",", ".");
                    _atletaTela.preco_double = athlets.atletas[i].preco_double;//== null ? "0.00" : double.Parse(athle.preco_double);
                    _atletaTela.min_valorizacao = await _athleteService.ScoreToValue(athlets.atletas[i].preco_double);
                    _atletaTela.Partida = partidas.Find(x => x.clube_casa_id == athlets.atletas[i].clube_id
                                                                  || x.clube_visitante_id == athlets.atletas[i].clube_id);


                    try
                    {
                        var pontTem = athlets.pontos.Replace(".", ",");
                        _atletaTela.ParcialTimeCount = double.Parse(pontTem);
                    }
                    catch (Exception)
                    {
                        _atletaTela.ParcialTimeCount = 0;
                    }



                    lstResult.Add(_atletaTela);
                }

                for (int i = 0; i < athlets.reservas.Count; i++)
                {
                    var _atletaTela = new AthleteCA();
                    var scouts = Scout.OrganizarScouts(athlets.reservas[i].scout);
                    var scoutsDef = Scout.OrganizarScoutsDef(athlets.reservas[i].scout);


                    _atletaTela.FotoAtleta = athlets.reservas[i].foto140;
                    _atletaTela.Posicao = athlets.reservas[i].posicao_id;
                    try
                    {
                        var ponto = athlets.reservas[i].pontos_num.ToString();
                        _atletaTela.PontosCout = double.Parse(ponto);
                    }
                    catch (Exception)
                    {
                        _atletaTela.PontosCout = 0;
                    }
                    if (athlets.reservas[i].scout != null)
                    {
                        GCount += athlets.reservas[i].scout.G;
                        ACount += athlets.reservas[i].scout.A;
                        DSCount += athlets.reservas[i].scout.DS;
                        SGCount += athlets.reservas[i].scout.SG;
                        DECount += athlets.reservas[i].scout.DE;
                    }
                    _atletaTela.NomeAtleta = athlets.reservas[i].apelido;
                    _atletaTela.Scouts = scouts;
                    _atletaTela.ScoutsDef = scoutsDef;
                    var clube = clubes.Find(x => x.id == athlets.reservas[i].clube_id);
                    _atletaTela.escudo_clube = clube._30x30;
                    _atletaTela.nome_clube = clube.abreviacao;
                    if (_atletaTela.PontosCout < 0)
                    {
                        _atletaTela.pont_color = "#b55b49";
                    }
                    else if (_atletaTela.PontosCout > 0)
                    {
                        _atletaTela.pont_color = "#7eaf7e";
                    }
                    else
                    {
                        _atletaTela.pont_color = "Gray";
                    }

                    _atletaTela.variacao_num = athlets.reservas[i].variacao_num == null ? "0.00"
                        : athlets.reservas[i].variacao_num.ToString().Replace(",", ".");
                    _atletaTela.media_num = athlets.reservas[i].media_num == null ? "0.00" : athlets.reservas[i].media_num.ToString().Replace(",", ".");
                    _atletaTela.preco_double = athlets.reservas[i].preco_double;//== null ? "0.00" : double.Parse(athle.preco_double);
                    _atletaTela.min_valorizacao = await _athleteService.ScoreToValue(athlets.reservas[i].preco_double);
                    _atletaTela.Partida = partidas.Find(x => x.clube_casa_id == athlets.reservas[i].clube_id
                                                                  || x.clube_visitante_id == athlets.reservas[i].clube_id);


                    try
                    {
                        var pontTem = athlets.pontos.Replace(".", ",");
                        _atletaTela.ParcialTimeCount = double.Parse(pontTem);
                    }
                    catch (Exception)
                    {
                        _atletaTela.ParcialTimeCount = 0;
                    }



                    lstResultReservas.Add(_atletaTela);
                }

                var resutl = new BaseAthleteCA();
                resutl.Athletes = lstResult.OrderBy(x => x.Posicao).ToList();
                resutl.Reservas = lstResultReservas.OrderBy(x => x.Posicao).ToList();
                resutl.GCount = GCount;
                resutl.ACount = ACount;
                resutl.DSCount = DSCount;
                resutl.SGCount = SGCount;
                resutl.DECount = DECount;
                return resutl;
            }
            catch (Exception ex)
            {
                var result = await App.Database.GetAthleteCAByTimeAsync(timeId);
                var baseA = new BaseAthleteCA();
                baseA.Athletes = result.OrderBy(x => x.Posicao).ToList();
                return baseA;
            }
        }
        public async Task<BaseAthleteCA> GetAthleteMarketClose(int timeId)
        {
            try
            {
                var market = Barrel.Current.Get<Market>(key: UrlCartola._mercadoStatus);

                var partidas = await _clubService.GetConfrontos(market.rodada_atual.ToString()).ConfigureAwait(true);
                var lstResult = new List<AthleteCA>();
                var lstResultReservas = new List<AthleteCA>();
                var clubes = await _clubService.ListClubs();
                var _teamSelect = await _teamService.GetTeamsCAIdAsync(timeId);
                var scoreds = await _athleteService.GetAthleteScoredAsync();
                double sumPartial = 0;
                double sumPartialReserva = 0;
                int GCount = 0;
                int ACount = 0;
                int DSCount = 0;
                int SGCount = 0;
                int DECount = 0;

                var substituicoes = await _teamService.GetSubtituicoesIdAsync(_teamSelect.time.time_id);

                if (substituicoes != null && substituicoes.Count > 0)
                {
                    foreach (var item in substituicoes)
                    {
                        _teamSelect.atletas.RemoveAll(x => x.atleta_id == item.saiu.atleta_id);
                        _teamSelect.atletas.Add(_teamSelect.reservas.Find(x => x.atleta_id == item.entrou.atleta_id));
                        if (_teamSelect.capitao_id == item.saiu.atleta_id.ToString())
                            _teamSelect.capitao_id = item.entrou.atleta_id.ToString();
                    }
                }

                for (int i = 0; i < _teamSelect.atletas.Count; i++)
                {
                    double partial = 0;
                    var scouts = Scout.OrganizarScouts(_teamSelect.atletas[i].scout);
                    var scoutsDef = Scout.OrganizarScoutsDef(_teamSelect.atletas[i].scout);
                    try
                    {
                        partial += scoreds.Find(x => x.id_jogador == _teamSelect.atletas[i].atleta_id).pontuacao_dou;
                        var atleScout = scoreds.Find(x => x.id_jogador == _teamSelect.atletas[i].atleta_id);
                        scouts = Scout.OrganizarScouts(atleScout.scout);
                        scoutsDef = Scout.OrganizarScoutsDef(atleScout.scout);
                        if (atleScout.scout != null)
                        {
                            GCount += atleScout.scout.G;
                            ACount += atleScout.scout.A;
                            DSCount += atleScout.scout.DS;
                            SGCount += atleScout.scout.SG;
                            DECount += atleScout.scout.DE;
                        }

                    }
                    catch (Exception)
                    {
                        scouts = "";
                        scoutsDef = "";
                        partial += 0;
                    }


                    var _atletaTela = new AthleteCA();
                    _atletaTela.FotoAtleta = _teamSelect.atletas[i].foto140;
                    _atletaTela.Posicao = _teamSelect.atletas[i].posicao_id;
                    _atletaTela.NomeAtleta = _teamSelect.atletas[i].apelido;
                    _atletaTela.Scouts = scouts;
                    _atletaTela.ScoutsDef = scoutsDef;

                    var clube = clubes.Find(x => x.id == _teamSelect.atletas[i].clube_id);
                    _atletaTela.escudo_clube = clube._30x30;
                    _atletaTela.nome_clube = clube.abreviacao;
                    _atletaTela.capita = false;


                    _atletaTela.variacao_num = _teamSelect.atletas[i].variacao_num == null ? "0.00"
                        : _teamSelect.atletas[i].variacao_num.ToString().Replace(",", ".");
                    _atletaTela.media_num = _teamSelect.atletas[i].media_num == null ? "0.00" : _teamSelect.atletas[i].media_num.ToString().Replace(",", ".");
                    _atletaTela.preco_double = _teamSelect.atletas[i].preco_double;//== null ? "0.00" : double.Parse(athle.preco_double);

                    var valorizacaoDouble = await _athleteService.ScoreToValueDouble(_teamSelect.atletas[i].preco_double);
                    _atletaTela.min_valorizacao = valorizacaoDouble.ToString("F") + "pts";
                    _atletaTela.Partida = partidas.Find(x => x.clube_casa_id == _teamSelect.atletas[i].clube_id
                                                                  || x.clube_visitante_id == _teamSelect.atletas[i].clube_id);


                    _atletaTela.PontosCout = partial;
                    if (_teamSelect.atletas[i].atleta_id == int.Parse(_teamSelect.capitao_id))
                    {
                        _atletaTela.capita = true;
                        _atletaTela.pontos_capita = _atletaTela.PontosCout;
                        _atletaTela.PontosCout += _atletaTela.PontosCout;
                        _atletaTela.pontos_capita_string = "2x " + _atletaTela.pontos_capita.ToString("F2").Replace(",", ".");
                        partial += _atletaTela.pontos_capita;
                    }
                    _atletaTela.NomeTime = _teamSelect.time.nome;
                    _atletaTela.CartoleiroEsquema = _teamSelect.time.nome_cartola + " - " + Schemes.GetSchemes(_teamSelect.time.esquema_id);
                    _atletaTela.escudo_time = _teamSelect.time.url_escudo_png;
                    _atletaTela.IdTime = _teamSelect.time.time_id;
                    if (_atletaTela.PontosCout < 0)
                    {
                        _atletaTela.pont_color = "#b55b49";
                    }
                    else if (_atletaTela.PontosCout > 0)
                    {
                        _atletaTela.pont_color = "#7eaf7e";
                    }
                    else
                    {
                        _atletaTela.pont_color = "Gray";
                    }

                    _atletaTela.IdAleta = _teamSelect.atletas[i].atleta_id;
                    _atletaTela.IdTime = _teamSelect.time.time_id;
                    _atletaTela.foto_timeCA = _teamSelect.time.url_escudo_png;
                    _atletaTela.NomeTime = _teamSelect.time.nome;
                    _atletaTela.CartoleiroEsquema = _teamSelect.time.nome_cartola + " - " + Schemes.GetSchemes(_teamSelect.esquema_id);

                    sumPartial += partial;

                    var result = _atletaTela.PontosCout + valorizacaoDouble;
                    //result = 7,40

                    lstResult.Add(_atletaTela);
                }

                for (int i = 0; i < _teamSelect.reservas.Count; i++)
                {
                    double partial = 0;
                    var scouts = Scout.OrganizarScouts(_teamSelect.reservas[i].scout);
                    var scoutsDef = Scout.OrganizarScoutsDef(_teamSelect.reservas[i].scout);
                    try
                    {
                        partial += scoreds.Find(x => x.id_jogador == _teamSelect.reservas[i].atleta_id).pontuacao_dou;
                        var atleScout = scoreds.Find(x => x.id_jogador == _teamSelect.reservas[i].atleta_id);
                        scouts = Scout.OrganizarScouts(atleScout.scout);
                        scoutsDef = Scout.OrganizarScoutsDef(atleScout.scout);
                        if (atleScout.scout != null)
                        {
                            GCount += atleScout.scout.G;
                            ACount += atleScout.scout.A;
                            DSCount += atleScout.scout.DS;
                            SGCount += atleScout.scout.SG;
                            DECount += atleScout.scout.DE;
                        }

                    }
                    catch (Exception)
                    {
                        scouts = "";
                        scoutsDef = "";
                        partial += 0;
                    }


                    var _atletaTela = new AthleteCA();
                    _atletaTela.FotoAtleta = _teamSelect.reservas[i].foto140;
                    _atletaTela.Posicao = _teamSelect.reservas[i].posicao_id;
                    _atletaTela.NomeAtleta = _teamSelect.reservas[i].apelido;
                    _atletaTela.Scouts = scouts;
                    _atletaTela.ScoutsDef = scoutsDef;

                    var clube = clubes.Find(x => x.id == _teamSelect.reservas[i].clube_id);
                    _atletaTela.escudo_clube = clube._30x30;
                    _atletaTela.nome_clube = clube.abreviacao;
                    _atletaTela.capita = false;


                    _atletaTela.variacao_num = _teamSelect.reservas[i].variacao_num == null ? "0.00"
                        : _teamSelect.reservas[i].variacao_num.ToString().Replace(",", ".");
                    _atletaTela.media_num = _teamSelect.reservas[i].media_num == null ? "0.00" : _teamSelect.reservas[i].media_num.ToString().Replace(",", ".");
                    _atletaTela.preco_double = _teamSelect.reservas[i].preco_double;//== null ? "0.00" : double.Parse(athle.preco_double);
                    _atletaTela.min_valorizacao = await _athleteService.ScoreToValue(_teamSelect.reservas[i].preco_double);
                    _atletaTela.Partida = partidas.Find(x => x.clube_casa_id == _teamSelect.reservas[i].clube_id
                                                                  || x.clube_visitante_id == _teamSelect.reservas[i].clube_id);


                    _atletaTela.PontosCout = partial;

                    if (_atletaTela.PontosCout < 0)
                    {
                        _atletaTela.pont_color = "#b55b49";
                    }
                    else if (_atletaTela.PontosCout > 0)
                    {
                        _atletaTela.pont_color = "#7eaf7e";
                    }
                    else
                    {
                        _atletaTela.pont_color = "Gray";
                    }

                    sumPartialReserva += partial;

                    lstResultReservas.Add(_atletaTela);
                }

                var resutl = new BaseAthleteCA();
                resutl.Time = new TeamCA
                {
                    capitao_id = _teamSelect.capitao_id,
                    esquema_id = _teamSelect.esquema_id,
                    clube_id = _teamSelect.time.clube_id,
                    nome = _teamSelect.time.nome,
                    nome_cartola = _teamSelect.time.nome_cartola,
                    patrimonio = _teamSelect.patrimonio,
                    time_id = _teamSelect.time.time_id,
                    slug = _teamSelect.time.slug,
                    temporada_inicial = _teamSelect.time.temporada_inicial,
                    assinante = _teamSelect.time.assinante,
                    url_escudo_png = _teamSelect.time.url_escudo_png,
                    url_escudo_svg = _teamSelect.time.url_escudo_svg,
                    foto_perfil = _teamSelect.time.foto_perfil
                };
                resutl.GCount = GCount;
                resutl.ACount = ACount;
                resutl.DSCount = DSCount;
                resutl.SGCount = SGCount;
                resutl.DECount = DECount;
                lstResult.ForEach(x => x.ParcialTimeCount = sumPartial);
                lstResultReservas.ForEach(x => x.ParcialTimeCount = sumPartialReserva);

                foreach (var item in lstResult)
                {
                    item.GCount = GCount;
                    item.ACount = ACount;
                    item.DSCount = DSCount;
                    item.SGCount = SGCount;
                    item.DECount = DECount;
                    await App.Database.InsertAthleteCAAsync(item);
                }
                foreach (var item in lstResultReservas)
                {
                    item.GCount = GCount;
                    item.ACount = ACount;
                    item.DSCount = DSCount;
                    item.SGCount = SGCount;
                    item.DECount = DECount;
                    await App.Database.InsertAthleteCAAsync(item);
                }
                //lstResult.ForEach(x => App.Database.InsertAthleteCAAsync(x));

                resutl.Athletes = lstResult.OrderBy(x => x.Posicao).ToList();
                resutl.Reservas = lstResultReservas.OrderBy(x => x.Posicao).ToList();
                return resutl;

            }
            catch (Exception ex)
            {
                var result = await App.Database.GetAthleteCAByTimeAsync(timeId);
                var baseA = new BaseAthleteCA();
                baseA.GCount = result.FirstOrDefault().GCount;
                baseA.ACount = result.FirstOrDefault().ACount;
                baseA.DSCount = result.FirstOrDefault().DSCount;
                baseA.SGCount = result.FirstOrDefault().SGCount;
                baseA.DECount = result.FirstOrDefault().DECount;

                baseA.Athletes = result.OrderBy(x => x.Posicao).ToList();
                return baseA;
            }
        }
    }
}
