using Android.OS;
using CartoPrime.Droid.Services;
using CartoPrime.Interfaces;
using Firebase.Analytics;
using Plugin.CurrentActivity;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Xamarin.Forms;

//https://medium.com/firebase-developers/firebase-analytics-on-xamarin-forms-c60ae8bbb24a
[assembly: Dependency(typeof(FirebaseAnalyticsService))]
namespace CartoPrime.Droid.Services
{
    public class FirebaseAnalyticsService : IFirebaseAnalytics
    {
        public void SendEvent(string eventId)
        {
            SendEvent(eventId, null);
        }

        public void SendEvent(string eventId, string paramName, string value)
        {
            SendEvent(eventId, new Dictionary<string, string>
            {
                {paramName, value}
            });
        }

        public void SendEvent(string eventId, IDictionary<string, string> parameters)
        {

            //utility method to fix eventId, you can skip it if you are sure to always pass valid eventIds
            eventId = FixEventId(eventId);

            var fireBaseAnalytics = FirebaseAnalytics.GetInstance(CrossCurrentActivity.Current.AppContext);

            if (parameters == null)
            {
                fireBaseAnalytics.LogEvent(eventId, null);
                return;
            }

            var bundle = new Bundle();

            foreach (var item in parameters)
            {
                bundle.PutString(item.Key, item.Value);
            }

            fireBaseAnalytics.LogEvent(eventId, bundle);
        }

        //utility method to fix eventId, you can skip it if you are sure to always pass valid eventIds
        private string FixEventId(string eventId)
        {
            if (string.IsNullOrWhiteSpace(eventId))
                return "unknown";

            //remove unwanted characters
            eventId = Regex.Replace(eventId, @"[^a-zA-Z0-9_]+", "_", RegexOptions.Compiled);

            //trim to 40 if needed
            return eventId.Substring(0, Math.Min(40, eventId.Length));
        }

    }
}