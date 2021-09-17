using CartoPrime.Application.Parameters;
using CartoPrime.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TM.Utils.Http.Response;

namespace CartoPrime.Application.Interfaces
{
    public interface IUserApplication
    {
        Task<BaseResponse<UserResponse>> AuthenticateAsync(UserTokenRequest loginUser);
        Task<BaseResponse<IEnumerable<UserResponse>>> ListUserAsync(UserParams paran);
        Task<BaseResponse<UserResponse>> Create(UserResponse paranObj);
        Task<BaseResponse<Boolean>> Remove(int key);
    }
}
