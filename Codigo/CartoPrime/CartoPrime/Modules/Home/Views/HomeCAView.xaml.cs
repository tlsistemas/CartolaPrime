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
    public partial class HomeCAView : ContentPage
    {
        HomeCAViewModel viewmodel;
        public HomeCAView()
        {
            InitializeComponent();
            BindingContext = new HomeCAViewModel();
        }

        protected async override void OnAppearing()
        {
            viewmodel = (HomeCAViewModel)BindingContext;
            _ = viewmodel.InitAsync().ConfigureAwait(true);
            base.OnAppearing();

        }
    }
}