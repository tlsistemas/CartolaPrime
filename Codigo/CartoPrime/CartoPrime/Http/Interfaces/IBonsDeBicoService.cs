using CartoPrime.Models.BonsBico;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CartoPrime.Http.Interfaces
{
    public interface IBonsDeBicoService
    {
        Task<List<BonsDeBico>> ListConfrontos(bool force = false);
        Task<List<BonsDeBico>> ListConfrontosB(bool force = false);
        Task<BonsDeBico> GetConfrontosAtletas(int idA, int idB);
    }
}
