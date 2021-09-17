using CartoPrime.Helpers;
using CartoPrime.Interfaces;
using CartoPrime.Modules.Round.ViewModels;
using Xamarin.Forms;

namespace CartoPrime.Modules.Round.Views
{
    public partial class RoundGamesPage : ContentPage
    {
        public RoundGamesPage()
        {
            InitializeComponent();
            try
            {
                BindingContext = new RoundGamesPageViewModel();
                //AnalyticsService.RastrearEvento(EventAnalytics.Category_NavigationPage, "MaisEscaladosPage");
                //LayAdView.AdUnitId = UrlCartola.BANNER_ID_CARTOLA_PRIME;
                NavigationPage.SetHasNavigationBar(this, true);
                NavigationPage.SetIconColor(this, (Color)App.Current.Resources["BrancoFumaca"]);

                var mostrarProp = int.Parse(App.Current.Properties["click"].ToString());
                mostrarProp += 1;

                if (mostrarProp == 5)
                {
                    mostrarProp = 0;
                    DependencyService.Get<IAdmobInterstitial>().Show();
                }

                App.Current.Properties["click"] = mostrarProp;
            }
            catch (System.Exception)
            {
            }
        }
    }
}
