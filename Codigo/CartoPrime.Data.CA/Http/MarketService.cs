using CartoPrime.Data.CA.Helpers;
using CartoPrime.Data.CA.Http.Interfaces;
using CartoPrime.Data.CA.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CartoPrime.Data.CA.Http
{
    public class MarketService : IMarketService
    {
        ApiService api;
        public MarketService()
        {
            api = new ApiService();
        }
        public async Task<Market> Information()
        {
            try
            {

                var json = await api.Get(UrlCartola._mercadoStatus);
                var saida = JsonConvert.DeserializeObject<Market>(json);


                return saida;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Market> InformationInitial()
        {
            try
            {
                var json = await api.Get(UrlCartola._mercadoStatus);
                var saida = JsonConvert.DeserializeObject<Market>(json);

                return saida;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<AthleteMarket>> AthletesMarket()
        {
            try
            {
                string json = await api.Get(UrlCartola._mercadoAtle);
                var athletes = JsonConvert.DeserializeObject<BaseAthleteMarket>(json).atletas;

                for (int i = 0; i < athletes.Count; i++)
                {
                    //athletes[i].preco_double = double.Parse(athletes[i].preco_num);
                    //athletes[i].variacao_double = double.Parse(athletes[i].variacao_num);
                    //athletes[i].pontos_double = double.Parse(athletes[i].pontos_num.Replace(".",","));
                    #region scout Ataque
                    if (athletes[i].scout != null)
                    {
                        if (athletes[i].scout.G > 0)
                        {
                            athletes[i].scout_tela += athletes[i].scout.G + "G ";
                        }
                        if (athletes[i].scout.A > 0)
                        {
                            athletes[i].scout_tela += athletes[i].scout.A + "A ";
                        }
                        if (athletes[i].scout.FT > 0)
                        {
                            athletes[i].scout_tela += athletes[i].scout.FT + "FT ";
                        }
                        if (athletes[i].scout.FD > 0)
                        {
                            athletes[i].scout_tela += athletes[i].scout.FD + "FD ";
                        }
                        if (athletes[i].scout.FF > 0)
                        {
                            athletes[i].scout_tela += athletes[i].scout.FF + "FF ";
                        }
                        if (athletes[i].scout.FS > 0)
                        {
                            athletes[i].scout_tela += athletes[i].scout.FS + "FS ";
                        }


                        if (athletes[i].scout.RB > 0)
                        {
                            athletes[i].scout_tela += athletes[i].scout.RB + "RB ";
                        }
                        if (athletes[i].scout.SG > 0)
                        {
                            athletes[i].scout_tela += "SG ";
                        }
                        if (athletes[i].scout.DD > 0)
                        {
                            athletes[i].scout_tela += athletes[i].scout.DD + "DD ";
                        }
                        if (athletes[i].scout.DP > 0)
                        {
                            athletes[i].scout_tela += athletes[i].scout.DP + "DP ";
                        }
                        if (athletes[i].scout.PI > 0)
                        {
                            athletes[i].scout_tela += athletes[i].scout.PI + "PI ";
                        }
                        #endregion

                        #region scout Defesa

                        if (athletes[i].scout.FC > 0)
                        {
                            athletes[i].scout_tela_def += athletes[i].scout.FC + "FC ";
                        }
                        if (athletes[i].scout.GC > 0)
                        {
                            athletes[i].scout_tela_def += athletes[i].scout.GC + "GC ";
                        }
                        if (athletes[i].scout.CA > 0)
                        {
                            athletes[i].scout_tela_def += athletes[i].scout.CA + "CA ";
                        }
                        if (athletes[i].scout.CV > 0)
                        {
                            athletes[i].scout_tela_def += athletes[i].scout.CV + "CV ";
                        }
                        if (athletes[i].scout.GS > 0)
                        {
                            athletes[i].scout_tela_def += athletes[i].scout.GS + "GS";
                        }

                        if (athletes[i].scout.PE > 0)
                        {
                            athletes[i].scout_tela_def += athletes[i].scout.PE + "PE ";
                        }
                        if (athletes[i].scout.I > 0)
                        {
                            athletes[i].scout_tela_def += athletes[i].scout.I + "I ";
                        }
                        if (athletes[i].scout.PP > 0)
                        {
                            athletes[i].scout_tela_def += athletes[i].scout.PP + "PP ";
                        }
                        if (athletes[i].scout.DS > 0)
                        {
                            athletes[i].scout_tela_def += athletes[i].scout.DS + "DS ";
                        }
                    }
                    else
                    {
                        athletes[i].scout = new Scout();
                        athletes[i].msg_scout = "O Cartola FC não está disponibilizando os scouts nesse momento. Obrigado!";
                    }

                    #endregion
                }


                return athletes;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
