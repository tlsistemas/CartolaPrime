using CartoPrime.Application.Http;
using CartoPrime.Domain.EntitiesCartola.Team;
using CartoPrime.Domain.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace CartoPrime.Pontuacoes
{
    class Program
    {
        static IApiService api;
        static int tableWidth = 73;
        static async Task Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
            .AddSingleton<IApiService, ApiService>()
            .BuildServiceProvider();

            api = serviceProvider.GetService<IApiService>();
            var ids = GetIds();
            foreach (var item in ids)
            {
                if (item != "")
                {
                    var team = await GetTeamIdAsync(int.Parse(item)).ConfigureAwait(true);
                    //PrintLine();
                    //Console.WriteLine(team.time.nome);
                    PrintRow(team.time.time_id.ToString(), team.time.nome, team.pontos_rodada);
                    //PrintLine();
                    //string saida = $"{team.time.time_id};{team.time.nome};{team.pontos_rodada}";
                    //Console.WriteLine(saida);
                }
            }

            Console.ReadLine();
        }

        private static async Task<TeamModel> GetTeamIdAsync(int idTeam)
        {
            try
            {
                string json = await api.Get(string.Concat(UrlCartola._timeSlug, idTeam) ?? "");
                var item = JsonConvert.DeserializeObject<TeamModel>(json);
                return item;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static string[] GetIds()
        {
            string ids = @"13924668;364309;942719;961332;19435336;4926245;3299511;4953515;16704764;3348591;5781967;12255141;644367;1484400;397575;12443034;747529;7355350;2774316;18234957;28824572;2760725;5541287;28092158;25554711;6732944;6019592;18010824;13964479;13972492;18193661;9370032;12193947;6114102;7946578;199787;20463017;22031247;";

            return ids.Split(';');

        }

        static void PrintLine()
        {
            Console.WriteLine(new string('-', tableWidth));
        }

        static void PrintRow(params string[] columns)
        {
            int width = (tableWidth - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            Console.WriteLine(row);
        }

        static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }
    }
}
