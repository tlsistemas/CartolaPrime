using CartoPrime.Helpers;
using CartoPrime.Interfaces;
using CartoPrime.Modules.Round.ViewModels;
using Xamarin.Forms;

namespace CartoPrime.Modules.Round.Views
{
    public partial class RoundPlayersPage : ContentPage
    {
        public RoundPlayersPage()
        {
            InitializeComponent();
            Title = "Jogadores Confirmados";
            txtDesc.Text = "C$ - Valor | VR - Variação | ME - Média | JO - Jogos";

            try
            {
                //AnalyticsService.RastrearEvento(EventAnalytics.Category_NavigationPage, "MaisEscaladosPage");
                //LayAdView.AdUnitId = UrlCartola.BANNER_ID_CARTOLA_PRIME;
                BindingContext = new RoundPlayersPageViewModel();
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
