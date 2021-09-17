using System;
using System.Collections.Generic;
using System.Text;

namespace CartoPrime.Models.Enum
{
    public enum EMarket
    {
        Open = 1,
        Closed = 2,
        Support = 4
    }

    public static class EMarketExtension
    {
        public static String ToText(this EMarket request)
        {
            var text = "Nenhum";
            switch (request)
            {
                case EMarket.Open: text = "Aberto"; break;
                case EMarket.Closed: text = "Fechado"; break;
                case EMarket.Support: text = "Manutenção"; break;
            }
            return text;
        }
        public static IEnumerable<Int32> EnumToSelectList(this EMarket request)
        {
            int[] itemValor = (int[])EMarket.GetValues(typeof(EMarket));
            return itemValor;
        }
    }
}
