using CartoPrime.Helpers;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CartoPrime.Modules.Home.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NerecoPopUpView : PopupPage
    {
        public NerecoPopUpView()
        {
            InitializeComponent();
        }

        private async void CloseAllPopup()
        {
            await PopupNavigation.Instance.PopAllAsync();
        }


        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            CloseAllPopup();
        }

        private async void TapView(object sender, EventArgs e)
        {
            try
            {
                var args = (TappedEventArgs)e;
                var index = args.Parameter;
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    var view = PopupNavigation.Instance.PopupStack?.LastOrDefault();

                    if (view != null)
                        await PopupNavigation.Instance.RemovePageAsync(view).ConfigureAwait(true);
                }).ConfigureAwait(true);
                switch (index)
                {
                    case "1":
                        await Browser.OpenAsync(UrlCartola._nerecoYoutube, BrowserLaunchMode.SystemPreferred); break;
                    case "2":
                        await Browser.OpenAsync(UrlCartola._nerecoInsta, BrowserLaunchMode.SystemPreferred); break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}