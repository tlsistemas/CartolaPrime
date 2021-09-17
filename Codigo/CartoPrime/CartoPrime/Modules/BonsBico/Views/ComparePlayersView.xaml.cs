using CartoPrime.Interfaces;
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
    public partial class ComparePlayersView : ContentPage
    {
        ComparePlayersViewModel viewmodel = new ComparePlayersViewModel();
        public ComparePlayersView()
        {
            InitializeComponent(); 
            BindingContext = viewmodel = new ComparePlayersViewModel();
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
            viewmodel = (ComparePlayersViewModel)BindingContext;
            _ = viewmodel.InitAsync().ConfigureAwait(true);
            base.OnAppearing();

        }
    }
}