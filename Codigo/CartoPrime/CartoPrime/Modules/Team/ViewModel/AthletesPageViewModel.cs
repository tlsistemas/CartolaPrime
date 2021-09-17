using CartoPrime.Helpers;
using CartoPrime.Interfaces;
using CartoPrime.Models;
using CartoPrime.Models.Enum;
using CartoPrime.Modules.Base;
using CartoPrime.Resources;
using CartoPrime.Services;
using CartoPrime.Views;
using MonkeyCache.FileStore;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Input;
using Xamarin.Forms;

namespace CartoPrime.Modules.Team.ViewModels
{
    public class AthletesPageViewModel : BaseViewModel<AthletesPageViewModel>//, IQueryAttributable, INotifyPropertyChanged
    {
        #region Parameters
        //public async void ApplyQueryAttributes(IDictionary<string, string> query)
        //{
        //    string paran = HttpUtility.UrlDecode(query["paran"]);
        //    await Init(paran).ConfigureAwait(true);
        //}

        //private async Task Init(string paran)
        //{
        //    try
        //    {
        //        ShowLoading(AppResources.LOADING);
        //        TeamCA = JsonConvert.DeserializeObject<TeamCA>(paran);
        //        if (CrossConnectivity.Current.IsConnected && !Barrel.Current.IsExpired(key: UrlCartola._mercadoStatus))
        //            Market = Barrel.Current.Get<Market>(key: UrlCartola._mercadoStatus);
        //        else if (!CrossConnectivity.Current.IsConnected)
        //        {
        //            var temp = await App.Database.GetMarketsAsync();
        //            Market = temp.LastOrDefault();
        //        }

        //        if (Market == null)
        //        {
        //            Market = await marketService.Information();
        //        }

        //        if (Market.status_mercado == (int)EMarket.Open)
        //            Round = Market.rodada_atual - 1;
        //        else
        //            Round = Market.rodada_atual;

        //        await UpdateAthletes();
        //        HideLoading();
        //        IsBusy = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHelper.ErrorHandle(ex, "AthletesPageViewModel", nameof(InitAsync));
        //        HideLoading();
        //    }
        //}
        #endregion

        #region Comands
        public ICommand AddTeamsCommand { get; set; }
        public ICommand DeleteTeamCommand { get; set; }
        public ICommand RefreshTeamsCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await UpdateAthletes().ConfigureAwait(true);
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

                    await UpdateAthletes().ConfigureAwait(true);

