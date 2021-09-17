using Android.Content;
using Android.Widget;
using AndroidX.Core.App;

namespace CartoPrime.Droid
{
    [BroadcastReceiver]
    public class Receiver1 : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            int nID = intent.GetIntExtra("nID", 0);
            switch (intent.GetStringExtra("action"))
            {
                case "MarkAsRead":
                    Toast.MakeText(context, "Marcada como lida!", ToastLength.Short).Show();
                    break;
                default:
                    Toast.MakeText(context, "Received intent!", ToastLength.Short).Show();
                    break;
            }
            var notificationManager = NotificationManagerCompat.From(context);
            notificationManager.Cancel(nID);
        }
    }
}