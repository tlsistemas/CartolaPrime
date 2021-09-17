using System;
using System.Collections.Generic;
using System.Text;

namespace CartoPrime.Models
{
    public class Positions
    {
        public static List<string> GetAllPosition()
        {
            List<string> lstRetorno = new List<string>();

            lstRetorno.Add("Goleiro");
            lstRetorno.Add("Lateral");
            lstRetorno.Add("Zagueiro");
            lstRetorno.Add("Meia");
            lstRetorno.Add("Atacante");
            lstRetorno.Add("Técnico");
            lstRetorno.Add("Todas Posições");

            return lstRetorno;
        }

        public static string GetPosition(int idPosicao)
        {
            switch (idPosicao)
            {
                case 1:
                    {
                        return "Goleiro";
                    }
                case 2:
                    {
                        return "Lateral";
                    }
                case 3:
                    {
                        return "Zagueiro";
                    }
                case 4:
                    {
                        return "Meia";
                    }
                case 5:
                    {
                        return "Atacante";
                    }
                case 6:
                    {
                        return "Técnico";
                    }
            }
            return "";
        }
        public static string GetPositionMin(int idPosicao)
        {
            switch (idPosicao)
            {
                case 1:
                    {
                        return "GOL";
                    }
                case 2:
                    {
                        return "LAT";
                    }
                case 3:
                    {
                        return "ZAG";
                    }
                case 4:
                    {
                        return "MEI";
                    }
                case 5:
                    {
                        return "ATA";
                    }
                case 6:
                    {
                        return "TEC";
                    }
            }
            return "";
        }
        public static int GetPosition(string Posicao)
        {
            switch (Posicao)
            {
                case "Goleiro":
                    {
                        return 1;
                    }
                case "Lateral":
                    {
                        return 2;
                    }
                case "Zagueiro":
                    {
                        return 3;
                    }
                case "Meia":
                    {
                        return 4;
                    }
                case "Atacante":
                    {
                        return 5;
                    }
                case "Técnico":
                    {
                        return 6;
                    }
            }
            return 0;
        }
    }
}
