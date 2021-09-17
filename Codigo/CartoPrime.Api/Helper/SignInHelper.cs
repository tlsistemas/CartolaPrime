using CartoPrime.Application.ViewModel;
using CartoPrime.Application.ViewModel.User.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TM.Utils.Authentication;
using TM.Utils.Http.Response;

namespace CartoPrime.Api.Helper
{
    public static class SignInHelper
    {
        public static UserToken GenerateAccessToken(BaseResponse<UserResponse> userResponse, JwtAuthenticationOptions jwtAuthenticationConfiguration)
        {
            if (userResponse != null && !userResponse.Error)
            {
                var claims = new[]
                {
                    //new Claim("Id", userResponse.Data.Id.ToString()),
                    new Claim("Key", userResponse.Data.Key),
                    new Claim("SessionId", Guid.NewGuid().ToString())
                };

                System.IdentityModel.Tokens.Jwt.JwtSecurityToken token = TokenFactory.GenerateJwtToken(claims, jwtAuthenticationConfiguration);

                return new UserToken
                {
                    AccessToken = TokenFactory.WriteToken(token),
                    Expires = token.ValidTo,
                    Issued = token.ValidFrom,
                    TokenType = "Bearer",
                    ExpiresIn = (long)TimeSpan.FromMinutes(jwtAuthenticationConfiguration.ExpirationMinutes).TotalSeconds,
                    Key = userResponse.Data.Key,
                    UserName = userResponse.Data.UserName
                };
            }
            else
                return null;
        }
    }
}
