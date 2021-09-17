using CartoPrime.Helpers;
using CartoPrime.Http.Interfaces;
using MonkeyCache.FileStore;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using CartoPrime.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;

namespace CartoPrime.Http
{
    public class LeaguesService : ILeaguesService
    {
        IApiService api;
        public LeaguesService()
        {
            api = DependencyService.Get<IApiService>();
        }

        public async Task<List<League>> GetLeaguesUserCAAsync()
        {
            try
            {
                var UserCA = Barrel.Current.Get<UserCA>(key: UrlCartola._timeAuth);

                string json = await api.Get(UrlCartola._ligaLogado, UserCA.glbstring);
                json = json.Replace("D'Alessandro", "D.Alessandro");
                var lst = JsonConvert.DeserializeObject<BaseLeague>(json);

                return lst.ligas.OrderByDescending(x => x.liga_id).ToList();
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<BaseTeamLeague> GetTeamsLeaguesAsync(string slug_league, int page = 1)
        {
            try
            {
                var body = $"?page={page}&orderBy=rodada";
                var UserCA = Barrel.Current.Get<UserCA>(key: UrlCartola._timeAuth);

                string json = await api.Get(string.Concat(UrlCartola._ligaSlug, slug_league, body), UserCA.glbstring);
                json = json.Replace("D'Alessandro", "D.Alessandro");
                var ligaTimes = JsonConvert.DeserializeObject<BaseTeamLeague>(json);

                var dados_jogadores = ligaTimes;
                return dados_jogadores;

            }
            catch (Exception ex)
            {
                return null;
            }



        }
    }
}
