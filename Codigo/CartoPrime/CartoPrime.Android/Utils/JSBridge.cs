using System;
using Android.Webkit;
using CartoPrime.Controls;
using CartoPrime.Droid.Renderers;
using Java.Interop;

namespace CartoPrime.Droid.Utils
{
    public class JSBridge : Java.Lang.Object
    {
        readonly WeakReference<HybridWebViewRenderer> hybridWebViewRenderer;

        public JSBridge(HybridWebViewRenderer hybridRenderer)
        {
            hybridWebViewRenderer = new WeakReference<HybridWebViewRenderer>(hybridRenderer);
        }

        [JavascriptInterface]
        [Export("invokeAction")]
        public void InvokeAction(string data)
        {
            HybridWebViewRenderer hybridRenderer;
            if (hybridWebViewRenderer != null && hybridWebViewRenderer.TryGetTarget(out hybridRenderer))
            {
                var dataBody = data;
                if (dataBody.Contains("|"))
                {
                    var paramArray = dataBody.Split("|");
                    var param1 = paramArray[0];
                    var param2 = paramArray[1];
                    ((HybridWebView)hybridRenderer.Element).InvokeAction(param1);
                }
                else
                {
                    ((HybridWebView)hybridRenderer.Element).InvokeAction(dataBody);
                }
            }
        }
    }
}