using CartoPrime.Helpers;
using CartoPrime.Http.Interfaces;
using CartoPrime.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CartoPrime.Http
{
    public class NewsService : INewsService
    {
        IApiService api;
        public NewsService()
        {
            api = DependencyService.Get<IApiService>();
        }

        public async Task<List<News>> GetNewsAsync()
        {
            try
            {
                string json = await api.Get(UrlCartola._noticias);
                var dados_noticias = JsonConvert.DeserializeObject<List<News>>(json);

                for (int i = 0; i < dados_noticias.Count; i++)
                {
                    dados_noticias[i].thumbnail = "https:" + dados_noticias[i].thumbnail;
                }
                return dados_noticias;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}
