using CartoPrime.Helpers;
using CartoPrime.Http.Interfaces;
using CartoPrime.Models;
using CartoPrime.Modules.Authentication.ViewModels;
using CartoPrime.Modules.Base;
using CartoPrime.Modules.Leagues.ViewModels;
using CartoPrime.Modules.Leagues.Views;
using CartoPrime.Resources;
using CartoPrime.Services;
using CartoPrime.Views;
using MonkeyCache.FileStore;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CartoPrime.Modules.Authentication.Views
{
    public partial class LoginPage : ContentPage
    {
        LeaguesPage view;
        IApiService api;
        CookieWebView _renderer = new CookieWebView();
        //CookieCollection cookeis = new CookieCollection();
        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            api = DependencyService.Get<IApiService>();

            //AnalyticsService.RastrearEvento(EventAnalytics.Category_NavigationPage, "LoginPage");

            var webAuth = new CookieWebView();

            webAuth.Source = UrlCartola._login2;

            Content = webAuth;

            webAuth.Navigated += async delegate
            {
                CookieCollection cookeis = webAuth.Cookies;


                try
                {

                    var GLBID = cookeis["GLBID"].Value;
                    if (GLBID != null)
                    {

                        string json = api.Get(UrlCartola._timeAuth, GLBID).Result;
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


                        if (pes != null)
                        {
                            Barrel.Current.Add(
                                key: "NomeTime",
                                data: pes.NomeTime,
                                expireIn: TimeSpan.FromMinutes(300));

                            Barrel.Current.Add(
                                key: "UrlEscudo",
                                data: pes.url_escudo,
                                expireIn: TimeSpan.FromMinutes(300));
                        }
                        else
                        {
                            Barrel.Current.Add(
                                key: "UrlEscudo",
                                data: "loginca.png",
                                expireIn: TimeSpan.FromMinutes(300));
                        }

                        var mercado = Barrel.Current.Get<Market>(key: UrlCartola._mercadoStatus);
                        string StatusMercado = "";
                        if (mercado == null || mercado.status_mercado == 4)
                        {
                            var status = "Mercado Em Manutenção. ";
                            var fechamnto = "Atenção, algumas informações podem não aparecer.";
                            StatusMercado = status + fechamnto;
                        }
                        else if (mercado.status_mercado == 2)
                        {
                            var status = "Mercado Fechado em ";
                            var fechamnto = mercado.fechamento.ano + "/" + mercado.fechamento.mes + "/" + mercado.fechamento.dia + " " + mercado.fechamento.hora + ":" + mercado.fechamento.minuto;
                            StatusMercado = status + Convert.ToDateTime(fechamnto).ToString("dd/MM/yyyy HH:mm");
                        }
                        else
                        {
                            var status = "Mercado Aberto até, ";
                            var fechamnto = mercado.fechamento.ano + "/" + mercado.fechamento.mes + "/" + mercado.fechamento.dia + " " + mercado.fechamento.hora + ":" + mercado.fechamento.minuto;
                            StatusMercado = status + Convert.ToDateTime(fechamnto).ToString("dd/MM/yyyy HH:mm");

                        }
                        Barrel.Current.Add(
                            key: "StatusMarket",
                            data: StatusMercado,
                            expireIn: TimeSpan.FromMinutes(300));

                        AnalyticsEvents.AnalyticsHandle("LoginPage", "LoginPage", JsonConvert.SerializeObject(pes));

                        await DisplayAlert("Sucesso", "Login efetuado com sucesso!", "OK");

                        MessagingCenter.Send(new UserCA(), "Logado");
                        MessagingCenter.Send(new UserCA(), "UpdateMenu");
                        await App.Current.MainPage.Navigation.PopToRootAsync();

                    }
                    else
                    {
                        DisplayAlert("Atenção", "Verifique seu usuário e senha", "OK");
                    }

                }
                catch (Exception ex)
                {
                    AnalyticsEvents.AnalyticsHandle("LoginPage", "LoginPage", ex.StackTrace);

                }

            };
        }
    }
}
