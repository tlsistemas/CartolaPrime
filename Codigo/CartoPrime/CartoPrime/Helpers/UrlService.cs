using System;
using System.Collections.Generic;
using System.Text;

namespace CartoPrime.Helpers
{
    public class UrlService
    {
        public static string _pontuados = Protocol.HTTPS + RootCartola.CAF + "atletas/pontuados";
        public static string _registerPush = Protocol.HTTP + RootCartola.Api + "Push";
        public static string _bb_jogosSerieA = Protocol.HTTP + RootCartola.Api + "BonsDeBico";
        public static string _bb_jogosSerieB = Protocol.HTTP + RootCartola.Api + "BonsDeBicoB";
        public static string _bb_jogosAtletas = Protocol.HTTP + RootCartola.Api + "BonsDeBico/BonsDeBicoAtletas";

        public static string _bb_classifSerieA = Protocol.HTTP + RootCartola.Sites + "tabelabba";
        public static string _bb_classifSerieB = Protocol.HTTP + RootCartola.Sites + "tabelabbb";


        public static string ApiKey { get; internal set; }
    }
}
