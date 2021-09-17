using Android.App;
using Android.Content;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Android.Graphics;
using Firebase.Messaging;
using CartoPrime.Interfaces;
using CartoPrime.Services;
using CartoPrime.Droid.Models;

namespace CartoPrime.Droid.Services
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class PushNotificationFirebaseMessagingService : FirebaseMessagingService
    {
        ICartoPrimeAppNotificationActionService _notificationActionService;
        INotificationRegistrationService _notificationRegistrationService;
        IDeviceInstallationService _deviceInstallationService;

        ICartoPrimeAppNotificationActionService NotificationActionService
            => _notificationActionService ??
                (_notificationActionService =
                ServiceContainer.Resolve<ICartoPrimeAppNotificationActionService>());

        INotificationRegistrationService NotificationRegistrationService
            => _notificationRegistrationService ??
                (_notificationRegistrationService =
                ServiceContainer.Resolve<INotificationRegistrationService>());

        IDeviceInstallationService DeviceInstallationService
            => _deviceInstallationService ??
                (_deviceInstallationService =
                ServiceContainer.Resolve<IDeviceInstallationService>());


        void SendNotification(IDictionary<string, string> data)
        {
            string messageTitle = null, messageBody = null, messageContent = null, ChannelID = null, messageLink = null, messageIcon = null, messageImage = null;
            JObject action1 = null, action2 = null;
            var intent = new Intent(this, typeof(MainActivity));

            int nID = NotificationID.getID();

            intent.PutExtra("nID", nID.ToString());

            intent.AddFlags(ActivityFlags.ClearTop).AddFlags(ActivityFlags.FromBackground);
            foreach (var key in data.Keys)
            {
                switch (key)
                {
                    case "title":
                        messageTitle = data[key];
                        break;
                    case "body":
                        messageBody = data[key];
                        break;
                    case "content":
                        JObject ct = JObject.Parse(data[key]);
                        JToken value;
                        if (ct.TryGetValue("content", out value))
                        {
                            messageContent = value.Value<string>();
                        }
                        if (ct.TryGetValue("link", out value))
                        {
                            messageLink = value.Value<string>();
                        }
                        if (ct.TryGetValue("icon", out value))
                        {
                            messageIcon = value.Value<string>();
                        }
                        if (ct.TryGetValue("image", out value))
                        {
                            messageImage = value.Value<string>();
                        }
                        if (ct.TryGetValue("categoryID", out value))
                        {
                            ChannelID = value.Value<string>();
                        }
                        if (ct.TryGetValue("action1", out value))
                        {
                            action1 = null;//JObject.Parse(value.Value<string>());
                        }
                        if (ct.TryGetValue("action2", out value))
                        {
                            action2 = null;// JObject.Parse(value.Value<string>());
                        }
                        break;
                }
                intent.PutExtra(key, data[key]);
            }

            var notificationBuilder =
                new Android.Support.V4.App.NotificationCompat.Builder(this, ChannelID ?? "L01")
                    .SetSmallIcon(Resource.Drawable.ic_notification)
                    .SetContentTitle(messageTitle ?? "")
                    .SetContentText(messageBody ?? "")
                    .SetAutoCancel(true)
                    //.SetContentIntent(pendingIntent)
                    .SetColor(Color.ParseColor("#ca6849"))
                    .SetColorized(true);

            Java.Net.URL url;
            Java.Net.HttpURLConnection connection;

            if (messageIcon != null)
            {
                url = new Java.Net.URL(messageIcon);
                connection = (Java.Net.HttpURLConnection)url.OpenConnection();
                connection.Connect();
                if (connection.ResponseCode == Java.Net.HttpStatus.Ok)
                {
                    notificationBuilder.SetLargeIcon(BitmapFactory.DecodeStream(connection.InputStream));
                }
                connection.Disconnect();
            }

            if (messageImage != null)
            {
                url = new Java.Net.URL(messageImage);
                connection = (Java.Net.HttpURLConnection)url.OpenConnection();
                connection.Connect();
                if (connection.ResponseCode == Java.Net.HttpStatus.Ok)
                {
                    notificationBuilder.SetStyle(new Android.Support.V4.App.NotificationCompat.BigPictureStyle().BigPicture(BitmapFactory.DecodeStream(connection.InputStream)));
                }

            }
            else if (messageContent != null)
            {
                notificationBuilder.SetStyle(new Android.Support.V4.App.NotificationCompat.BigTextStyle().BigText(messageContent));
            }

            var notificationManager = Android.Support.V4.App.NotificationManagerCompat.From(this);

            if (action1 != null)
            {
                PendingIntent pendingIntent, pendingIntent2;
                Intent actionLink;
                Intent actionMarkAsRead;
                JToken value;
                if (!action1.TryGetValue("link", out value))
                {
                    actionLink = new Intent(this, typeof(Receiver1));
                    actionLink.PutExtra("nID", nID);
                    actionLink.PutExtra("action", "MarkAsRead");
                    pendingIntent = PendingIntent.GetBroadcast(this, nID, actionLink, PendingIntentFlags.OneShot);
                }
                else
                {
                    intent.PutExtra("action", value.Value<string>());
                    pendingIntent = PendingIntent.GetActivity(this, nID, intent, PendingIntentFlags.OneShot);
                }
                notificationBuilder.SetContentIntent(pendingIntent).AddAction(0, action1.Value<string>("text"), pendingIntent);

                if (action2 != null)
                {
                    if (!action2.TryGetValue("link", out value))
                    {
                        actionMarkAsRead = new Intent(this, typeof(Receiver1));
                        actionMarkAsRead.PutExtra("nID", nID);
                        actionMarkAsRead.PutExtra("action", "MarkAsRead");
                        pendingIntent2 = PendingIntent.GetBroadcast(this, nID, actionMarkAsRead, PendingIntentFlags.OneShot);
                    }
                    else
                    {
                        intent.PutExtra("action", value.Value<string>());
                        pendingIntent2 = PendingIntent.GetActivity(this, nID, intent, PendingIntentFlags.OneShot);
                    }
                    notificationBuilder.AddAction(0, action2.Value<string>("text"), pendingIntent2);
                }
            }
            else
            {
                notificationBuilder.SetContentIntent(PendingIntent.GetActivity(this, nID, intent, PendingIntentFlags.OneShot));
            }
            notificationManager.Notify(nID, notificationBuilder.Build());
        }


        public override void OnNewToken(string token)
        {
            DeviceInstallationService.Token = token;

            NotificationRegistrationService.RefreshRegistrationAsync()
                .ContinueWith((task) => { if (task.IsFaulted) throw task.Exception; });
        }

        public override void OnMessageReceived(RemoteMessage message)
        {
            SendNotification(message.Data);
        }
    }
}