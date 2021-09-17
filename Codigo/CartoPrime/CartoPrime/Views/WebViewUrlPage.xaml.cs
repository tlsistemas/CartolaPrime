using CartoPrime.Interfaces;
using CartoPrime.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CartoPrime.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WebViewUrlPage : ContentPage
    {
        private LoadingPopupPage pageLoading;
        public WebViewUrlPage(string url, string title, string sendEvent)
        {
            InitializeComponent();

            pageLoading = new LoadingPopupPage();
            Title = title;
            try
            {
                Browser.Uri = url;
                //AnalyticsService.RastrearEvento(EventAnalytics.Category_NavigationPage, "MaisEscaladosPage");
                //LayAdView.AdUnitId = UrlCartola.BANNER_ID_CARTOLA_PRIME;
                AnalyticsEvents.AnalyticsHandle("WebViewUrlPage", sendEvent);

                var mostrarProp = int.Parse(App.Current.Properties["click"].ToString());
                mostrarProp += 1;

                if (mostrarProp == 5)
                {
                    mostrarProp = 0;
                    DependencyService.Get<IAdmobInterstitial>().Show();
                }

                App.Current.Properties["click"] = mostrarProp;
            }
            catch (System.Exception ex)
            {
                AnalyticsEvents.AnalyticsHandle("AthletesPageView", sendEvent, ex.StackTrace);

            }

        }
    }
}