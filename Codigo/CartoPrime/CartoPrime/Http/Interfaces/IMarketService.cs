using CartoPrime.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CartoPrime.Http.Interfaces
{
    public interface IMarketService
    {
        Task<Market> Information();
        Task<Market> InformationInitial();
        Task<List<AthleteMarket>> AthletesMarket();
    }
}
