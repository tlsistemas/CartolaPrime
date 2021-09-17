using System.ComponentModel;
using System.Diagnostics;
using Android.Content;
using Android.Gms.Ads;
using Android.Widget;
using CartoPrime.Droid.Renderers;
using CartoPrime.Helpers;
using CartoPrime.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AdMobView), typeof(AdMobViewRenderer))]
namespace CartoPrime.Droid.Renderers
{
    public class AdMobViewRenderer : ViewRenderer
    {
        public AdMobViewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                var adView = new AdView(Context);

                switch ((Element as AdMobView).Size)
                {
                    case AdMobView.Sizes.Standardbanner:
                        adView.AdSize = AdSize.Banner;
                        break;
                    case AdMobView.Sizes.LargeBanner:
                        adView.AdSize = AdSize.LargeBanner;
                        break;
                    case AdMobView.Sizes.MediumRectangle:
                        adView.AdSize = AdSize.MediumRectangle;
                        break;
                    case AdMobView.Sizes.FullBanner:
                        adView.AdSize = AdSize.FullBanner;
                        break;
                    case AdMobView.Sizes.Leaderboard:
                        adView.AdSize = AdSize.Leaderboard;
                        break;
                    case AdMobView.Sizes.SmartBannerPortrait:
                        adView.AdSize = AdSize.SmartBanner;
                        break;
                    default:
                        adView.AdSize = AdSize.Banner;
                        break;
                }

                // TODO: change this id to your admob id
                adView.AdUnitId = UrlCartola.BANNER_ID_CARTOLA_PRIME;

                if (Debugger.IsAttached)
                {                 
                    // Teste
                    adView.LoadAd(new AdRequest.Builder().AddTestDevice(AdRequest.DeviceIdEmulator).Build());
                }
                else
                {
                    // produção
                    var requestbuilder = new AdRequest.Builder();
                    adView.LoadAd(requestbuilder.Build());
                }





                SetNativeControl(adView);
            }
        }
    }
}