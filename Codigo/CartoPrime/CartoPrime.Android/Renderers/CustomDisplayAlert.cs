using CartoPrime.Droid.Renderers;
using CartoPrime.Interfaces;
using Plugin.CurrentActivity;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(CustomDisplayAlert))]
namespace CartoPrime.Droid.Renderers
{
    [Android.Runtime.Preserve(AllMembers = true)]
    public class CustomDisplayAlert : ICustomDisplayAlert
    {
        public async Task<bool> Show(string title, string message, string accept)
        {
            try
            {
                return await ((MainActivity)CrossCurrentActivity.Current.Activity).Show(title, message, accept);
            }
            catch (Exception ex)
            {
                //mottu.Services.Exceptions.ErrorHelper.ErrorHandle(ex, "CustomDisplayAlert", "Show - 3 parametros");
            }

            return false;
        }

        public async Task<bool> Show(string title, string message, string accept, string cancel)
        {
            try
            {
                return await ((MainActivity)CrossCurrentActivity.Current.Activity).DisplayAlert(title, message, accept, cancel);
            }
            catch (Exception ex)
            {
                //mottu.Services.Exceptions.ErrorHelper.ErrorHandle(ex, "CustomDisplayAlert", "Show - 4 parametros");
            }

            return false;
        }

        public void Verificar()
        {
            try
            {
                ((MainActivity)CrossCurrentActivity.Current.Activity).Verificar();
            }
            catch (Exception ex)
            {
                //mottu.Services.Exceptions.ErrorHelper.ErrorHandle(ex, "CustomDisplayAlert", "Verificar");
            }
        }
    }
}