using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using FFImageLoading.Forms.Platform;
using MonkeyCache.FileStore;
using System.Threading.Tasks;
using System.Threading;
using Android.Content;
using CartoPrime.Droid.Services;
using CartoPrime.Interfaces;
using CartoPrime.Services;
using Firebase.Installations;
using Firebase;
using Firebase.Iid;
using CartoPrime.Container;
using Plugin.CurrentActivity;

namespace CartoPrime.Droid
{
    [Activity(Label = "CartoPrime", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, Android.Gms.Tasks.IOnSuccessListener
    {
        IDeviceInstallationService _deviceInstallationService;
        IDeviceInstallationService DeviceInstallationService
            => _deviceInstallationService ??
                (_deviceInstallationService =
                ServiceContainer.Resolve<IDeviceInstallationService>());

        internal static readonly int NOTIFICATION_ID = 0;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Bootstrap.Begin(() => new DeviceInstallationService());

            Xamarin.Forms.Forms.SetFlags("Brush_Experimental");
            //global::Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");

            global::Rg.Plugins.Popup.Popup.Init(this);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            CachedImageRenderer.Init(true);
            Barrel.ApplicationId = "cach_ca";

            var refreshedToken = FirebaseInstanceId.Instance.Token;
            if (DeviceInstallationService.NotificationsSupported)
            {
                FirebaseInstallations.GetInstance(FirebaseApp.Instance).GetToken(false).AddOnSuccessListener(this);
                CreateNotificationChannel();
            }
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            LoadApplication(new App());

            if (Intent.Extras != null)
            {
                foreach (var key in Intent.Extras.KeySet())
                {
                    if (key != null)
                    {
                        string launchURL = Intent.Extras.Get("action").ToString();

                    }
                }
            }
        }
        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            if (intent != null)
            {
                string launchURL = intent.Extras.Get("action").ToString();
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void OnSuccess(Java.Lang.Object result)
                => DeviceInstallationService.Token =
                    result.Class.GetMethod("getToken").Invoke(result).ToString();


        void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                return;
            }

            var notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);

            var channel = new NotificationChannel("L01", "Pedidos", NotificationImportance.High)
            {
                Description = "Situação dos pedidos"
            };
            notificationManager.CreateNotificationChannel(channel);
            channel = new NotificationChannel("L02", "Incentivos", NotificationImportance.Default)
            {
                Description = "Informações sobre Incentivos"
            };
            notificationManager.CreateNotificationChannel(channel);
        }

        #region Display
        public void Verificar()
        {
            try
            {
                var locationProviders = Android.Provider.Settings.Secure.GetString(this.ContentResolver, Android.Provider.Settings.Secure.LocationProvidersAllowed);

                if (locationProviders == null || string.IsNullOrEmpty(locationProviders))
                    StartActivity(new Intent(Android.Provider.Settings.ActionLocationSourceSettings));
            }
            catch (System.Exception ex)
            {
                //mottu.Services.Exceptions.ErrorHelper.ErrorHandle(ex, "MainActivity", "Verificar");
            }
        }

        public async Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
        {
            var tokenSource = new CancellationTokenSource();

            var ct = tokenSource.Token;

            AlertDialog.Builder builder = new AlertDialog.Builder(new ContextThemeWrapper(this, Resource.Style.AlertDialog_AppCompat));

            builder.SetTitle(title);

            builder.SetMessage(message);

            var result = false;

            builder.SetPositiveButton(accept, (sender, e) =>
            {
                result = true;

                tokenSource.Cancel();
            });

            builder.SetNegativeButton(cancel, (sender, args) =>
            {
                tokenSource.Cancel();
            });

            builder.Show();

            var task = Task.Run(() =>
            {
                while (true)
                {
                    if (ct.IsCancellationRequested)
                        return;

                    Thread.Sleep(100);
                }
            }, tokenSource.Token);

            await task;

            return result;

        }

        public async Task<bool> Show(string title, string message, string accept)
        {
            var tokenSource = new CancellationTokenSource();

            var ct = tokenSource.Token;

            AlertDialog.Builder builder = new AlertDialog.Builder(new ContextThemeWrapper(this, Resource.Style.AlertDialog_AppCompat));

            builder.SetTitle(title);
            //builder.SetIcon(Resource.Drawable.my_icon);
            builder.SetMessage(message);

            var result = false;

            builder.SetPositiveButton(accept, (sender, e) =>
            {
                result = true;

                tokenSource.Cancel();
            });

            builder.Show();

            var task = Task.Run(() =>
            {
                while (true)
                {
                    if (ct.IsCancellationRequested)
                        return;

                    Thread.Sleep(1);
                }
            }, tokenSource.Token);

            await task;

            return result;

        }

        #endregion
    }
}