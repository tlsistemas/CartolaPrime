using CartoPrime.Helpers;
using CartoPrime.Models;
using CartoPrime.Models.Enum;
using CartoPrime.Modules.Base;
using CartoPrime.Services;
using MonkeyCache.FileStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CartoPrime.Modules.Team.ViewModels
{
    public class ClimbByAthleteViewModel : BaseViewModel<ClimbByAthleteViewModel>
    {
        #region Propertes
        private AtletaTime _team;
        public AtletaTime Team
        {
            get { return _team; }
            set
            {
                SetProperty(ref _team, value);
            }
        }

        private List<Athlete> _athletes;
        public List<Athlete> Athletes
        {
            get { return _athletes; }
            set
            {
                SetProperty(ref _athletes, value);
            }
        }
        private List<Athlete> _athletesReservas;
        public List<Athlete> AthletesReservas
        {
            get { return _athletesReservas; }
            set
            {
                SetProperty(ref _athletesReservas, value);
            }
        }

        private IEnumerable<int> _esquemas;
        public IEnumerable<int> Esquemas
        {
            get { return _esquemas; }
            set
            {
                SetProperty(ref _esquemas, value);
            }
        }

        private string _esquemasSelected;
        public string EsquemasSelected
        {
            get { return _esquemasSelected; }
            set
            {
                SetProperty(ref _esquemasSelected, value);
            }
        }

        private int _esquemasIndex;
        public int EsquemasIndex
        {
            get { return _esquemasIndex; }
            set
            {
                SetProperty(ref _esquemasIndex, value);
            }
        }
        #endregion

        #region Comands
        public ICommand FindAthleteCommand { get; set; }
        #endregion

        #region Construtor
        public ClimbByAthleteViewModel()
        {
            Title = "Escalação";
            FindAthleteCommand = new Command<object>(async (item) => await FindAthletePosition(item));
            _ = GetInitial().ConfigureAwait(true);
        }
        #endregion

        #region Metodos
        private async Task GetInitial()
        {
            ShowLoading(Resources.AppResources.LOADING);
            try
            {
                var user = Barrel.Current.Get<UserCA>(key: UrlCartola._timeAuth);
                Team = await teamService.GetMyTeamsAsync(user.glbstring).ConfigureAwait(true);

                var substituicoes = await teamService.GetSubtituicoesIdAsync(Team.time.time_id);
                if (substituicoes != null && substituicoes.Count > 0)
                {
                    foreach (var item in substituicoes)
                    {
                        Team.atletas.RemoveAll(x => x.atleta_id == item.saiu.atleta_id);
                        Team.atletas.Add(Team.reservas.Find(x => x.atleta_id == item.entrou.atleta_id));
                        if (Team.capitao_id == item.saiu.atleta_id.ToString())
                            Team.capitao_id = item.entrou.atleta_id.ToString();
                    }
                }

                Athletes = Team.atletas.OrderBy(x => x.posicao_id).ToList();
                AthletesReservas = Team.reservas.OrderBy(x => x.posicao_id).ToList();

                var market = new Market();
                market = Barrel.Current.Get<Market>(key: UrlCartola._mercadoStatus);
                var partidas = await _clubService.GetConfrontos(market.rodada_atual.ToString()).ConfigureAwait(true);
                var marketAthle = await marketService.AthletesMarket().ConfigureAwait(true);
                for (int i = 0; i < Athletes.Count; i++)
                {
                    var athle = marketAthle.Find(x => x.atleta_id == Athletes[i].atleta_id);
                    Athletes[i].variacao_num = athle.variacao_num == null ? 0 : double.Parse(athle.variacao_num);
                    Athletes[i].media_num = athle.media_num;
                    Athletes[i].preco_double = athle.preco_double;//== null ? "0.00" : double.Parse(athle.preco_double);
                    Athletes[i].min_valorizacao = await athleteService.ScoreToValue(Athletes[i].preco_double);
                    Athletes[i].Partida = partidas.Find(x => x.clube_casa_id == Athletes[i].clube_id
                                                                      || x.clube_visitante_id == Athletes[i].clube_id);
                }
                for (int i = 0; i < AthletesReservas.Count; i++)
                {
                    var athle = marketAthle.Find(x => x.atleta_id == AthletesReservas[i].atleta_id);
                    AthletesReservas[i].variacao_num = athle.variacao_num == null ? 0 : double.Parse(athle.variacao_num);
                    AthletesReservas[i].media_num = athle.media_num;
                    AthletesReservas[i].preco_double = athle.preco_double;//== null ? "0.00" : double.Parse(athle.preco_double);
                    AthletesReservas[i].min_valorizacao = await athleteService.ScoreToValue(AthletesReservas[i].preco_double);
                    AthletesReservas[i].Partida = partidas.Find(x => x.clube_casa_id == AthletesReservas[i].clube_id
                                                                      || x.clube_visitante_id == AthletesReservas[i].clube_id);
                }
                AnalyticsEvents.AnalyticsHandle("ClimbByAthleteView", nameof(GetInitial));

            }
            catch (Exception ex)
            {
                AnalyticsEvents.AnalyticsHandle("ClimbByAthleteView", nameof(GetInitial), ex.StackTrace);

            }

            HideLoading();
        }

        private async Task FindAthletePosition(object position)
        {

        }
        #endregion
    }
}
