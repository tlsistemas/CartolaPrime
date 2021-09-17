using CartoPrime.Application.Parameters;
using CartoPrime.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TM.Utils.Http.Response;

namespace CartoPrime.Application.Interfaces
{
    public interface IPushApplication
    {
        Task<BaseResponse<IEnumerable<PushResponse>>> ListPushAsync(PushParams paran);
        Task<BaseResponse<PushResponse>> Create(PushRequest paranObj);
        Task<BaseResponse<Boolean>> Remove(string key);
    }
}