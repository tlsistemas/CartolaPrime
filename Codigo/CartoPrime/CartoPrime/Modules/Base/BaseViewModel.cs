using CartoPrime.Extensions;
using CartoPrime.Helpers;
using CartoPrime.Http.Interfaces;
using CartoPrime.Interfaces;
using CartoPrime.Modules.Home.Views;
using CartoPrime.Modules.Popups.Views;
using CartoPrime.Services;
using CartoPrime.Views;
using Plugin.Toast;
using Rg.Plugins.Popup.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CartoPrime.Modules.Base
{
    public class BaseViewModel<T> : ObservableObject
    {
        #region Interfaces
        protected IApiService Api { get; set; }
        protected IMarketService marketService;
        protected IClubService _clubService;
        protected IPositionsService _positionsService;
        protected IAthleteService athleteService;
        protected IClassificationBRAService ClassificationBRAService;
        protected ILeaguesService leaguesService;
        protected IPartialService partialService;
        protected INewsService NewsService;
        protected ITeamService teamService;
        protected IBonsDeBicoService _bonsDeBicoService;
        #endregion

        #region Properties
        private bool isLoading;
        protected LoadingView _loadingView;

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }
        public ICommand WhatsappCommand { get; set; }

        #endregion

        #region Construtor
        public BaseViewModel()
        {
            Api = Xamarin.Forms.DependencyService.Get<IApiService>();
            marketService = Xamarin.Forms.DependencyService.Get<IMarketService>();
            _clubService = Xamarin.Forms.DependencyService.Get<IClubService>();
            _positionsService = Xamarin.Forms.DependencyService.Get<IPositionsService>();
            athleteService = Xamarin.Forms.DependencyService.Get<IAthleteService>();
            ClassificationBRAService = Xamarin.Forms.DependencyService.Get<IClassificationBRAService>();
            leaguesService = Xamarin.Forms.DependencyService.Get<ILeaguesService>();
            partialService = Xamarin.Forms.DependencyService.Get<IPartialService>();
            NewsService = Xamarin.Forms.DependencyService.Get<INewsService>();
            teamService = Xamarin.Forms.DependencyService.Get<ITeamService>();
            _bonsDeBicoService = Xamarin.Forms.DependencyService.Get<IBonsDeBicoService>();

            WhatsappCommand = new Command(async () =>
            {
                await Whatsapp("").ConfigureAwait(true);
            });
        }

        public virtual void Init(params object[] args)
        {

        }

        public virtual Task InitAsync(params object[] args)
        {
            return Task.FromResult(false);
        }
        #endregion

        #region Metodos
        protected async Task Whatsapp(string number)
        {
            try
            {
                var msgcompleta = $"whatsapp://send/?phone={number}";
                var uri = new Uri(msgcompleta);
                await Browser.OpenAsync(uri).ConfigureAwait(true);
            }
            catch (Exception ex)
            {
                //Solicita instalação do WhatsApp
                if (ex.Message == "No Activity found to handle Intent { act=android.intent.action.VIEW dat=whatsapp://send/?phone=5511963104368 (has extras) }")
                {
                    CrossToastPopUp.Current.ShowToastMessage("Instale o aplicativo Whatsapp");
                }
                else
                {
                    //ErrorHelper.ErrorHandle(ex, "BaseViewModel", "OctadeskMethod");
                }
            }
            finally
            {
                //AnalyticsAppCenter.AnalyticsHandle("BaseViewModel", "OctadeskMethod");
            }
        }
        public virtual void Destroy()
        {

        }

        #endregion

        #region Navigation
        public async Task Navigate(object route)
        {
            //Shell.Current.FlyoutIsPresented = false;
            switch (route.ToString())
            {
                case "ClassifBonsDeBicoViewA":
                    await App.Current.MainPage.Navigation.PushAsync(new WebViewUrlPage(UrlService._bb_classifSerieA, "Bons de Bico Serie A", "ClassifBonsDeBicoViewA")).ConfigureAwait(true);
                    return;
                case "ClassifBonsDeBicoViewB":
                    await App.Current.MainPage.Navigation.PushAsync(new WebViewUrlPage(UrlService._bb_classifSerieB, "Bons de Bico Serie B", "ClassifBonsDeBicoViewB")).ConfigureAwait(true);
                    return;
                case "BonsDeBicoView":
                    //await NavigationService.NavigateToAsync<RoundGamesPageViewModel>();
                    await Shell.Current.GoToAsync(Pages.LigaBonsDeBicoView).ConfigureAwait(true);
                    //await App.Current.MainPage.Navigation.PushAsync(new RoundGamesPage()).ConfigureAwait(true);
                    return;
                case "BonsDeBicoViewB":
                    //await NavigationService.NavigateToAsync<RoundGamesPageViewModel>();
                    await Shell.Current.GoToAsync(Pages.LigaBonsDeBicoViewB).ConfigureAwait(true);
                    //await App.Current.MainPage.Navigation.PushAsync(new RoundGamesPage()).ConfigureAwait(true);
                    return;
                case "LoginView":
                    //await NavigationService.NavigateToAsync<RoundGamesPageViewModel>();
                    await Shell.Current.GoToAsync(Pages.Login).ConfigureAwait(true);
                    //await App.Current.MainPage.Navigation.PushAsync(new RoundGamesPage()).ConfigureAwait(true);
                    return;
                case "RoundGamesPage":
                    //await NavigationService.NavigateToAsync<RoundGamesPageViewModel>();
                    await Shell.Current.GoToAsync(Pages.RoundGamesPage).ConfigureAwait(true);
                    //await App.Current.MainPage.Navigation.PushAsync(new RoundGamesPage()).ConfigureAwait(true);
                    return;
                case "ClassificationBRAPage":
                    
                    //await NavigationService.NavigateToAsync<ClassificationBRAPageViewModel>();
                    await Shell.Current.GoToAsync(Pages.ClassificationBRAPage).ConfigureAwait(true);
                    //await App.Current.MainPage.Navigation.PushAsync(new ClassificationBRAPage()).ConfigureAwait(true);
                    return;
                case "ClassifBraBView":
                    //await Browser.OpenAsync(UrlCartola._classBraB, BrowserLaunchMode.SystemPreferred);
                    //await App.Current.MainPage.Navigation.PushAsync(new WebViewUrlPage(UrlCartola._reiDoPitaco, "")).ConfigureAwait(true);
                    return;
                case "NewsPage":
                    //await NavigationService.NavigateToAsync<NewsPageViewModel>();
                    await Shell.Current.GoToAsync(Pages.NewsPage).ConfigureAwait(true);
                    //await App.Current.MainPage.Navigation.PushAsync(new NewsPage()).ConfigureAwait(true);
                    return;
                case "ContactsPage":
                    //await NavigationService.NavigateToAsync<ContactsPageViewModel>();
                    await Shell.Current.GoToAsync(Pages.ContactsPage).ConfigureAwait(true);
                    //await App.Current.MainPage.Navigation.PushAsync(new ContactsPage()).ConfigureAwait(true);
                    return;
                case "ScalePage":
                    AnalyticsEvents.AnalyticsHandle("BaseView", "ScalePage");
                    await Browser.OpenAsync(UrlCartola._ca, BrowserLaunchMode.SystemPreferred);
                    //await App.Current.MainPage.Navigation.PushAsync(new WebViewUrlPage(UrlCartola._ca, "Escalar seu time")).ConfigureAwait(true);
                    //await Shell.Current.GoToAsync(Pages.ClimbView).ConfigureAwait(true);
                    return;
                case "AdsPage":
                    //await NavigationService.NavigateToAsync<AdsPageViewModel>();
                    //await App.Current.MainPage.Navigation.PushAsync(new HomeCAView()).ConfigureAwait(true);
                    await Shell.Current.GoToAsync(Pages.AdsPage).ConfigureAwait(true);
                    return;
                case "TeamsPage":
                    //await NavigationService.NavigateToAsync<TeamsPageViewModel>();
                    //await App.Current.MainPage.Navigation.PushAsync(new TeamsPage()).ConfigureAwait(true);
                    await Shell.Current.GoToAsync(Pages.TeamsPage).ConfigureAwait(true);
                    return;
                case "LeaguesPage":
                    //await NavigationService.NavigateToAsync<LeaguesPageViewModel>();
                    //await App.Current.MainPage.Navigation.PushAsync(new LeaguesPage()).ConfigureAwait(true);
                    await Shell.Current.GoToAsync(Pages.LeaguesPage).ConfigureAwait(true);
                    return;
                case "ScoredPage":
                    //await NavigationService.NavigateToAsync<ScoredViewModel>();
                    //await App.Current.MainPage.Navigation.PushAsync(new ScoredPage()).ConfigureAwait(true);
                    await Shell.Current.GoToAsync(Pages.ScoredPage).ConfigureAwait(true);
                    return;
                case "InsertTeamsPage":
                    //await NavigationService.NavigateToAsync<InsertTeamsPageViewModel>();
                    //await App.Current.MainPage.Navigation.PushAsync(new ScoredPage()).ConfigureAwait(true);
                    await Shell.Current.GoToAsync(Pages.InsertTeamsPage).ConfigureAwait(true);
                    return;
                case "MostClimbedPage":
                    //await NavigationService.NavigateToAsync<MostClimbedPageViewModel>();
                    //await App.Current.MainPage.Navigation.PushAsync(new MostClimbedPage()).ConfigureAwait(true);
                    await Shell.Current.GoToAsync(Pages.MostClimbedPage).ConfigureAwait(true);
                    return;
                case "MostClimbedPositionPage":
                    //await NavigationService.NavigateToAsync<MostClimbedPageViewModel>();
                    //await App.Current.MainPage.Navigation.PushAsync(new MostClimbedPage()).ConfigureAwait(true);
                    await App.Current.MainPage.Navigation.PushAsync(new WebViewUrlPage(UrlCartola._EscaladosPorPosicao, "+Escalados Posiçoes", "MostClimbedPositionPage")).ConfigureAwait(true);
                    return;
                case "PartnerView":
                    //await PopupNavigation.Instance.PushAsync(new PartnerPopUpView(), false).ConfigureAwait(true);
                    //await Shell.Current.GoToAsync(Pages.PartnerView).ConfigureAwait(true);
                    AnalyticsEvents.AnalyticsHandle("BaseView", "PartnerView");
                    await Browser.OpenAsync(UrlCartola._CartoPrimePlanoSocioAnual, BrowserLaunchMode.SystemPreferred);
                    return;
                case "CanalCartoPrimeView":
                    AnalyticsEvents.AnalyticsHandle("BaseView", "CanalCartoPrimeView");
                    await Browser.OpenAsync(UrlCartola._canalCartoPrime, BrowserLaunchMode.SystemPreferred);
                    //await App.Current.MainPage.Navigation.PushAsync(new WebViewUrlPage(UrlCartola._canalCartoPrime, "")).ConfigureAwait(true);
                    return;
                case "NerecoView":
                    //await PopupNavigation.Instance.PushAsync(new NerecoPopUpView(), false).ConfigureAwait(true);
                    AnalyticsEvents.AnalyticsHandle("BaseView", "NerecoView");
                    await Browser.OpenAsync(UrlCartola._nerecoYoutube, BrowserLaunchMode.SystemPreferred);
                    //await App.Current.MainPage.Navigation.PushAsync(new WebViewUrlPage(UrlCartola._nereco, "")).ConfigureAwait(true);
                    return;
                case "ReiDoPitacoView":
                    AnalyticsEvents.AnalyticsHandle("BaseView", "ReiDoPitacoView");
                    await Browser.OpenAsync(UrlCartola._reiDoPitaco, BrowserLaunchMode.SystemPreferred);
                    //await App.Current.MainPage.Navigation.PushAsync(new WebViewUrlPage(UrlCartola._reiDoPitaco, "")).ConfigureAwait(true);
                    return;
                case "InstaCartoPrimeView":
                    AnalyticsEvents.AnalyticsHandle("BaseView", "InstaCartoPrimeView");
                    await Browser.OpenAsync(UrlCartola._instagranCartoPrime, BrowserLaunchMode.SystemPreferred);
                    //await App.Current.MainPage.Navigation.PushAsync(new WebViewUrlPage(UrlCartola._intagranCartoPrime, "")).ConfigureAwait(true);
                    return;
                case "DivulgueAquiView":
                    try
                    {
                        AnalyticsEvents.AnalyticsHandle("BaseView", "DivulgueAquiView");
                        await Whatsapp("+5562998551924");
                        // Chat.Open("+5563984418785", "Olá, gotaria de saber sobre a divulgação no App de vocês.");
                    }
                    catch (Exception ex)
                    {
                        CrossToastPopUp.Current.ShowToastMessage("Instale o aplicativo Whatsapp");
                    }
                    return;
                case "DicasExView":
                    //await Browser.OpenAsync(UrlCartola._CartoPrimePlanoSocioAnual, BrowserLaunchMode.SystemPreferred);
                    //await App.Current.MainPage.Navigation.PushAsync(new WebViewUrlPage(UrlCartola._intagranCartoPrime, "")).ConfigureAwait(true);
                    return;
                case "PlanoAnualView":
                    //await Browser.OpenAsync(UrlCartola._CartoPrimePlanoSocioAnual, BrowserLaunchMode.SystemPreferred);
                    //await App.Current.MainPage.Navigation.PushAsync(new WebViewUrlPage(UrlCartola._intagranCartoPrime, "")).ConfigureAwait(true);
                    return;
                case "Plano1TurnoView":
                    //await Browser.OpenAsync(UrlCartola._CartoPrimePlanoSocio1Turno, BrowserLaunchMode.SystemPreferred);
                    //await App.Current.MainPage.Navigation.PushAsync(new WebViewUrlPage(UrlCartola._intagranCartoPrime, "")).ConfigureAwait(true);
                    return;
                case "Plano2TurnoView":
                    //await Browser.OpenAsync(UrlCartola._CartoPrimePlanoSocio2Turno, BrowserLaunchMode.SystemPreferred);
                    //await App.Current.MainPage.Navigation.PushAsync(new WebViewUrlPage(UrlCartola._intagranCartoPrime, "")).ConfigureAwait(true);
                    return;
                default:
                    break;
            }
            ShellNavigationState state = Shell.Current.CurrentState;
            await Shell.Current.GoToAsync($"{state.Location}/{route.ToString()}");
        }
        #endregion

        #region Loading
        protected void ShowLoading(string LoadMessage)
        {
            if (!isLoading)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    isLoading = true;
                    if (PopupNavigation.Instance.PopupStack != null && PopupNavigation.Instance.PopupStack.Count == 0)
                    {
                        var _loadingView = new LoadingView(LoadMessage);
                        await PopupNavigation.Instance.PushAsync(_loadingView, false).ConfigureAwait(true);
                    }
                });
            }
        }

        protected async void HideLoading()
        {
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                var view = PopupNavigation.Instance.PopupStack?.LastOrDefault();

                if (view != null)
                    await PopupNavigation.Instance.RemovePageAsync(view).ConfigureAwait(true);
            }).ConfigureAwait(true);

            isLoading = false;
        }
        #endregion

        #region Popup

        protected void PushPopupSimpleView(Page page)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Shell.Current.Navigation.PushModalAsync(page).ConfigureAwait(true);
                //await _navigationService. .NavigatePopUpToAsync<Tview>().ConfigureAwait(true);
            });
        }

        protected async Task<bool> PushPopupMessageQuestionAlert(string message, bool simOunao)
        {
            string titulo, opcao1, opcao2;
            titulo = "Atenção";
            if (simOunao)
            {
                opcao1 = "Sim";
                opcao2 = "Não";
            }
            else
            {
                opcao1 = "OK";
                opcao2 = "Cancelar";
            }
            var displayAlert = DependencyService.Get<ICustomDisplayAlert>();
            var result = await displayAlert.Show(titulo, message, opcao1, opcao2).ConfigureAwait(true);
            return result;
        }

        protected void PopupView()
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                var view = PopupNavigation.Instance.PopupStack.LastOrDefault();
                await PopupNavigation.Instance.RemovePageAsync(view).ConfigureAwait(true);
            });
        }

        protected void PushPopupGenericMessageAlert(
            string titulo,
            string mensagem,
            bool atencao,
            bool cancelarBtn = false,
            string icone = null,
            ICommand continuarCommand = null,
            ICommand cancelarCommand = null)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                var view = new GenericMsgAlertView(titulo, mensagem, atencao, cancelarBtn, icone, continuarCommand, cancelarCommand);
                await PopupNavigation.Instance.PushAsync(view, false).ConfigureAwait(true);
            });

        }


        #endregion

        #region GoToHome

        public async void GoToHome()
        {
            try
            {
                await Shell.Current.Navigation.PopToRootAsync();
                //await NavigationService.GoToRootAsync().ConfigureAwait(true);
                //await NavigationService.PopAsync().ConfigureAwait(true);
                //await _navigationService.NavigateToAsync<HomeOLDViewModel>().ConfigureAwait(true);
                //await NavigationService.GoToRootAsync<HomeViewModel>().ConfigureAwait(true);
            }
            catch (Exception ex)
            {
                //ErrorHelper.ErrorHandle(ex, "BaseViewModel", "GoToHome");
            }
        }

        //public async Task GoToHomeAsync()
        //{
        //    if (IsBusy)
        //        return;
        //    AnalyticsEvents.AnalyticsHandle("ListaPedidosViewModel", "HomeViewChamada");
        //    IsBusy = true;
        //    var valid = await NavigationService.ExistViewInStackAsync("Home");
        //    if (valid)
        //    {
        //        await NavigationService.PopAsync();
        //    }
        //    else
        //    {
        //        await NavigationService.GoToRootAsync<HomeViewModel>().ConfigureAwait(true);// NavigateToAsync<HomeViewModel>().ConfigureAwait(true);
        //    }
        //}

        #endregion GoToHome
    }
}
