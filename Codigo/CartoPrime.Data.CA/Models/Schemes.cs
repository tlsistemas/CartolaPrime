using System;
using System.Collections.Generic;
using System.Text;

namespace CartoPrime.Data.CA.Models
{
    public class Schemes
    {
        public static string GetSchemes(int id)
        {
            switch (id)
            {
                case 1:
                    {
                        return "3-4-3";
                    }
                case 2:
                    {
                        return "3-5-2";
                    }
                case 3:
                    {
                        return "4-3-3";
                    }
                case 4:
                    {
                        return "4-4-2";
                    }
                case 5:
                    {
                        return "4-5-1";
                    }
                case 6:
                    {
                        return "5-3-2";
                    }
                case 7:
                    {
                        return "5-4-1";
                    }
            }
            return "";
        }
    }
}
