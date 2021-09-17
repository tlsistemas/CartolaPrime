using CartoPrime.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CartoPrime.Http.Interfaces
{
    public interface ILeaguesService
    {
        Task<List<League>> GetLeaguesUserCAAsync();
        Task<BaseTeamLeague> GetTeamsLeaguesAsync(string slug_league, int page = 1);
    }
}
