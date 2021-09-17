using CartoPrime.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CartoPrime.Http.Interfaces
{
    public interface IPartialService
    {
        Task<List<TeamCA>> GetMarketClose();
        Task<List<TeamCA>> GetMarketOpen(int round);
        Task<BaseAthleteCA> GetAthleteMarketOpen(int timeId, int round);
        Task<BaseAthleteCA> GetAthleteMarketClose(int timeId);
        Task<List<Time>> GetLeagueMarketClose(List<Time> TeamsUp);
    }
}
