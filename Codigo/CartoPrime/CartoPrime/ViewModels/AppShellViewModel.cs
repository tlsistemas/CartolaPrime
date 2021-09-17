using CartoPrime.Helpers;
using CartoPrime.Interfaces;
using CartoPrime.Models;
using CartoPrime.Modules.Authentication.Views;
using CartoPrime.Modules.Base;
using CartoPrime.Modules.BonsBico.Views;
using CartoPrime.Modules.Classification.Views;
using CartoPrime.Modules.Home.Views;
using CartoPrime.Modules.Leagues.Views;
using CartoPrime.Modules.MyInformations.Views;
using CartoPrime.Modules.Round.Views;
using CartoPrime.Modules.Team.Views;
using CartoPrime.Services;
using CartoPrime.Views;
using MonkeyCache.FileStore;
using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CartoPrime.ViewModels
{
    public class AppShellViewModel : BaseViewModel<AppShellViewModel>
    {
        readonly INotificationRegistrationService _notificationRegistrationService;
        Dictionary<string, Type> routes = new Dictionary<string, Type>();

        #region Properties
        public bool _enableExit;
        public bool EnableExit
        {
            get { return _enableExit; }
            set { SetProperty(ref _enableExit, value); }
        }

        private string _escudoTeam;
        public string EscudoTeam
        {
            get { return _escudoTeam; }
            set { SetProperty(ref _escudoTeam, value); }
        }

        private string _nameTeam;
        public string NameTeam
        {
            get { return _nameTeam; }
            set { SetProperty(ref _nameTeam, value); }
        }

        private string _statusMarket;
        public string StatusMarket
        {
            get { return _statusMarket; }
            set { SetProperty(ref _statusMarket, value); }
        }
        #endregion

        #region Commands
        public ICommand NavigateCommand { get; }
        public ICommand ExitCommand { get; set; }

        #endregion

        public AppShellViewModel()
        {
            RegisterRoutes();
            SetTeamLog().ConfigureAwait(true);

            NavigateCommand = new Command(async (o) => await Navigate(o));
            ExitCommand = new Command(async () => await Exit());

            _notificationRegistrationService =
                    ServiceContainer.Resolve<INotificationRegistrationService>();
            _notificationRegistrationService.RegisterDeviceAsync().ConfigureAwait(true);

            MessagingCenter.Subscribe<UserCA>(this, "UpdateMenu", message =>
            {
                try
                {
                    NameTeam = Barrel.Current.Get<string>(key: "NomeTime") != null ? Barrel.Current.Get<string>(key: "NomeTime") : "Cartola Prime FC";
                    EscudoTeam = Barrel.Current.Get<string>(key: "UrlEscudo");
                    StatusMarket = Barrel.Current.Get<string>(key: "StatusMarket");
                    if (EscudoTeam == null)
                    {
                        NameTeam = "Bem Vindo";
                        EscudoTeam = "loginca.png";
                        StatusMarket = Barrel.Current.Get<string>(key: "StatusMarket");
                    }


                }
                catch (Exception ex)
                {
                    NameTeam = "Bem Vindo";
                    EscudoTeam = "loginca.png";
                    StatusMarket = "";
                }

                //SetTeamLog().ConfigureAwait(true);
            });
            try
            {
                NameTeam = Barrel.Current.Get<string>(key: "NomeTime") != null ? Barrel.Current.Get<string>(key: "NomeTime") : "Cartola Prime FC";
                EscudoTeam = Barrel.Current.Get<string>(key: "UrlEscudo");
                StatusMarket = Barrel.Current.Get<string>(key: "StatusMarket");


            }
            catch (Exception ex)
            {
                NameTeam = "Bem Vindo";
                EscudoTeam = "loginca.png";
                StatusMarket = "";
            }
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(Pages.Login).ConfigureAwait(true);
        }

        #region Metodos
        private async Task SetTeamLog()
        {
            try
            {
                var team = App.Database.GetUsersCAAsync().Result.FirstOrDefault();
                if (team != null)
                {
                    EscudoTeam = team.url_escudo;
                    NameTeam = "Bem Vindo " + team.NomeTime;

                    Barrel.Current.Add(
                        key: "NomeTime",
                        data: team.NomeTime,
                        expireIn: TimeSpan.FromMinutes(300));

                    Barrel.Current.Add(
                        key: "UrlEscudo",
                        data: team.url_escudo,
                        expireIn: TimeSpan.FromMinutes(300));
                }
                else
                {
                    EscudoTeam = "loginca.png";
                    NameTeam = "Cartolas Prime FC";
                    Barrel.Current.Add(
                        key: "UrlEscudo",
                        data: "loginca.png",
                        expireIn: TimeSpan.FromMinutes(300));
                }

                var mercado = Barrel.Current.Get<Market>(key: UrlCartola._mercadoStatus);
                if (mercado == null || mercado.status_mercado == 4)
                {
                    var status = "Mercado Em Manutenção. ";
                    var fechamnto = "Atenção, algumas informações podem não aparecer.";
                    StatusMarket = status + fechamnto;
                }
                else if (mercado.status_mercado == 2)
                {
                    var status = "Mercado Fechado em ";
                    var fechamnto = mercado.fechamento.dia + "/" + mercado.fechamento.mes + "/" + mercado.fechamento.ano + " " + mercado.fechamento.hora + ":" + mercado.fechamento.minuto; ;
                    StatusMarket = status + Convert.ToDateTime(fechamnto);
                }
                else
                {
                    var status = "Mercado Aberto até, ";
                    var fechamnto = mercado.fechamento.dia + "/" + mercado.fechamento.mes + "/" + mercado.fechamento.ano + " " + mercado.fechamento.hora + ":" + mercado.fechamento.minuto;
                    StatusMarket = status + Convert.ToDateTime(fechamnto);

                }
                Barrel.Current.Add(
                    key: "StatusMarket",
                    data: StatusMarket,
                    expireIn: TimeSpan.FromMinutes(300));
            }
            catch (Exception ex)
            {

            }
        }
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
        private async Task PushPage(Page page)
        {
            await Shell.Current.Navigation.PushAsync(page);
            Shell.Current.FlyoutIsPresented = false;
        }
        private async Task Navigate(object route)
        {
            Shell.Current.FlyoutIsPresented = false;
            await base.Navigate(route.ToString()).ConfigureAwait(true);
        }
        void RegisterRoutes()
        {
            routes.Add("RoundPlayersPage", typeof(RoundPlayersPage));
            routes.Add("RoundGamesPage", typeof(RoundGamesPage));
            routes.Add("ClassificationBRAPage", typeof(ClassificationBRAPage));
            routes.Add("NewsPage", typeof(NewsPage));
            routes.Add("ContactsPage", typeof(ContactsPage));
            routes.Add("AdsPage", typeof(AdsPage));
            routes.Add("TeamsPage", typeof(TeamsPage));
            routes.Add("LeaguesPage", typeof(LeaguesPage));
            routes.Add("ScoredPage", typeof(ScoredPage));
            routes.Add("MostClimbedPage", typeof(MostClimbedPage));
            routes.Add("PartnerView", typeof(PartnerView));
            routes.Add("InsertTeamsPage", typeof(InsertTeamsPage));
            routes.Add("AthletesPage", typeof(AthletesPage));
            routes.Add("TeamsLeaguePage", typeof(TeamsLeaguePage));
            routes.Add("ClimbView", typeof(ClimbView));
            routes.Add("ClimbByAthleteView", typeof(ClimbByAthleteView));
            routes.Add("LoginPage", typeof(LoginPage));
            routes.Add("LigaBonsDeBicoView", typeof(LigaBonsDeBicoView));
            routes.Add("LigaBonsDeBicoViewB", typeof(LigaBonsDeBicoViewB));
            routes.Add("ComparePlayersView", typeof(ComparePlayersView));


            foreach (var item in routes)
            {
                Routing.RegisterRoute(item.Key, item.Value);
            }
        }
        private async Task Exit()
        {
            try
            {
                if (!EscudoTeam.Equals("loginca.png"))
                {
                    Shell.Current.FlyoutIsPresented = false;
                    ShowLoading(Resources.AppResources.LOADING);
                    var pes = Barrel.Current.Get<UserCA>(key: UrlCartola._timeAuth);
                    await App.Database.DeleteUserCAAsync(pes);
                    Barrel.Current.Empty(key: UrlCartola._timeAuth);
                    Barrel.Current.Empty(key: "NomeTime");
                    Barrel.Current.Empty(key: "UrlEscudo");

                    //MessagingCenter.Send(new UserCA(), "Logado");
                    MessagingCenter.Send(new UserCA(), "UpdateMenu");

                    //NameTeam = "Bem Vindo";
                    //EscudoTeam = "loginca.png";
                    //StatusMarket = Barrel.Current.Get<string>(key: "StatusMarket");
                    AnalyticsEvents.AnalyticsHandle("AppShellView", nameof(Exit));

                    HideLoading();
                }
            }
            catch (Exception ex)
            {
                HideLoading(); 
                AnalyticsEvents.AnalyticsHandle("AppShellView", nameof(Exit), ex.StackTrace);

                CrossToastPopUp.Current.ShowToastMessage(ex.Message);
            }
        }
        #endregion
    }
}
