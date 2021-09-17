using CartoPrime.Modules.Home.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CartoPrime.Modules.Home.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : ContentPage
    {
        HomeViewModel viewmodel;

        public HomeView()
        {
            InitializeComponent();
            BindingContext = new HomeViewModel();
        }
        protected async override void OnAppearing()
        {
            viewmodel = (HomeViewModel)BindingContext;
            _ = viewmodel.InitAsync().ConfigureAwait(true);
            base.OnAppearing();

        }
    }
}