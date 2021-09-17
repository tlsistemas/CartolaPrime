using CartoPrime.Helpers;
using CartoPrime.Views;
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
    public partial class PartnerPopUpView : PopupPage
    {
        public PartnerPopUpView()
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

        private async void TapPlano(object sender, EventArgs e)
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
                        await App.Current.MainPage.Navigation.PushModalAsync(new WebViewUrlPage(UrlCartola._CartoPrimePlanoSocioAnual, "PLANO DE SÓCIO TOP 3 ANUAL", ""));
                        break;
                    case "2":
                        await App.Current.MainPage.Navigation.PushModalAsync(new WebViewUrlPage(UrlCartola._CartoPrimePlanoSocio1Turno, "PLANO DE SÓCIO TOP 3 1° TURNO", ""));
                        break;
                    case "3":
                        await App.Current.MainPage.Navigation.PushModalAsync(new WebViewUrlPage(UrlCartola._CartoPrimePlanoSocio2Turno, "PLANO DE SÓCIO TOP 3 2° TURNO", ""));
                        break;
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