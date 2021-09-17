using CartoPrime.ViewModels;

namespace CartoPrime
{
    public partial class AppShell : Xamarin.Forms.Shell
    {


        public AppShell()
        {
            InitializeComponent();
            BindingContext = new AppShellViewModel();

        }




    }
}
