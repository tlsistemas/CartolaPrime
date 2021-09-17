using CartoPrime.Models;
using CartoPrime.Models.Atletas;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CartoPrime.Http.Interfaces
{
    public interface ITeamService
    {
        Task<List<TeamCA>> GetTeamsCAAsync(string key);
        Task<int> InsetTeamsFullCAAsync(TeamCA root, List<Clube> clubes);
        Task<List<TeamCA>> GetTeamsCAAsync();
        Task<TeamSlug> GetTeamsCAIdAsync(int idTeam);
        Task<TeamSlug> GetTeamsCAIdAsync(int idTeam, int roud);
        Task<AtletaTime> GetMyTeamsAsync(string glbtoken);
        Task<List<Substituicoes>> GetSubtituicoesIdAsync(int idTeam);
    }
}
