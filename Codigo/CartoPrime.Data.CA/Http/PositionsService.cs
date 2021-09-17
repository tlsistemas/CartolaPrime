using CartoPrime.Data.CA.Http.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CartoPrime.Data.CA.Http
{
    public class PositionsService : IPositionsService
    {
        public PositionsService() { }

        public async Task<List<string>> GetAll()
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

        public async Task<string> Get(int id)
        {
            switch (id)
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
        public async Task<int> Get(string position)
        {
            switch (position)
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
