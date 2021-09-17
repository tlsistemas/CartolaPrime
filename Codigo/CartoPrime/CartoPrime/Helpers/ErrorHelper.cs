using CartoPrime.Services.Exceptions;
using CartoPrime.Services.RequestProvider;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace CartoPrime.Helpers
{
    public static class ErrorHelper
    {
        #region Error

        public static void ErrorHandle(Exception ex, string className, string methodName)
        {
            var prop = new Dictionary<string, string>();
            ErrorHandle(ex, className, methodName, prop);
        }

        public static void ErrorHandle(Exception ex, string className, string methodName, Dictionary<string, string> properties)
        {
            properties.Add(className, methodName);
            if (!(ex is null))
            {
                properties.Add("LineError", new StackTrace(ex, true)?.GetFrames()?.FirstOrDefault()?.GetFileLineNumber().ToString());
                properties.Add("MetodoError", new StackTrace(ex, true)?.GetFrames()?.FirstOrDefault()?.GetMethod()?.ToString());
                properties.Add("FileError", new StackTrace(ex, true)?.GetFrames()?.FirstOrDefault()?.GetFileName()?.ToString());
            }

            // Access
            if (ex is ServiceAuthenticationException)
            {
                // May or not log this ===================
                properties.Add("ServiceAuthentication", (ex as ServiceAuthenticationException).Content);
                // =================================
            }

            // Requests
            if (ex is HttpRequestException)
            {
                properties.Add("HttpCode", (ex as HttpRequestExceptionEx).HttpCode.ToString());
            }

            // Deafult
            properties.Add("MessageException", ex.ToString());
            Crashes.TrackError(ex, properties);
        }

        #endregion Error
    }
}
