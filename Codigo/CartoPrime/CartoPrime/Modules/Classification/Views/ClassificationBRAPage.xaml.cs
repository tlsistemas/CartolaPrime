using CartoPrime.Interfaces;
using CartoPrime.Modules.Classification.ViewModels;
using Xamarin.Forms;

namespace CartoPrime.Modules.Classification.Views
{
    public partial class ClassificationBRAPage : ContentPage
    {
        ClassificationBRAPageViewModel viewmodel;
        public ClassificationBRAPage()
        {
            InitializeComponent();
            BindingContext = new ClassificationBRAPageViewModel();
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

        protected async override void OnAppearing()
        {
            viewmodel = (ClassificationBRAPageViewModel)BindingContext;
            _ = viewmodel.InitAsync().ConfigureAwait(true);
            base.OnAppearing();

        }
    }
}
