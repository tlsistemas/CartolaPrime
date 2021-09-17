using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using CartoPrime.Droid.Renderers;
using CartoPrime.Modules.Authentication.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CookieWebView), typeof(CookieWebViewRenderer))]
namespace CartoPrime.Droid.Renderers
{
    public class CookieWebViewRenderer
         : Xamarin.Forms.Platform.Android.WebViewRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.WebView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                Control.SetWebViewClient(new CookieWebViewClient(CookieWebView));
            }
        }

        public CookieWebView CookieWebView
        {
            get { return Element as CookieWebView; }
        }

        public static void ClearCookies()
        {
            CookieSyncManager.CreateInstance(Forms.Context);
            CookieManager cookieManager = CookieManager.Instance;
            cookieManager.RemoveAllCookie();
        }
    }

    internal class CookieWebViewClient
        : WebViewClient
    {
        private readonly CookieWebView _cookieWebView;
        internal CookieWebViewClient(CookieWebView cookieWebView)
        {
            _cookieWebView = cookieWebView;
        }

        public override void OnPageStarted(global::Android.Webkit.WebView view, string url, Android.Graphics.Bitmap favicon)
        {
            base.OnPageStarted(view, url, favicon);

            _cookieWebView.OnNavigating(new CookieNavigationEventArgs
            {
                Url = url
            });
        }

        public override void OnPageFinished(global::Android.Webkit.WebView view, string url)
        {

            string cookieHeader = CookieManager.Instance.GetCookie(url);
            var cookies = new CookieCollection();

            if (cookieHeader != null)
            {
                String[] cookiesArray = cookieHeader.Split(';');

                for (int i = 0; i < cookiesArray.Length; i++)
                {
                    var cookiePairs = cookiesArray[i].Split('&');
                    var cookiePieces = cookiePairs;//.Split('=');
                    if (cookiePieces[0].Contains("="))
                    {
                        //cookiePieces[0] = cookiePieces[0].Substring(0, cookiePieces[0].IndexOf("="));
                        Cookie c = new Cookie();
                        int posc = cookiePieces[0].IndexOf("=");
                        c.Name = cookiePieces[0].Substring(0, posc).Trim();
                        c.Value = cookiePieces[0].Substring(posc + 1).Trim();

                        cookies.Add(c);
                    }
                }

                _cookieWebView.OnNavigated(new CookieNavigatedEventArgs
                {
                    Cookies = cookies,
                    Url = url
                });
                CookieManager.Instance.RemoveAllCookie();
            }
        }


    }
}