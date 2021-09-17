using CartoPrime.Modules.Leagues.ViewModels;
using Xamarin.Forms;

namespace CartoPrime.Modules.Leagues.Views
{
    public partial class TeamsLeaguePage : ContentPage
    {
        public TeamsLeaguePage(string slug)
        {
            InitializeComponent();
            BindingContext = new TeamsLeaguePageViewModel(slug);
        }

        private void header_BackButtonPressedEvent(Enums.NavigationBackButtonAction showBackButtonActionRequested)
        {
            base.OnBackButtonPressed();
        }
    }
}
