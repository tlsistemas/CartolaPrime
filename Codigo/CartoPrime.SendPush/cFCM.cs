using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CartolaPrime.SendPush
{
    public class cFCM
    {
        /// <summary>
        /// Get Access Token From JSON Key Async
        /// </summary>
        /// <param name="jsonKeyFilePath">Path to your JSON Key file</param>
        /// <param name="scopes">Scopes required in access token</param>
        /// <returns>Access token as string Task</returns>
        public static async Task<string> GetAccessTokenFromJSONKeyAsync(string jsonKeyFilePath, params string[] scopes)
        {
            using (var stream = new FileStream(jsonKeyFilePath, FileMode.Open, FileAccess.Read))
            {
                return await GoogleCredential
                    .FromStream(stream) // Loads key file
                    .CreateScoped(scopes) // Gathers scopes requested
                    .UnderlyingCredential // Gets the credentials
                    .GetAccessTokenForRequestAsync(); // Gets the Access Token
            }
        }

        /// <summary>
        /// Get Access Token From JSON Key
        /// </summary>
        /// <param name="jsonKeyFilePath">Path to your JSON Key file</param>
        /// <param name="scopes">Scopes required in access token</param>
        /// <returns>Access token as string</returns>
        public static string GetAccessTokenFromJSONKey(string jsonKeyFilePath, params string[] scopes)
        {
            return GetAccessTokenFromJSONKeyAsync(jsonKeyFilePath, scopes).Result;
        }

        /// <summary>
        /// Get Access Token From P12 Key Async
        /// </summary>
        /// <param name="p12KeyFilePath">Path to your P12 Key file</param>
        /// <param name="serviceAccountEmail">Service Account Email</param>
        /// <param name="keyPassword">Key Password</param>
        /// <param name="scopes">Scopes required in access token</param>
        /// <returns>Access token as string Task</returns>
        public static async Task<string> GetAccessTokenFromP12KeyAsync(string p12KeyFilePath, string serviceAccountEmail, string keyPassword = "notasecret", params string[] scopes)
        {
            return await new ServiceAccountCredential(
                new ServiceAccountCredential.Initializer(serviceAccountEmail)
                {
                    Scopes = scopes
                }.FromCertificate(
                    new X509Certificate2(
                        p12KeyFilePath,
                        keyPassword,
                        X509KeyStorageFlags.Exportable))).GetAccessTokenForRequestAsync();
        }

        /// <summary>
        /// Get Access Token From P12 Key
        /// </summary>
        /// <param name="p12KeyFilePath">Path to your P12 Key file</param>
        /// <param name="serviceAccountEmail">Service Account Email</param>
        /// <param name="keyPassword">Key Password</param>
        /// <param name="scopes">Scopes required in access token</param>
        /// <returns>Access token as string</returns>
        public static string GetAccessTokenFromP12Key(string p12KeyFilePath, string serviceAccountEmail, string keyPassword, params string[] scopes)
        {
            return GetAccessTokenFromP12KeyAsync(p12KeyFilePath, serviceAccountEmail, keyPassword, scopes).Result;
        }

        public static string GetTokenAndCall()
        {
            var token = GetAccessTokenFromJSONKey(
             "CartoPrime-app.json",
             "https://www.googleapis.com/auth/firebase.messaging");
            return token;
        }
    }
}
