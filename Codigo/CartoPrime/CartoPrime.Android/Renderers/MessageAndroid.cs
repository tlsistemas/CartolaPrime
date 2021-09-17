
using Android.App;
using Android.Widget;
using CartoPrime.Droid.Renderers;
using CartoPrime.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(MessageAndroid))]
namespace CartoPrime.Droid.Renderers
{
    public class MessageAndroid : IMessage
    {
        public void LongAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }

        public void ShortAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }
    }
}