using CartoPrime.Helpers;
using CartoPrime.Http.Interfaces;
using CartoPrime.Models;
using CartoPrime.Models.Atletas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CartoPrime.Http
{
    public class TeamService : ITeamService
    {
        IApiService api;
        public TeamService()
        {
            api = DependencyService.Get<IApiService>();
        }

        public async Task<List<TeamCA>> GetTeamsCAAsync(string key)
        {
            try
            {
                string json = await api.Get(string.Concat(UrlCartola._timeNome + "[", key, "]"));
                var dados_jogadores = JsonConvert.DeserializeObject<List<TeamCA>>(json);
                var lst = await App.Database.GetTeamCAsAsync();

                for (int i = 0; i < dados_jogadores.Count; i++)
                {
                    if (lst.Exists(x => x.time_id == dados_jogadores[i].time_id))
                    {
                        dados_jogadores[i].item_enable = false;
                        dados_jogadores[i].image_add = "ic_added_green.png";
                        //dados_jogadores.RemoveAt(i);
                    }
                    else
                    {
                        dados_jogadores[i].item_enable = true;
                        dados_jogadores[i].image_add = "ic_add_white.png";
                        if (dados_jogadores[i].assinante)
                            dados_jogadores[i].assinanteCaminho = "selo_pro.png";
                    }
                }
                return dados_jogadores;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public async Task<int> InsetTeamsFullCAAsync(TeamCA root, List<Clube> clubes)
        {
            try
            {
                string json = await api.Get(string.Concat(UrlCartola._timeId, root.time_id) ?? "");
                var item = JsonConvert.DeserializeObject<TeamSlug>(json);
                root.temporada_inicial = item.time.temporada_inicial;
                root.rodada_time_id = item.time.rodada_time_id;
                root.clube_id = item.time.clube_id;
                root.clube_string = clubes.Find(x => x.id == root.clube_id)._30x30;
                root.pontos = "0.00";
                root.patrimonio = item.patrimonio;
                root.mensagem = item.mensagem;
                root.rodada_atual = item.rodada_atual;
                //root.variacao_pontos = item.variacao_pontos;
                root.variacao_patrimonio_count = item.variacao_patrimonio;
                root.capitao_id = item.capitao_id;
                root.esquema_id = item.time.esquema_id;

                var team = item.time;

                var i = await App.Database.InsertTeamCAAsync(root);
                var o = await App.Database.InsertTeamCAAsync(team);
                return 0;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UNIQUE"))
                    return 2;
                return 1;
            }
        }

        public async Task<TeamSlug> GetTeamsCAIdAsync(int idTeam)
        {
            try
            {
                string json = await api.Get(string.Concat(UrlCartola._timeId, idTeam) ?? "");
                var item = JsonConvert.DeserializeObject<TeamSlug>(json);
                return item;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Substituicoes>> GetSubtituicoesIdAsync(int idTeam)
        {
            try
            {
                string json = await api.Get(string.Concat(UrlCartola._timeSubstituicao, idTeam) ?? "");
                var item = JsonConvert.DeserializeObject<List<Substituicoes>>(json);
                return item;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<TeamSlug> GetTeamsCAIdAsync(int idTeam, int roud)
        {
            try
            {
                string json = await api.Get(string.Concat(UrlCartola._timeId, idTeam+ "/"+roud) ?? "");
                var item = JsonConvert.DeserializeObject<TeamSlug>(json);
                return item;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<AtletaTime> GetMyTeamsAsync(string glbtoken)
        {
            try
            {
                string json = await api.Get(string.Concat(UrlCartola._myTime), glbtoken);
                var item = JsonConvert.DeserializeObject<AtletaTime>(json);
                return item;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<TeamCA>> GetTeamsCAAsync()
        {
            try
            {
                var dados_jogadores = await App.Database.GetTeamCAsAsync();
                for (int i = 0; i < dados_jogadores.Count; i++)
                {
                    if(dados_jogadores[i].time_id == 13924668)
                    dados_jogadores[i].temporada_inicial = 2012;
                }
                return dados_jogadores;

            }
            catch (Exception ex)
            {
                return new List<TeamCA>();
            }



        }
    }
}
