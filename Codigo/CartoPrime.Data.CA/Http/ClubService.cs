using CartoPrime.Data.CA.Helpers;
using CartoPrime.Data.CA.Http.Interfaces;
using CartoPrime.Data.CA.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Utils.Http.Response;

namespace CartoPrime.Data.CA.Http
{
    public class ClubService : IClubService
    {
        ApiService api;
        public ClubService()
        {
            api = new ApiService();
        }
        public async Task<List<Clube>> ListClubs()
        {
            var json = api.Get(UrlCartola._clubes).Result;
            var list = new List<Clube>();
            try
            {
                JObject jObject = JObject.Parse(json.Replace("\"", "'"));

                foreach (JToken jToken in jObject.Children())
                {
                    if (!jToken.HasValues)
                    {
                        continue;
                    }
                    Clube str = JsonConvert.DeserializeObject<Clube>(jToken.First.ToString());
                    foreach (JToken jToken1 in JObject.Parse(str.escudos.ToString()).Children())
                    {
                        if (jToken1.ToString().Length <= 70)
                        {
                            continue;
                        }
                        str._30x30 = jToken1.First.ToString();
                        goto Label0;
                    }
                Label0:
                    list.Add(str);
                }
            }
            catch (Exception ex)
            {
                return list;

            }

            return list;
        }

        public async Task<List<Partida>> GetConfrontos(string rodada = null)
        {
            var response = new BaseResponse<Rodada>();
            var partidas = new List<Partida>();

            response = await api.Get<Rodada>(UrlCartola._partidas + "/" + rodada);
            partidas = response.Data.Partidas.OrderBy(x => x.partida_data).ToList();


            var clubes = await ListClubs();
            for (int i = 0; i < partidas.Count; i++)
            {
                var clube_mand = clubes.Find(x => x.id == partidas[i].clube_casa_id);
                var clube_visit = clubes.Find(x => x.id == partidas[i].clube_visitante_id);
                partidas[i].clube_casa_escudo = clube_mand._30x30;
                partidas[i].clube_visitante_escudo = clube_visit._30x30;
                partidas[i].clube_casa_nome = clube_mand.nome;
                partidas[i].clube_visitante_nome = clube_visit.nome;
                partidas[i].local_confronto = (string.Concat(partidas[i].local, " - ", string.Format("{0:f}", partidas[i].partida_data)));
            }

            return partidas;
        }
    }
}
