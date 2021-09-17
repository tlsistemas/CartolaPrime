using CartoPrime.Helpers;
using CartoPrime.Models;
using CartoPrime.Modules.Base;
using CartoPrime.Resources;
using CartoPrime.Services;
using MonkeyCache.FileStore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CartoPrime.ViewModels
{
    public class MostClimbedPageViewModel : BaseViewModel<MostClimbedPageViewModel>
    {
        #region Comands
        public ICommand AddTeamsCommand { get; set; }
        public ICommand RefreshButtonCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await GetInforsAsync().ConfigureAwait(true);
                });
            }
        }
        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsRefreshing = true;

                    await GetInforsAsync().ConfigureAwait(true);

                    IsRefreshing = false;
                });
            }
        }
        #endregion

        #region Properties
        private List<AthleteClimbed> _players;
        public List<AthleteClimbed> Players
        {
            get { return _players; }
            set { SetProperty(ref _players, value); }
        }
        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }
        #endregion

        public MostClimbedPageViewModel()
        {
            GetInforsAsync().ConfigureAwait(true);
        }

        private async Task GetInforsAsync()
        {
            if (IsBusy) return;
            IsBusy = true;
            ShowLoading(AppResources.LOADING);
            try
            {
                var StatusMercado = Barrel.Current.Get<Market>(key: UrlCartola._mercadoStatus);
                Players = await athleteService.GetAthleteClimbedsync(true).ConfigureAwait(true);
                var partidas = await _clubService.GetConfrontos(StatusMercado.rodada_atual.ToString()).ConfigureAwait(true);

                var marketAthle = await marketService.AthletesMarket().ConfigureAwait(true);
                for (int i = 0; i < Players.Count; i++)
                {
                    var athle = marketAthle.Find(x => x.atleta_id == Players[i].Atleta.atleta_id);
                    Players[i].Atleta.variacao_num = athle.variacao_num == null ? 0 : double.Parse(athle.variacao_num);
                    Players[i].Atleta.media_num = athle.media_num;
                    Players[i].Atleta.pontos_num = athle.pontos_double;
                    Players[i].Atleta.preco_double = athle.preco_double;//== null ? "0.00" : double.Parse(athle.preco_double);
                    Players[i].Atleta.min_valorizacao = await athleteService.ScoreToValue(athle.preco_double);
                    Players[i].Atleta.Partida = partidas.Find(x => x.clube_casa_id == Players[i].clube_id
                                                              || x.clube_visitante_id == Players[i].clube_id);
                    Players[i].Count = i + 1;
                }

                AnalyticsEvents.AnalyticsHandle("MostClimbedPageView", nameof(GetInforsAsync));

            }
            catch (System.Exception ex)
            {
                AnalyticsEvents.AnalyticsHandle("MostClimbedPageView", nameof(GetInforsAsync), ex.StackTrace);

            }
           
            


            HideLoading();
            IsBusy = false;
        }

        private async Task UpdateList()
        {
            if (IsBusy) return;
            IsBusy = true;
            ShowLoading(AppResources.LOADING);

            Players = await athleteService.GetAthleteClimbedsync(true).ConfigureAwait(true);

            HideLoading();
            IsBusy = false;
        }
    }
}
