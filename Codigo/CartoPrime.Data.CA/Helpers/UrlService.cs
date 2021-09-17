using System;
using System.Collections.Generic;
using System.Text;

namespace CartoPrime.Data.CA.Helpers
{
    public class UrlService
    {
        public static string _pontuados = Protocol.HTTPS + RootCartola.CAF + "atletas/pontuados";
        public static string _registerPush = Protocol.HTTP + RootCartola.Api + "Push";

        public static string ApiKey { get; internal set; }
    }
}
