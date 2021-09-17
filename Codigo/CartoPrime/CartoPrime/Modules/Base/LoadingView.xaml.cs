using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace CartoPrime.Modules.Base
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingView : PopupPage
    {
        public LoadingView(string LoadMessage = null)
        {
            InitializeComponent();
            if (!string.IsNullOrEmpty(LoadMessage))
            {
                lblMessage.Text = LoadMessage;
            }
        }

        protected override bool OnBackButtonPressed() => true;
    }
}