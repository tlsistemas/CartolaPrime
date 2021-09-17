using CartoPrime.Data.CA.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CartoPrime.Data.CA.Http.Interfaces
{
    public interface IClubService
    {
        Task<List<Clube>> ListClubs();
        Task<List<Partida>> GetConfrontos(string rodada = null);
    }
}
