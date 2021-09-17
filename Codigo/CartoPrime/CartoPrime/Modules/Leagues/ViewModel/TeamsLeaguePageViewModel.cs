using CartoPrime.Helpers;
using CartoPrime.Http.Interfaces;
using CartoPrime.Interfaces;
using CartoPrime.Models;
using CartoPrime.Models.Enum;
using CartoPrime.Modules.Base;
using CartoPrime.Modules.Team.Views;
using CartoPrime.Resources;
using CartoPrime.Services;
using CartoPrime.ViewModels;
using CartoPrime.Views;
using MonkeyCache.FileStore;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Input;
using Xamarin.Forms;

namespace CartoPrime.Modules.Leagues.ViewModels
{
    public class TeamsLeaguePageViewModel : BaseViewModel<TeamsLeaguePageViewModel>//, IQueryAttributable, INotifyPropertyChanged
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
        //        League = JsonConvert.DeserializeObject<League>(paran);
        //        _ = GetInitial().ConfigureAwait(true);
        //        HideLoading();
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHelper.ErrorHandle(ex, "TeamsLeaguePageViewModel", nameof(Init));
        //        HideLoading();
        //    }
        //}
        #endregion
        #region Properties
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

        private bool _selectFilterRound;
        public bool SelectFilterRound
        {
            get { return _selectFilterRound; }
            set
            {
                SetProperty(ref _selectFilterRound, value);
            }
        }

        private bool _selectFilterChampionship;
        public bool SelectFilterChampionship
        {
            get { return _selectFilterChampionship; }
            set
            {
                SetProperty(ref _selectFilterChampionship, value);
            }
        }

        private League _league;
        public League League
        {
            get { return _league; }
            set
            {
                SetProperty(ref _league, value);
            }
        }

        private BaseTeamLeague _teamsLeague;
        public BaseTeamLeague TeamsLeague
        {
            get { return _teamsLeague; }
            set
            {
                SetProperty(ref _teamsLeague, value);
            }
        }

        private List<Time> _times;
        public List<Time> Times

        {
            get { return _times; }
            set
            {
                SetProperty(ref _times, value);
            }
        }

        private Time _teamSelected;
        public Time TeamSelected
        {
            get { return _teamSelected; }
            set
            {
                SetProperty(ref _teamSelected, value);
                if (value != null)
                    _ = ViewItem(value).ConfigureAwait(true);
            }
        }
        #endregion

        #region Comands
        public ICommand RoundFilterCommand { get; set; }
        public ICommand ChampionshipFilterCommand { get; set; }
        public ICommand RefreshTeamsCommand
        {
            get
            {
                return new Command(async () =>
                {
                    //await UpdateTeams();
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

                    await GetInitial().ConfigureAwait(true);

                    IsRefreshing = false;
                });
            }
        }
        #endregion

        public TeamsLeaguePageViewModel(string _slug)
        {
            RoundFilterCommand = new Command(RoundFilter);
            ChampionshipFilterCommand = new Command(ChampionshipFilter);
            SelectFilterChampionship = false;
            SelectFilterRound = true;
            League = new League { slug = _slug };
            _ = GetInitial().ConfigureAwait(true);
        }

        #region Metodos
        private async Task GetInitial()
        {
            try
            {
                ShowLoading(AppResources.LOADING);
                var mark = await marketService.Information().ConfigureAwait(true);
                if (mark == null || mark.status_mercado == (int)EMarket.Support)
                {
                    Xamarin.Forms.DependencyService.Get<IMessage>().LongAlert("Por favor, tente novamente.");
                }
                TeamsLeague = await leaguesService.GetTeamsLeaguesAsync(League.slug);
                var _times = TeamsLeague.times.OrderByDescending(x=>x.pontos.rodada).ToList();


                if (mark.status_mercado == (int)EMarket.Closed)
                {
                    var newTimes = await UpdateTeams(_times).ConfigureAwait(true);
                    if (newTimes != null)
                        Times = newTimes;
                    else
                        Times = _times;
                }
                else
                {
                    Times = _times;
                }
                AnalyticsEvents.AnalyticsHandle("TeamsLeaguePageView", nameof(GetInitial));

            }
            catch (Exception ex)
            {
                AnalyticsEvents.AnalyticsHandle("TeamsLeaguePageView", nameof(GetInitial), ex.StackTrace);

            }
            //pageLoading.HidePopup();
            HideLoading();
            IsBusy = false;
        }

        private async Task<List<Time>> UpdateTeams(List<Time> times)
        {
            try
            {
                var date = await partialService.GetLeagueMarketClose(times).ConfigureAwait(true);
                AnalyticsEvents.AnalyticsHandle("TeamsLeaguePageView", nameof(UpdateTeams));

                return date;

            }
            catch (Exception ex)
            {
                AnalyticsEvents.AnalyticsHandle("TeamsLeaguePageView", nameof(UpdateTeams), ex.StackTrace);

                return null;
            }
        }
        #endregion

        #region Events
        private async void RoundFilter(object obj)
        {
            SelectFilterChampionship = false;
            SelectFilterRound = true;
            var TeamsUp = await OrderByListRound();
            for (int i = 0; i < TeamsUp.Count; i++)
            {
                TeamsUp[i].ranking.rodada = (i + 1);
            }
            this.Times = TeamsUp;

        }
        private async void ChampionshipFilter(object obj)
        {
            SelectFilterChampionship = true;
            SelectFilterRound = false;
            var TeamsUp = await OrderByListChampionship();
            for (int i = 0; i < TeamsUp.Count; i++)
            {
                TeamsUp[i].ranking.rodada = (i + 1);
            }
            this.Times = TeamsUp;
        }



        private async Task<List<Time>> OrderByListChampionship()
        {
            var result = TeamsLeague.times.OrderByDescending(x => x.pontos.campeonato).ToList();
            return result;
        }
        private async Task<List<Time>> OrderByListRound()
        {
            var result = TeamsLeague.times.OrderByDescending(x => x.pontos.rodada).ToList();
            return result;
        }
        private async Task ViewItem(object sender)
        {
            try
            {
                    int idTime = TeamSelected.time_id;
                    TeamSelected = null;
                    AnalyticsEvents.AnalyticsHandle("TeamsLeaguePageView", nameof(ViewItem));

                    await App.Current.MainPage.Navigation.PushAsync(new AthletesPage(idTime));
            }
            catch (Exception ex)
            {
                AnalyticsEvents.AnalyticsHandle("TeamsLeaguePageView", nameof(ViewItem), ex.StackTrace);

                Xamarin.Forms.DependencyService.Get<IMessage>().LongAlert("Aguarde o mercado fechar para ver o time do seu amigo!");
            }
        }
        #endregion
    }
}
