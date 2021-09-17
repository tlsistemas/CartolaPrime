using System;
using System.Collections.Generic;
using System.Text;

namespace CartoPrime.Services
{
    public class AnalyticsEvents
    {
        //private static ITrackingService TrackingService;

        public AnalyticsEvents()
        {
            //TrackingService = ContainerLocator.Resolve<ITrackingService>();
        }

        #region Analytics

        public static void AnalyticsHandle(string className, string methodName, string eventId = null)
        {
            var properties = new Dictionary<string, string> { { className, methodName } };
            //Microsoft.AppCenter.Analytics.Analytics.TrackEvent(className, properties);
            App.FirebaseAnalyticsService.SendEvent(className, properties);
            //if (eventId != null)
            //{
            //    TrackingService.AppsFlyerSendEvent(eventId);
            //}
        }

        public static void AnalyticsFirebaseHandle(string className, string methodName, string eventId)
        {
            var properties = new Dictionary<string, string> { { methodName, eventId } };
            App.FirebaseAnalyticsService.SendEvent(className, properties);
        }

        public static void AnalyticsErrorHandle(string className, string methodName, string messageErro)
        {
            var properties = new Dictionary<string, string> { { methodName, messageErro } };
            //Microsoft.AppCenter.Analytics.Analytics.TrackEvent(className, properties);
            App.FirebaseAnalyticsService.SendEvent(className, properties);
        }

        public static void AnalyticsErrorHandle(string className, string methodName, string message, string messageErro)
        {
            var properties = new Dictionary<string, string> { { className, methodName } };
            properties.Add(message, messageErro);
            //Microsoft.AppCenter.Analytics.Analytics.TrackEvent(className, properties);
            App.FirebaseAnalyticsService.SendEvent(className, properties);
        }

        #endregion Analytics
    }
}
