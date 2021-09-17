using CartoPrime.Helpers;
using CartoPrime.Http.Interfaces;
using CartoPrime.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CartoPrime.Http
{
    public class ClassificationBRAService : IClassificationBRAService
    {
        IApiService api;
        public ClassificationBRAService()
        {
            api = DependencyService.Get<IApiService>();
        }

        public async Task<ClassificationBRA> GetClassication(int year)
        {
            try
            {
                var json = api.Get(UrlCartola._classBra + $"{year}.json").Result;
                var said = JsonConvert.DeserializeObject<ClassificationBRA>(json);
                return said;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
