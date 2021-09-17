using CartoPrime.Helpers;
using CartoPrime.Http.Interfaces;
using CartoPrime.Models;
using CartoPrime.Models.BonsBico;
using MonkeyCache.FileStore;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CartoPrime.Http
{
    public class BonsDeBicoService : IBonsDeBicoService
    {
        IApiService api;
        public BonsDeBicoService()
        {
            api = DependencyService.Get<IApiService>();
        }

        public async Task<List<BonsDeBico>> ListConfrontos(bool force = false)
        {
            var json = api.Get(UrlService._bb_jogosSerieA).Result;
            var dados_jogos = JsonConvert.DeserializeObject<BaseResponse<List<BonsDeBico>>>(json);
            return dados_jogos.Data;
            //if (force)
            //{
            //    var json = api.Get(UrlService._bb_jogosSerieA).Result;
            //    var dados_jogos = JsonConvert.DeserializeObject<BaseResponse<List<BonsDeBico>>>(json);

            //    if (dados_jogos.Data != null)
            //    {
            //        Barrel.Current.Add(
            //                key: UrlService._bb_jogosSerieA,
            //                data: dados_jogos.Data,
            //                expireIn: TimeSpan.FromMinutes(10));
            //        return dados_jogos.Data;
            //    }
            //    else
            //        return Barrel.Current.Get<List<BonsDeBico>>(key: UrlService._bb_jogosSerieA);
            //}
            //var conect = CrossConnectivity.Current.IsConnected;
            //var expired = Barrel.Current.IsExpired(key: UrlService._bb_jogosSerieA);
            //if (conect && !expired)
            //    return Barrel.Current.Get<List<BonsDeBico>>(key: UrlService._bb_jogosSerieA);
            //else if (conect)
            //{
            //    var json = api.Get(UrlService._bb_jogosSerieA).Result;
            //    var dados_jogos = JsonConvert.DeserializeObject<BaseResponse<List<BonsDeBico>>>(json);

            //    if (dados_jogos.Data != null)
            //    {
            //        Barrel.Current.Add(
            //                key: UrlService._bb_jogosSerieA,
            //                data: dados_jogos.Data,
            //                expireIn: TimeSpan.FromMinutes(10));
            //        return dados_jogos.Data;
            //    }
            //    else
            //        return Barrel.Current.Get<List<BonsDeBico>>(key: UrlService._bb_jogosSerieA);
            //}
            //else
            //{
            //    return Barrel.Current.Get<List<BonsDeBico>>(key: UrlService._bb_jogosSerieA);
            //}
        }
        public async Task<List<BonsDeBico>> ListConfrontosB(bool force = false)
        {
            var json = api.Get(UrlService._bb_jogosSerieB).Result;
            var dados_jogos = JsonConvert.DeserializeObject<BaseResponse<List<BonsDeBico>>>(json);
            return dados_jogos.Data;

            //if (force)
            //{
            //    var json = api.Get(UrlService._bb_jogosSerieB).Result;
            //    var dados_jogos = JsonConvert.DeserializeObject<BaseResponse<List<BonsDeBico>>>(json);

            //    if (dados_jogos.Data != null)
            //    {
            //        Barrel.Current.Add(
            //                key: UrlService._bb_jogosSerieB,
            //                data: dados_jogos.Data,
            //                expireIn: TimeSpan.FromMinutes(10));
            //        return dados_jogos.Data;
            //    }
            //    else
            //        return Barrel.Current.Get<List<BonsDeBico>>(key: UrlService._bb_jogosSerieB);
            //}
            //var conect = CrossConnectivity.Current.IsConnected;
            //var expired = Barrel.Current.IsExpired(key: UrlService._bb_jogosSerieB);
            //if (conect && !expired)
            //    return Barrel.Current.Get<List<BonsDeBico>>(key: UrlService._bb_jogosSerieB);
            //else if (conect)
            //{
            //    var json = api.Get(UrlService._bb_jogosSerieB).Result;
            //    var dados_jogos = JsonConvert.DeserializeObject<BaseResponse<List<BonsDeBico>>>(json);

            //    if (dados_jogos.Data != null)
            //    {
            //        Barrel.Current.Add(
            //                key: UrlService._bb_jogosSerieB,
            //                data: dados_jogos.Data,
            //                expireIn: TimeSpan.FromMinutes(10));
            //        return dados_jogos.Data;
            //    }
            //    else
            //        return Barrel.Current.Get<List<BonsDeBico>>(key: UrlService._bb_jogosSerieB);
            //}
            //else
            //{
            //    return Barrel.Current.Get<List<BonsDeBico>>(key: UrlService._bb_jogosSerieB);
            //}
        }

        public async Task<BonsDeBico> GetConfrontosAtletas(int idA, int idB)
        {
            try
            {
                var json = api.Get($"{UrlService._bb_jogosAtletas}?idA={idA}&idB={idB}").Result;
                var dados_jogos = JsonConvert.DeserializeObject<BaseResponse<BonsDeBico>>(json);
                if (!dados_jogos.Error && dados_jogos.Data != null)
                    return dados_jogos.Data;
                else
                    return new BonsDeBico();
            }
            catch (Exception)
            {
                return new BonsDeBico();
            }


        }
    }
}
