using CartoPrime.Helpers;
using CartoPrime.Interfaces;
using CartoPrime.Models;
using CartoPrime.Models.Enum;
using CartoPrime.Modules.Base;
using CartoPrime.Modules.Team.Views;
using CartoPrime.Resources;
using CartoPrime.Services;
using CartoPrime.Views;
using MonkeyCache.FileStore;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CartoPrime.Modules.Team.ViewModels
{
    public class TeamsPageViewModel : BaseViewModel<TeamsPageViewModel>
    {
        #region Comands
        public ICommand AddTeamsCommand { get; }
        public ICommand RefreshTeamsCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await UpdateTeams();
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

                    await UpdateTeams();

                    IsRefreshing = false;
                });
            }
        }
        public ICommand ViewItemCommand { get; set; }
        #endregion

        #region Properties
        private TeamCA _teamSelected;
        public TeamCA TeamSelected
        {
            get { return _teamSelected; }
            set
            {
                SetProperty(ref _teamSelected, value);
                if (value != null)
                    _ = ViewItem(value).ConfigureAwait(true);
            }
        }

        private List<TeamCA> _teams;
        public List<TeamCA> Teams
        {
            get { return _teams; }
            set
            {
                SetProperty(ref _teams, value);
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
        private bool _addedTime;
        public bool AddedTime
        {
            get { return _addedTime; }
            set { SetProperty(ref _addedTime, value); }
        }
        private bool _notTime;
        public bool NotTime
        {
            get { return _notTime; }
            set { SetProperty(ref _notTime, value); }
        }
        #endregion

        public TeamsPageViewModel()
        {
            Title = "Parciais";
            AddTeamsCommand = new Command(OnAddItem);
            //ViewItemCommand = new Command<object>(ViewItem);
            GetInitial().ConfigureAwait(true);
        }

        #region Metodos
        private async Task GetInitial()
        {
            try
            {
                //pageLoading = new LoadingPopupPage();
                //await App.Current.MainPage.Navigation.PushPopupAsync(pageLoading, true);
                if (IsBusy) return;
                IsBusy = true;
                ShowLoading(AppResources.LOADING);
                //var mark = await marketService.Information().ConfigureAwait(true);
                Teams = await teamService.GetTeamsCAAsync().ConfigureAwait(true);
                Teams = Teams.OrderByDescending(x => x.pontos_count).ToList();
                for (int i = 0; i < this.Teams.Count; i++)
                {
                    Teams[i].posicao = (i + 1).ToString();
                }
                if (this.Teams.Count > 0)
                {
                    AddedTime = true;
                    NotTime = false;
                }
                else
                {
                    AddedTime = false;
                    NotTime = true;
                }
                //pageLoading.HidePopup();
                HideLoading();
                IsBusy = false;
                AnalyticsEvents.AnalyticsHandle("TeamsPageView", nameof(GetInitial));

            }
            catch (Exception ex)
            {
                var test = ex.Message;
                AnalyticsEvents.AnalyticsHandle("TeamsPageView", nameof(GetInitial), ex.StackTrace);

            }

        }
        private async Task UpdateTeams()
        {
            try
            {
                //pageLoading = new LoadingPopupPage();
                //await App.Current.MainPage.Navigation.PushPopupAsync(pageLoading, true);
                if (IsBusy) return;
                IsBusy = true;
                ShowLoading(AppResources.LOADING);
                var mark = await marketService.Information().ConfigureAwait(true);

                if (mark == null || mark.status_mercado == (int)EMarket.Support)
                {

                    Xamarin.Forms.DependencyService.Get<IMessage>().LongAlert("Por favor, tente novamente.");
                }
                else if (mark.status_mercado == (int)EMarket.Closed)
                {
                    this.Teams = await partialService.GetMarketClose().ConfigureAwait(true);
                    Xamarin.Forms.DependencyService.Get<IMessage>().LongAlert("Parciais atualizadas");
                }
                else if (mark.status_mercado == (int)EMarket.Open)
                {
                    this.Teams = await partialService.GetMarketOpen(mark.rodada_atual - 1).ConfigureAwait(true);
                    Xamarin.Forms.DependencyService.Get<IMessage>().LongAlert("Parciais atualizadas");
                }
                if (this.Teams.Count > 0)
                {
                    AddedTime = true;
                    NotTime = false;
                }
                else
                {
                    AddedTime = false;
                    NotTime = true;
                }

                AnalyticsEvents.AnalyticsHandle("TeamsPageView", nameof(UpdateTeams));


            }
            catch (Exception ex)
            {
                var a = ex.Message;
                AnalyticsEvents.AnalyticsHandle("TeamsPageView", nameof(UpdateTeams), ex.StackTrace);

            }
            //pageLoading.HidePopup();
            HideLoading();
            IsBusy = false;
        }

        #endregion

        #region Events
        private async void OnAddItem(object obj)
        {
            AnalyticsEvents.AnalyticsHandle("TeamsPageView", nameof(OnAddItem));

            await base.Navigate(Pages.InsertTeamsPage).ConfigureAwait(true);
        }

        private async Task ViewItem(object sender)
        {
            try
            {
                //Market _mak = new Market();
                //if (CrossConnectivity.Current.IsConnected && !Barrel.Current.IsExpired(key: UrlCartola._mercadoStatus))
                //{
                //    _mak = Barrel.Current.Get<Market>(key: UrlCartola._mercadoStatus);
                //}
                //else if (!CrossConnectivity.Current.IsConnected)
                //{
                //    var temp = await App.Database.GetMarketsAsync();
                //    _mak = temp.LastOrDefault();
                //}

                //if (_mak.rodada_atual < 1)
                //{
                //    Xamarin.Forms.DependencyService.Get<IMessage>().LongAlert("Na 1ª rodada aguarde o mercado fechar para ver o time do seu amigo!");
                //}
                //else
                //{
                    //var json = JsonConvert.SerializeObject(TeamSelected);
                    int idTime = TeamSelected.time_id;
                    TeamSelected = null;
                    await App.Current.MainPage.Navigation.PushAsync(new AthletesPage(idTime));
                    //await Shell.Current.GoToAsync(new AthletesPage(json)).ConfigureAwait(true);
                    //await Shell.Current.GoToAsync($"{Pages.AthletesPage}?paran={json}").ConfigureAwait(true);
                //}

                AnalyticsEvents.AnalyticsHandle("TeamsPageView", nameof(ViewItem));

            }
            catch (Exception ex)
            {
                AnalyticsEvents.AnalyticsHandle("TeamsPageView", nameof(ViewItem), ex.StackTrace);

                Xamarin.Forms.DependencyService.Get<IMessage>().LongAlert("Aguarde o mercado fechar para ver o time do seu amigo!");
            }

        }

        #endregion
    }
}
