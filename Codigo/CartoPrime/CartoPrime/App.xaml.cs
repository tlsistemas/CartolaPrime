using System;
using Xamarin.Forms;
using CartoPrime.Http.Interfaces;
using CartoPrime.Http;
using CartoPrime.Interfaces;
using CartoPrime.Data;
using System.IO;
using CartoPrime.Models;
using MonkeyCache.FileStore;
using CartoPrime.Helpers;
using CartoPrime.Services;

namespace CartoPrime
{
    public partial class App : Application
    {
        public static IFirebaseAnalytics FirebaseAnalyticsService;

        static CADatabase database;
        static Market MarketCurrent;
        public static CADatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new CADatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "db_ca.db3"));
                }
                return database;
            }
        }
        
        public App()
        {
            InitializeComponent();
            RegisterServices();
            App.Current.Properties["click"] = 1;
            var mostrarProp = int.Parse(App.Current.Properties["click"].ToString());

            mostrarProp += 1;

            if (mostrarProp == 6)
            {
                mostrarProp = 0;
                DependencyService.Get<IAdmobInterstitial>().Show();
            }

            _ = RegisterDateInitialAsync();

            App.Current.Properties["click"] = 1;

            FirebaseAnalyticsService = DependencyService.Get<IFirebaseAnalytics>();

            MainPage = new AppShell();
        }

        #region Eventos

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
        #endregion

        #region Metodos
        void RegisterServices()
        {
            DependencyService.Register<IApiService, ApiService>();
            DependencyService.Register<ITeamService, TeamService>();
            DependencyService.Register<IAthleteService, AthleteService>();
            DependencyService.Register<IClubService, ClubService>();
            DependencyService.Register<IMarketService, MarketService>();
            DependencyService.Register<IPositionsService, PositionsService>();            
            DependencyService.Register<IClassificationBRAService, ClassificationBRAService>();
            DependencyService.Register<ILeaguesService, LeaguesService>();
            DependencyService.Register<IPartialService, PartialService>();
            DependencyService.Register<INewsService, NewsService>();
            DependencyService.Register<IBonsDeBicoService, BonsDeBicoService>();
            DependencyService.Register<IAdmobInterstitial>(); 
            DependencyService.Register<IFirebaseAnalytics>();
        }


        async System.Threading.Tasks.Task RegisterDateInitialAsync()
        {
            var mark = await DependencyService.Get<IMarketService>().InformationInitial();
            if (mark != null)
            {
                Barrel.Current.Add(
                    key: UrlCartola._mercadoStatus,
                    data: mark,
                    expireIn: TimeSpan.FromHours(1));
                await App.Database.InsertMarketAsync(mark);
            }

        }
        #endregion
    }
}
