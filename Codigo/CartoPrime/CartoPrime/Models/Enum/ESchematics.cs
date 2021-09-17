using System;
using System.Collections.Generic;
using System.Text;

namespace CartoPrime.Models.Enum
{
    public enum ESchematics
    {
        P343 = 1,
        P352 = 2,
        P433 = 3,
        P442 = 4,
        P451 = 5,
        P532 = 6,
        P541 = 7
    }

    public static class EschematicsExtension
    {
        public static String ToText(this ESchematics request)
        {
            var text = "Nenhum";
            switch (request)
            {
                case ESchematics.P343: text = "3-4-3"; break;
                case ESchematics.P352: text = "3-5-2"; break;
                case ESchematics.P433: text = "4-3-3"; break;
                case ESchematics.P442: text = "4-4-2"; break;
                case ESchematics.P451: text = "4-5-1"; break;
                case ESchematics.P532: text = "5-3-2"; break;
                case ESchematics.P541: text = "5-4-1"; break;
            }
            return text;
        }
        public static IEnumerable<Int32> EnumToSelectList(this ESchematics request)
        {
            int[] itemValor = (int[])ESchematics.GetValues(typeof(ESchematics));
            return itemValor;
        }

        public static IEnumerable<String> EnumToSelectListValue(this ESchematics request)
        {
            string[] itemValor = (string[])ESchematics.GetValues(typeof(ESchematics));
            return itemValor;
        }
    }
}
