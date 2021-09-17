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
    public class TeamService : ITeamService
    {
        ApiService api;
        public TeamService()
        {
            api = new ApiService();
        }

        public async Task<List<TeamCA>> GetTeamsCAAsync(string key)
        {
            try
            {
                string json = await api.Get(string.Concat(UrlCartola._timeNome + "[", key, "]"));
                var dados_jogadores = JsonConvert.DeserializeObject<List<TeamCA>>(json);
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
                string json = await api.Get(string.Concat(UrlCartola._timeSlug, root.time_id) ?? "");
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

                //var i = await App.Database.InsertTeamCAAsync(root);
                //var o = await App.Database.InsertTeamCAAsync(team);
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
                string json = await api.Get(string.Concat(UrlCartola._timeSlug, idTeam) ?? "");
                var item = JsonConvert.DeserializeObject<TeamSlug>(json);
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
                string json = await api.Get(string.Concat(UrlCartola._timeSlug, idTeam + "/" + roud) ?? "");
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
    }
}
