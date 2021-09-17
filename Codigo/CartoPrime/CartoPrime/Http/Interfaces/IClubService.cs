using CartoPrime.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CartoPrime.Http.Interfaces
{
    public interface IClubService
    {
        Task<List<Clube>> ListClubs();
        Task<List<Partida>> GetConfrontos(string rodada = null);
    }
}
