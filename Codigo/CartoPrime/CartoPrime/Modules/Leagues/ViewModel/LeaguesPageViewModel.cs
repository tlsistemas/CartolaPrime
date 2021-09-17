using CartoPrime.Helpers;
using CartoPrime.Interfaces;
using CartoPrime.Models;
using CartoPrime.Modules.Authentication.ViewModels;
using CartoPrime.Modules.Authentication.Views;
using CartoPrime.Modules.Base;
using CartoPrime.Modules.Leagues.Views;
using CartoPrime.Resources;
using CartoPrime.Services;
using CartoPrime.Views;
using MonkeyCache.FileStore;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CartoPrime.Modules.Leagues.ViewModels
{
    public class LeaguesPageViewModel : BaseViewModel<LeaguesPageViewModel>
    {
        #region Command
        public ICommand LoginCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsRefreshing = true;
                    await App.Current.MainPage.Navigation.PushAsync(new LoginPage());
                    // await NavigationService.NavigateAsync(Pages.Login);
                    IsRefreshing = false;
                });
            }
        }
        public ICommand RefreshButtonCommand
        {
            get
            {
                return new Command(async () =>
                {
                    GetInitial();
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

                    GetInitial();

                    IsRefreshing = false;
                });
            }
        }
        public ICommand ViewItemCommand { get; set; }
        #endregion

        #region Properties
        private League _leagueSelected;
        public League LeagueSelected
        {
            get { return _leagueSelected; }
            set
            {
                SetProperty(ref _leagueSelected, value);
                if (value != null)
                    _ = ViewItem(value).ConfigureAwait(true);
            }
        }

        private LoadingPopupPage pageLoading;
        private bool _layLogin;
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
        public bool LayLogin
        {
            get { return _layLogin; }
            set
            {
                SetProperty(ref _layLogin, value);
            }
        }

        private bool _layList;
        public bool LayList
        {
            get { return _layList; }
            set
            {
                SetProperty(ref _layList, value);
            }
        }

        private UserCA _userCA;
        public UserCA UserCA
        {
            get { return _userCA; }
            set
            {
                SetProperty(ref _userCA, value);
            }
        }

        private List<League> _leagues;
        public List<League> Leagues
        {
            get { return _leagues; }
            set
            {
                SetProperty(ref _leagues, value);
            }
        }
        #endregion
        public LeaguesPageViewModel()
        {
            Title = "Minhas Ligas";
            ViewItemCommand = new Command<object>(async (item) => await ViewItem(item));
            LayLogin = true;
            LayList = false;

            if (CrossConnectivity.Current.IsConnected && !Barrel.Current.IsExpired(key: UrlCartola._timeAuth))
            {
                UserCA = Barrel.Current.Get<UserCA>(key: UrlCartola._timeAuth);
                LayLogin = false;
                LayList = true;
                GetInitial();
            }
            else if (!CrossConnectivity.Current.IsConnected)
            {
                UserCA = App.Database.GetUsersCAAsync().Result.FirstOrDefault();
                LayLogin = false;
                LayList = true;
            }
            MessagingCenter.Subscribe<UserCA>(this, "Logado", message =>
            {
                LayLogin = false;
                LayList = true;
                GetInitial();
            });
        }

        #region Metodos
        public async void GetInitial()
        {
            try
            {
                //pageLoading = new LoadingPopupPage();
                //await App.Current.MainPage.Navigation.PushPopupAsync(pageLoading, true);
                if (IsBusy) return;
                IsBusy = true;
                ShowLoading(AppResources.LOADING);
                LayList = true;
                LayLogin = false;
                Leagues = await leaguesService.GetLeaguesUserCAAsync();

                AnalyticsEvents.AnalyticsHandle("LeaguesPageView", nameof(GetInitial));
            }
            catch (Exception ex)
            {
                AnalyticsEvents.AnalyticsHandle("LeaguesPageView", nameof(GetInitial), ex.StackTrace);

            }
            //pageLoading.HidePopup();
            HideLoading();
            IsBusy = false;
        }

        public async void Login()
        {
            var webAuth = new CookieWebView();

            webAuth.Source = UrlCartola._login2;

            webAuth.Navigated += async delegate
            {
                CookieCollection cookeis = webAuth.Cookies;
                try
                {

                    var GLBID = cookeis["GLBID"].Value;
                    if (GLBID != null)
                    {

                        string json = base.Api.Get(UrlCartola._timeAuth, GLBID).Result;
                        var timeLogado = JsonConvert.DeserializeObject<BaseTimeLoggedIn>(json); ;
                        var pes = new UserCA();
                        pes.glbstring = GLBID.ToString();
                        pes.id = timeLogado.time.time_id;
                        pes.NomeTime = timeLogado.time.nome;
                        pes.NomeCartoleiro = timeLogado.time.nome_cartola;
                        pes.url_escudo = timeLogado.time.url_escudo_png;
                        pes.slug = timeLogado.time.slug;

                        Barrel.Current.Add(
                            key: UrlCartola._timeAuth,
                            data: pes,
                            expireIn: TimeSpan.FromDays(300));

                        await App.Database.InsertUserCAAsync(pes);

                        Xamarin.Forms.DependencyService.Get<IMessage>().LongAlert("Login efetuado com sucesso! Click no botão logo a cima para atualizar a lista.");
                        //await DisplayAlert("Sucesso", "Login efetuado com sucesso! Click no botão logo a cima para atualizar a lista.", "OK");

                        await App.Current.MainPage.Navigation.PopAsync();

                    }
                    else
                    {
                        Xamarin.Forms.DependencyService.Get<IMessage>().LongAlert("Verifique seu usuário e senha.");
                        //DisplayAlert("Atenção", "Verifique seu usuário e senha", "OK");
                    }

                    AnalyticsEvents.AnalyticsHandle("LeaguesPageView", nameof(Login));
                }
                catch (Exception ex)
                {
                    AnalyticsEvents.AnalyticsHandle("LeaguesPageView", nameof(Login), ex.StackTrace);

                }

            };
        }
        #endregion

        #region Eventos
        private async Task ViewItem(object sender)
        {
            string slug = LeagueSelected.slug;
            LeagueSelected = null;
            await App.Current.MainPage.Navigation.PushAsync(new TeamsLeaguePage(slug));

            //var json = JsonConvert.SerializeObject(LeagueSelected);
            //LeagueSelected = null;
            //await Shell.Current.GoToAsync($"{Pages.TeamsLeaguePage}?paran={json}").ConfigureAwait(true);
        }
        #endregion
    }
}
