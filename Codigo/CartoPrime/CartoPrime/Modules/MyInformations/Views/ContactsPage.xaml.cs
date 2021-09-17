using CartoPrime.Interfaces;
using System;
using Xamarin.Forms;

namespace CartoPrime.Modules.MyInformations.Views
{
    public partial class ContactsPage : ContentPage
    {
        public ContactsPage()
        {
            InitializeComponent();

            Title = "CartoPrime FC";

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

        private void btmRemovProp_Clicked(object sender, EventArgs e)
        {

            //DependencyService.Get<IAdmobInterstitial>().Show();
            //DependencyService.Get<IAdRewarded>().Show();
        }

        private void TapOpenAvaliacao_Tapped(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://play.google.com/store/apps/details?id=br.com.prime.ca"));
        }
    }
}
