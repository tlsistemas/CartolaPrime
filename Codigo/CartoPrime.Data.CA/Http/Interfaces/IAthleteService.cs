using CartoPrime.Data.CA.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CartoPrime.Data.CA.Http.Interfaces
{
    public interface IAthleteService
    {
        Task<List<BaseAthletePunctuated>> GetAthleteAsync();
        Task<List<BaseAthletePunctuated>> GetAthleteScoredAsync();
        Task<List<AthleteClimbed>> GetAthleteClimbedsync(bool force = false);
        Task<String> ScoreToValue(double preco_atual);
        Task<double> ScoreToValueDouble(double preco_atual);
    }
}
