using CartoPrime.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CartoPrime.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdsPage : ContentPage
    {
        private ObservableCollection<AdsModel> _listAds;
        public AdsPage()
        {
            InitializeComponent();
            Title = "Dicas Para Mitar";

            //AnalyticsService.RastrearEvento(EventAnalytics.Category_NavigationPage, "AdsPage");
            //ListAds.ItemsSource = _listAds;
        }

        #region Eventos
        private void Btm29DicasSite_Clicked(object sender, EventArgs e)
        {
            //AnalyticsService.RastrearEvento(EventAnalytics.Category_ClikedMonetize, "Btm29DicasSite");
            Browser.OpenAsync(UrlCartola._29DicasSite, BrowserLaunchMode.SystemPreferred);
        }

        private void Btm29DicasCheckout_Clicked(object sender, EventArgs e)
        {
            //AnalyticsService.RastrearEvento(EventAnalytics.Category_ClikedMonetize, "Btm29DicasCheckout");
            Browser.OpenAsync(UrlCartola._29DicasChekout, BrowserLaunchMode.SystemPreferred);
        }

        private void BtmDicasSite_Clicked(object sender, EventArgs e)
        {
            //AnalyticsService.RastrearEvento(EventAnalytics.Category_ClikedMonetize, "BtmDicasSite");
            Browser.OpenAsync(UrlCartola._dicasSite, BrowserLaunchMode.SystemPreferred);
        }

        private void BtmDicasCheckout_Clicked(object sender, EventArgs e)
        {
            //AnalyticsService.RastrearEvento(EventAnalytics.Category_ClikedMonetize, "BtmDicasCheckout");
            Browser.OpenAsync(UrlCartola._dicasChekout, BrowserLaunchMode.SystemPreferred);
        }

        private void BtmCartomanteMensal_Clicked(object sender, EventArgs e)
        {
            //AnalyticsService.RastrearEvento(EventAnalytics.Category_ClikedMonetize, "BtmCartomanteMensal");
            Browser.OpenAsync(UrlCartola._camanteMensal, BrowserLaunchMode.SystemPreferred);
        }

        private void BtmCartomanteAnual_Clicked(object sender, EventArgs e)
        {
            //AnalyticsService.RastrearEvento(EventAnalytics.Category_ClikedMonetize, "BtmCartomanteAnual");
            Browser.OpenAsync(UrlCartola._camanteAnual, BrowserLaunchMode.SystemPreferred);
        }

        private void BtmTraderSite_Clicked(object sender, EventArgs e)
        {
            //AnalyticsService.RastrearEvento(EventAnalytics.Category_ClikedMonetize, "BtmTraderSite");
            Browser.OpenAsync(UrlCartola._traderSite, BrowserLaunchMode.SystemPreferred);
        }

        private void BtmTraderCheckout_Clicked(object sender, EventArgs e)
        {
            //AnalyticsService.RastrearEvento(EventAnalytics.Category_ClikedMonetize, "BtmTraderCheckout");
            Browser.OpenAsync(UrlCartola._traderChekout, BrowserLaunchMode.SystemPreferred);
        }

        #endregion
    }

    public class AdsModel
    {
        public String Title { get; set; }
        public String Description { get; set; }
        public String UrlSite { get; set; }
        public String UrlCkt { get; set; }
        public String Img { get; set; }
    }
}