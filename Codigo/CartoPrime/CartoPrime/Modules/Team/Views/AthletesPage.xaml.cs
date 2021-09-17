using CartoPrime.Interfaces;
using CartoPrime.Modules.Team.ViewModels;
using CartoPrime.Views;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CartoPrime.Modules.Team.Views
{
    public partial class AthletesPage : ContentPage
    {

        public AthletesPage(int idTime)
        {
            InitializeComponent();
            BindingContext = new AthletesPageViewModel(idTime);
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

        private void header_BackButtonPressedEvent(Enums.NavigationBackButtonAction showBackButtonActionRequested)
        {
            base.OnBackButtonPressed();
        }
    }
}
