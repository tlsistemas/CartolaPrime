using CartoPrime.Helpers;
using CartoPrime.Models;
using CartoPrime.Modules.Leagues.ViewModels;
using MonkeyCache.FileStore;
using Xamarin.Forms;

namespace CartoPrime.Modules.Leagues.Views
{
    public partial class LeaguesPage : ContentPage
    {
        LeaguesPageViewModel ViewModel;
        public LeaguesPage()
        {
            InitializeComponent();
            BindingContext = new LeaguesPageViewModel();

        }
    }
}