                    IsRefreshing = false;
                });
            }
        }

        public ICommand PreviousTeamsCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsRefreshing = true;

                    await ProviRound().ConfigureAwait(true);

                    IsRefreshing = false;
                });
            }
        }

        public ICommand NextTeamsCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsRefreshing = true;

                    await NextRound().ConfigureAwait(true);

                    IsRefreshing = false;
                });
            }
        }
        #endregion

        #region Properties
        private LoadingPopupPage pageLoading;
        private TeamCA _teamCA;
        public TeamCA TeamCA
        {
            get { return _teamCA; }
            set
            {
                SetProperty(ref _teamCA, value);
            }
        }
        private int _round;
        public int Round
        {
            get { return _round; }
            set
            {
                SetProperty(ref _round, value);
            }
        }
        private AthleteCA _athleteCA;
        public AthleteCA AthleteCA
        {
            get { return _athleteCA; }
            set
            {
                SetProperty(ref _athleteCA, value);
            }
        }
        private Market _market;
        public Market Market
        {
            get { return _market; }
            set
            {
                SetProperty(ref _market, value);
            }
        }
        private List<AthleteCA> _athletes;
        public List<AthleteCA> Athletes
        {
            get { return _athletes; }
            set
            {
                SetProperty(ref _athletes, value);
            }
        }
        private List<AthleteCA> _reservas;

        public List<AthleteCA> Reservas
        {
            get { return _reservas; }
            set
            {
                SetProperty(ref _reservas, value);
            }
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

        private int _id_time;
        public int id_time
        {
            get { return _id_time; }
            set
            {
                SetProperty(ref _id_time, value);
            }
        }

        private int _GCount;
        public int GCount
        {
            get { return _GCount; }
            set
            {
                SetProperty(ref _GCount, value);
            }
        }
        private int _ACount;
        public int ACount
        {
            get { return _ACount; }
            set
            {
                SetProperty(ref _ACount, value);
            }
        }
        private int _DSCount;
        public int DSCount
        {
            get { return _DSCount; }
            set
            {
                SetProperty(ref _DSCount, value);
            }
        }
        private int _DECount;
        public int DECount
        {
            get { return _DECount; }
            set
            {
                SetProperty(ref _DECount, value);
            }
        }
        private int _SGCount;
        public int SGCount
        {
            get { return _SGCount; }
            set
            {
                SetProperty(ref _SGCount, value);
            }
        }
        private string _ScoutString;
        public string ScoutString
        {
            get { return $"DS {DSCount} | DE {DECount} | SG {SGCount}"; }
            set
            {
                SetProperty(ref _ScoutString, value);
            }
        }

        private string _valorAtleta;
        public string ValorAtleta
        {
            get { return _valorAtleta + " - "; }
            set
            {
                SetProperty(ref _valorAtleta, value);
            }
        }

        private string _valorizacaoAtleta;
        public string ValorizacaoAtleta
        {
            get { return _valorizacaoAtleta; }
            set
            {
                SetProperty(ref _valorizacaoAtleta, value);
            }
        }


        #endregion

        public AthletesPageViewModel(int idTime)
        {
            Title = "Parciais";
            TeamCA = new TeamCA { time_id = idTime };
            DeleteTeamCommand = new Command(DeleteTeam);
            //_ = Init(json);
            GetInitial().ConfigureAwait(true);
        }



        #region Metodos
        private async Task GetInitial()
        {
            try
            {
                ShowLoading(AppResources.LOADING);

                if (CrossConnectivity.Current.IsConnected && !Barrel.Current.IsExpired(key: UrlCartola._mercadoStatus))
                    Market = Barrel.Current.Get<Market>(key: UrlCartola._mercadoStatus);
                else if (!CrossConnectivity.Current.IsConnected)
                {
                    var temp = await App.Database.GetMarketsAsync().ConfigureAwait(true);
                    Market = temp.LastOrDefault();
                }

                if (Market == null)
                {
                    Market = await marketService.Information().ConfigureAwait(true);
                }

                if (Market.status_mercado == (int)EMarket.Open)
                {
                    Round = Market.rodada_atual - 1;
                    var result = await partialService.GetAthleteMarketOpen(TeamCA.time_id, Round).ConfigureAwait(true);
                    this.Athletes = result.Athletes;
                    this.Reservas = result.Reservas;
                    this.AthleteCA = Athletes.FirstOrDefault();
                    this.GCount = result.GCount;
                    this.ACount = result.ACount;
                    this.SGCount = result.SGCount;
                    this.DECount = result.DECount;
                    this.DSCount = result.DSCount;
                }
                else
                {
                    Round = Market.rodada_atual;
                    await UpdateAthletes().ConfigureAwait(true);
                }
                //await UpdateAthletes();



                AnalyticsEvents.AnalyticsHandle("AthletesPageView", nameof(GetInitial));

                HideLoading();
            }
            catch (Exception ex)
            {
                AnalyticsEvents.AnalyticsHandle("AthletesPageView", nameof(GetInitial), ex.StackTrace);

                HideLoading();
            }
        }
        private async Task UpdateAthletes()
        {
            try
            {
                //pageLoading = new LoadingPopupPage();
                //await App.Current.MainPage.Navigation.PushPopupAsync(pageLoading, true);
                if (IsBusy) return;
                IsBusy = true;
                ShowLoading(AppResources.LOADING);


                if (Market == null || Market.status_mercado == (int)EMarket.Support)
                {
                    Xamarin.Forms.DependencyService.Get<IMessage>().LongAlert("Por favor, tente novamente.");
                }
                else if (Market.rodada_atual == 1 && Market.status_mercado == (int)EMarket.Open || Market.status_mercado == (int)EMarket.Support)
                {
                    this.Athletes = new List<AthleteCA>();

                    this.AthleteCA = new AthleteCA
                    {
                        foto_timeCA = TeamCA.url_escudo_png,
                        NomeTime = TeamCA.nome,
                        CartoleiroEsquema = TeamCA.CartoleiroEsquema,
                        IdTime = TeamCA.time_id,
                        ParcialTime = "0"
                    };
                    this.GCount = 0;
                    this.ACount = 0;
                    this.SGCount = 0;
                    this.DECount = 0;
                    this.DSCount = 0;
                    Xamarin.Forms.DependencyService.Get<IMessage>().LongAlert("Parciais atualizadas");
                }
                else if (Market.status_mercado == (int)EMarket.Closed)
                {
                    var result = new BaseAthleteCA();
                    if (Round != Market.rodada_atual)
                        result = await partialService.GetAthleteMarketOpen(TeamCA.time_id, Round).ConfigureAwait(true);
                    else
                        result = await partialService.GetAthleteMarketClose(TeamCA.time_id).ConfigureAwait(true);
                    this.Athletes = result.Athletes;
                    this.Reservas = result.Reservas;
                    this.AthleteCA = Athletes.FirstOrDefault();
                    this.GCount = result.GCount;
                    this.ACount = result.ACount;
                    this.SGCount = result.SGCount;
                    this.DECount = result.DECount;
                    this.DSCount = result.DSCount;
                    Xamarin.Forms.DependencyService.Get<IMessage>().LongAlert("Parciais atualizadas");
                }
                else if (Market.status_mercado == (int)EMarket.Open)
                {
                    var result = await partialService.GetAthleteMarketOpen(TeamCA.time_id, Round).ConfigureAwait(true);
                    this.Athletes = result.Athletes;
                    this.Reservas = result.Reservas;

                    this.AthleteCA = Athletes.FirstOrDefault();
                    this.GCount = result.GCount;
                    this.ACount = result.ACount;
                    this.SGCount = result.SGCount;
                    this.DECount = result.DECount;
                    this.DSCount = result.DSCount;
                    Xamarin.Forms.DependencyService.Get<IMessage>().LongAlert("Parciais atualizadas");
                }

                id_time = TeamCA.time_id;
                AnalyticsEvents.AnalyticsHandle("AthletesPageView", nameof(UpdateAthletes));

            }
            catch (Exception ex)
            {
                var a = ex.Message;
                AnalyticsEvents.AnalyticsHandle("AthletesPageView", nameof(UpdateAthletes), ex.StackTrace);

            }
            //pageLoading.HidePopup();
            HideLoading();
            IsBusy = false;
        }

        #endregion

        #region Events

        private async void DeleteTeam()
        {
            try
            {
                //pageLoading = new LoadingPopupPage();
                //await App.Current.MainPage.Navigation.PushPopupAsync(pageLoading, true);
                if (IsBusy) return;
                IsBusy = true;
                ShowLoading(AppResources.LOADING);

                var answer = await App.Current.MainPage.DisplayAlert("Atenção", "Deseja Realmente excluir o time", "Sim", "não");

                if (answer)
                {

                    var time = await App.Database.GetTeamCAAsync(id_time).ConfigureAwait(true);

                    // Verificar
                    await App.Database.DeleteTeamCAAsync(time); await App.Current.MainPage.DisplayAlert("Concluído", "Time excluido. Atualize sua lista de times", "OK");

                    AnalyticsEvents.AnalyticsHandle("AthletesPageView", nameof(DeleteTeam));

                    await Application.Current.MainPage.Navigation.PopModalAsync();
                }
            }
            catch (Exception ex)
            {
                var a = ex.Message;
                AnalyticsEvents.AnalyticsHandle("AthletesPageView", nameof(DeleteTeam), ex.StackTrace);

            }
            //pageLoading.HidePopup();
            HideLoading();
            IsBusy = false;
        }
        private async Task NextRound()
        {
            Round++;
            if (Round <= Market.rodada_atual)
                await UpdateAthletes();
            else
            {
                Round--;
                Xamarin.Forms.DependencyService.Get<IMessage>().LongAlert("Não a próxima rodada.");
            }
        }
        private async Task ProviRound()
        {
            Round--;
            if (Round >= 1)
                await UpdateAthletes().ConfigureAwait(true);
            else
            {
                Round++;
                Xamarin.Forms.DependencyService.Get<IMessage>().LongAlert("Não a próxima rodada.");
            }
        }
        private async void OnAddItem(object obj)
        {
            //await App.Current.MainPage.Navigation.PushAsync(new InsertTeamsPage());
            //await Shell.Current.GoToAsync(nameof(InsertTeamsPage));
        }

        //public async void OnNavigatedTo(INavigationParameters Partidaameters)
        //{
        //    //pageLoading = new LoadingPopupPage();
        //    //await App.Current.MainPage.Navigation.PushPopupAsync(pageLoading, true);
        //    if (IsBusy) return;
        //    IsBusy = true;
        //    ShowLoading(AppResources.LOADING);
        //    TeamCA = JsonConvert.DeserializeObject<TeamCA>(Partidaameters["param"].ToString());

        //    if (CrossConnectivity.Current.IsConnected && !Barrel.Current.IsExpired(key: UrlCartola._mercadoStatus))
        //        Market = Barrel.Current.Get<Market>(key: UrlCartola._mercadoStatus);
        //    else if (!CrossConnectivity.Current.IsConnected)
        //    {
        //        var temp = await App.Database.GetMarketsAsync();
        //        Market = temp.LastOrDefault();
        //    }

        //    if (Market == null)
        //    {
        //        Market = await _marketService.Information();
        //    }

        //    if (Market.status_mercado == (int)EMarket.Open)
        //        Round = Market.rodada_atual - 1;
        //    else
        //        Round = Market.rodada_atual;



        //    //pageLoading.HidePopup();
        //    HideLoading();
        //    IsBusy = false;

        //    await UpdateAthletes();
        //}
        #endregion
    }
}
