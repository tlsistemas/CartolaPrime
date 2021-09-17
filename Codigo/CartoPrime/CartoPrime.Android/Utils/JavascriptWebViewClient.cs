using Android.Webkit;
using CartoPrime.Droid.Renderers;
using Xamarin.Forms.Platform.Android;

namespace CartoPrime.Droid.Utils
{
    public class JavascriptWebViewClient : FormsWebViewClient
    {
        string _javascript;
        public JavascriptWebViewClient(HybridWebViewRenderer renderer, string javascript) : base(renderer)
        {
            _javascript = javascript;
        }


        public override void OnPageFinished(WebView view, string url)
        {
            base.OnPageFinished(view, url);
            view.EvaluateJavascript(_javascript, null);
        }

    }
}