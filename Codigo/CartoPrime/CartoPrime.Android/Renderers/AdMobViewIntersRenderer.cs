using Android.Gms.Ads;
using CartoPrime.Droid.Renderers;
using CartoPrime.Helpers;
using CartoPrime.Interfaces;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Dependency(typeof(AdMobViewIntersRenderer))]
namespace CartoPrime.Droid.Renderers
{
    public class AdMobViewIntersRenderer : ViewRenderer, IAdmobInterstitial
    {
        InterstitialAd _ad;
        public void Show()
        {
            //var context = Application.Context;
            _ad = new InterstitialAd(this.Context);

            // AdView Produção Cartoleiro Prime
            _ad.AdUnitId = UrlCartola.INTERS_ID_CARTOLA_PRIME;

            var intlistener = new InterstitialAdListener(_ad);

            intlistener.OnAdLoaded();
            _ad.AdListener = intlistener;


            if (Debugger.IsAttached)
            {
                // Propagandas Test
                var builderTest = new AdRequest.Builder().AddTestDevice(AdRequest.DeviceIdEmulator)
                .Build();
                _ad.LoadAd(builderTest);
            }
            else
            {
                // Propagandas Produção
                var requestbuilder = new AdRequest.Builder();
                _ad.LoadAd(requestbuilder.Build());
            }









        }
    }


    public class InterstitialAdListener : AdListener
    {
        readonly InterstitialAd _ad;

        public InterstitialAdListener(InterstitialAd ad)
        {
            _ad = ad;
        }

        public override void OnAdLoaded()
        {
            base.OnAdLoaded();

            if (_ad.IsLoaded)
                _ad.Show();
        }
    }
}