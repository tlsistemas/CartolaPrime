using System;
using System.Collections.Generic;
using System.Text;

namespace CartoPrime.Helpers
{
    public class UrlCartola
    {
        public static string _competicoes = Protocol.HTTPS + RootCartola.CAF + "auth/competicoes";
        public static string _competicoes_finalizadas = Protocol.HTTPS + RootCartola.CAF + "competicoes/finalizadas";
        public static string _convites = Protocol.HTTPS + RootCartola.CAF + "auth/convites";
        public static string _mensagem = Protocol.HTTPS + RootCartola.CAF + "auth/mensagem/";
        public static string _competicoes_mata_mata_finalizados = Protocol.HTTPS + RootCartola.CAF + "auth/competicoes/mata-mata/finalizados";
        public static string _mercado_atleta_pontuacao = Protocol.HTTPS + RootCartola.CAF + "auth/mercado/atleta/%s/pontuacao";
        public static string _time_salvar = Protocol.HTTPS + RootCartola.CAF + "auth/time/salvar";
        public static String liga_associacao(String str)
        {
            return Protocol.HTTPS + RootCartola.CAF +"auth/liga/" + str + "/associacao";
        }



        public static string _pontuados = Protocol.HTTPS + RootCartola.CAF + "atletas/pontuados";
        public static string _destaques = Protocol.HTTPS + RootCartola.CAF + "mercado/destaques";
        public static string _clubes = Protocol.HTTPS + RootCartola.CAF + "clubes";
        public static string _timeSlug = Protocol.HTTPS + RootCartola.CAF + "time/slug/";
        public static string _timeId = Protocol.HTTPS + RootCartola.CAF + "time/id/";
        public static string _timeNome = Protocol.HTTPS + RootCartola.CAF + "times?q=";
        public static string _ligaNome = Protocol.HTTPS + RootCartola.CAF + "ligas?q=";
        public static string _ligaSlug = Protocol.HTTPS + RootCartola.CAF + "auth/liga/";
        public static string _myTime = Protocol.HTTPS + RootCartola.CAF + "auth/time/";
        public static string _myTime_info = Protocol.HTTPS + RootCartola.CAF + "auth/time/info";
        public static string _pos_rodada_destaques = Protocol.HTTPS + RootCartola.CAF + "pos-rodada/destaques";
        public static string _noticias = Protocol.HTTPS + RootCartola.CAF + "auth/noticias";
        public static string _esquemas = Protocol.HTTPS + RootCartola.CAF + "esquemas";
        public static string _ligaSlugFillRodada1 = "?page=1&orderBy=rodada";
        public static string _ligaSlugFillRodada2 = "?page=2&orderBy=rodada";
        public static string _ligaSlugFillRodada3 = "?page=3&orderBy=rodada";
        public static string _ligaSlugFillRodada4 = "?page=4&orderBy=rodada";
        public static string _ligaSlugFillRodada5 = "?page=5&orderBy=rodada";
        public static string _ligaSlugFillRodada6 = "?page=6&orderBy=rodada";
        public static string _ligaSlugFillRodada7 = "?page=7&orderBy=rodada";
        public static string _ligaSlugFillRodada8 = "?page=8&orderBy=rodada";
        public static string _ligaSlugFillRodada9 = "?page=9&orderBy=rodada";
        public static string _ligaSlugFillRodada10 = "?page=10&orderBy=rodada";

        public static string _ligaLogado = Protocol.HTTPS + RootCartola.CAF + "auth/ligas";
        public static string _mercadoStatus = Protocol.HTTPS + RootCartola.CAF + "mercado/status";
        public static string _mercadoAtle = Protocol.HTTPS + RootCartola.CAF + "atletas/mercado";
        public static string _partidas = Protocol.HTTPS + RootCartola.CAF + "partidas";
        public static string _timeAuth = Protocol.HTTPS + RootCartola.CAF + "auth/time";
        public static string _timeSubstituicao = Protocol.HTTPS + RootCartola.CAF + "time/substituicoes/";

