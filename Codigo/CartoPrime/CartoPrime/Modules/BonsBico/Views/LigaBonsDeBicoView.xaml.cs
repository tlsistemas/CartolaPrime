using CartoPrime.Interfaces;
using CartoPrime.Models.BonsBico;
using CartoPrime.Modules.BonsBico.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CartoPrime.Modules.BonsBico.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LigaBonsDeBicoView : ContentPage
    {
        LigaBonsDeBicoViewModel viewmodel;


        public LigaBonsDeBicoView()
        {
            InitializeComponent();

            BindingContext = new LigaBonsDeBicoViewModel();
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
            viewmodel = (LigaBonsDeBicoViewModel)BindingContext;
            _ = viewmodel.InitAsync().ConfigureAwait(true);
            base.OnAppearing();

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var time = (((TappedEventArgs)e).Parameter) as BonsDeBico;        
            _ = viewmodel.ViewPalyers(time);
        }
    }
}