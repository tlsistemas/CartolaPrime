using CartoPrime.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TM.Utils.Http.Response;

namespace CartoPrime.Application.Interfaces
{
    public interface IBonsDeBicoApplication
    {
        Task<BaseResponse<IEnumerable<MatchBonsDeBico>>> ListMatchs();
        Task<BaseResponse<IEnumerable<MatchBonsDeBico>>> ListMatchsB();
        Task<BaseResponse<MatchBonsDeBico>> ListMatchs(int idA, int idB);
    }
}