        public static string _ca = Protocol.HTTPS + RootCartola.AUT_CONT + "/#!/time";
        public static string _login = Protocol.HTTPS + RootCartola.GLO + "api/authentication";
        public static string _login2 = Protocol.HTTPS + RootCartola.AUT + Protocol.HTTPS + RootCartola.AUT_CONT;
        public static string _classBra = Protocol.HTTP + RootCartola.GLOS + "servico/esportes_campeonato/classificacao/resumida/futebol/futebol_de_campo/profissional/campeonato-brasileiro/campeonato-brasileiro-";
        public static string _centralj = Protocol.HTTPS + RootCartola.GLOS + "temporeal/futebol/central.json";
        public static string _classBraB = Protocol.HTTPS + RootCartola.GLOS + "futebol/brasileirao-serie-b";



        public static string _EscaladosPorPosicao = Protocol.HTTPS + RootCartola.GLOSINTER + "cartola-fc/mais-escalados/mais-escalados-do-cartola-fc";

        public static String _camanteAnual = Protocol.HTTPS + RootCartola.HOTMART + "E6322446U?ap=2454";
        public static String _camanteMensal = Protocol.HTTPS + RootCartola.HOTMART + "E6322446U?ap=6860";
        public static String _guru = Protocol.HTTPS + RootCartola.HOTMART + "R14141301O";



        public static String _traderSite = Protocol.HTTPS + RootCartola.MONETIZZE + "r/APJ5735531";
        public static String _traderChekout = Protocol.HTTPS + RootCartola.MONETIZZE + "r/APJ5735531?u=c";



        public static String _29DicasSite = Protocol.HTTPS + RootCartola.MONETIZZE + "r/AXB5719892";
        public static String _29DicasChekout = Protocol.HTTPS + RootCartola.MONETIZZE + "r/AXB5719892?u=c";

        public static String _dicasSite = Protocol.HTTPS + RootCartola.MONETIZZE + "r/ABV5719898";
        public static String _dicasChekout = Protocol.HTTPS + RootCartola.MONETIZZE + "r/ABV5719898?u=c";



        public static String _CartoPrimePlanoSocioAnual = "https://www.mercadopago.com.br/checkout/v1/payment/redirect/f042ca8c-3d6c-46a8-ac43-fd3ba7eb4a15/payment-option-form/?preference-id=170885170-5c5766c2-c835-4def-807d-7103fe316847&source=link&p=1088c79723fc538025452907560faded#/";
        public static String _CartoPrimePlanoSocio1Turno = "https://www.mercadopago.com.br/checkout/v1/payment/redirect/616f17b5-2700-4bdb-917f-cc74ededc012/payment-option-form/?source=link&preference-id=170885170-60d3b069-e9ab-4a5b-be19-858177fb57cb&p=1088c79723fc538025452907560faded#/";
        public static String _CartoPrimePlanoSocio2Turno = "https://www.mercadopago.com.br/checkout/v1/payment/redirect/a6a81140-e41b-4e8a-9f42-2b90c34f526d/payment-option-form/?source=link&preference-id=170885170-d746905d-f7ea-4410-bac7-14bc518d2ed0&p=1088c79723fc538025452907560faded#/";

        public static String BANNER_ID_CARTO_LOUCO = "ca-app-pub-2375658831220075/4032205947";
        public static String BANNER_ID_CARTOLA_PRIME = "ca-app-pub-2375658831220075/4567343850";
        public static String INTERS_ID_CARTOLA_PRIME = "ca-app-pub-2375658831220075/5852505365";
        public static String BANNER_ID_TEST = "ca-app-pub-3940256099942544/2934735716";


        public static String _donloadDriveConfrontoBonsBico = Protocol.HTTPS + RootCartola.DriveG + "uc?id=1PGoG_mGmyDxSAgRO5vzlSSUJrIDSH8lR&export=download";
        public static String _donloadDriveConfrontoBonsBicoB = Protocol.HTTPS + RootCartola.DriveG + "uc?id=1UPAbVOvpAOBcQQ_mCCNc37EjzejQWyku&export=download";

        public static String _reiDoPitaco = "https://www.reidopitaco.com.br/";
        public static String _instagranCartoPrime = "https://instagram.com/CartoPrimefc?igshid=i4tg6burniud";
        public static String _nerecoYoutube = "https://www.youtube.com/channel/UCq4Lm8uMSGUJQfn1qxsEY0A";
        public static String _nerecoInsta = "https://www.instagram.com/_nareco/?igshid=10ctf664s31o6";
        public static String _canalCartoPrime = "https://www.youtube.com/channel/UCNj0F2CM6JT8MojxKXNf9Qw";


    }
}
