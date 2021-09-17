using CartoPrime.Helpers;
using CartoPrime.Interfaces;
using CartoPrime.ViewModels;
using Xamarin.Forms;

namespace CartoPrime.Modules.Round.Views
{
    public partial class MostClimbedPage : ContentPage
    {
        public MostClimbedPage()
        {
            InitializeComponent();
            BindingContext = new MostClimbedPageViewModel();

            Title = "Mais Escalados";
            try
            {
                //AnalyticsService.RastrearEvento(EventAnalytics.Category_NavigationPage, "MaisEscaladosPage");
                //LayAdView.AdUnitId = UrlCartola.BANNER_ID_CARTOLA_PRIME;

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
