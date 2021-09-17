using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CartoPrime.Modules.Base.ViewModel
{
    public class LoadingViewModel : BaseViewModel<LoadingViewModel>
    {
        //public bool GetLogged { get => !string.IsNullOrEmpty(SecureStorage.GetAsync("Logged").Result); }

        public LoadingViewModel()
        {
            _ = Task.Run(async () => await LoadinPage().ConfigureAwait(true));
        }

        private async Task LoadinPage()
        {

        }

        #region Dispara Permissão Geolocation

        public async Task<PermissionStatus> CheckAndRequestLocationPermission()
        {
            PermissionStatus status = PermissionStatus.Unknown;
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                status = await Permissions.CheckStatusAsync<Permissions.LocationAlways>().ConfigureAwait(true);
                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.LocationAlways>().ConfigureAwait(true);
                    if (status == PermissionStatus.Granted)
                        await SecureStorage.SetAsync("LocationAlwaysPermission", "1");
                }
            }).ConfigureAwait(true);
            return status;
        }

        #endregion Dispara Permissão Geolocation
    }
}
