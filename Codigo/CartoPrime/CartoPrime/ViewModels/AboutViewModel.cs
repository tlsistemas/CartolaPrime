using CartoPrime.Http.Interfaces;
using CartoPrime.Modules.Base;


using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CartoPrime.ViewModels
{
    public class AboutViewModel : BaseViewModel<AboutViewModel>
    {
        public AboutViewModel(
             IApiService apiService)
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamain-quickstart"));
        }

        public ICommand OpenWebCommand { get; }
    }
}