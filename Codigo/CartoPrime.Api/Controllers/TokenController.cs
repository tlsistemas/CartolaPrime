using System;
using System.Threading.Tasks;
using CartoPrime.Api.Helper;
using CartoPrime.Application.Interfaces;
using CartoPrime.Application.ViewModel;
using CartoPrime.Application.ViewModel.User.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;
using TM.Utils.Authentication;
using TM.Utils.Http.Response;

namespace CartoPrime.Api.Controllers
{
    [Route("")]
    public class TokenController : BaseController
    {
        public IConfiguration Configuration { get; }
        private readonly ILogger<TokenController> _logger;
        private readonly JwtAuthenticationOptions _jwtAuthentication;
        private readonly IUserApplication _userApplication;

        public TokenController(
            ILogger<TokenController> logger,
            IOptions<JwtAuthenticationOptions> jwtAuthenticationConfiguration,
            IUserApplication userApplication, IConfiguration configuration)
        {
            this.Configuration = configuration;
            this._logger = logger;
            this._jwtAuthentication = jwtAuthenticationConfiguration.Value ?? throw new ArgumentNullException(nameof(jwtAuthenticationConfiguration));
            this._userApplication = userApplication;
        }

        [Route("/")]
        [HttpGet]
        [AllowAnonymous]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Swagger()
        {
            return Redirect("/swagger");
        }

        /// <summary>
        /// Generate Access Token
        /// </summary>
        /// <response code="200">Retorna token do usuário válido por 2 Horas.</response>
        /// <response code="422">
        /// Retorna os erros encontrados ao obter o token.
        /// </response>
        [HttpPost]
        [Route("api/token")]
        [ProducesResponseType(typeof(UserToken), 200)]
        [ProducesResponseType(typeof(BaseResponse<bool>), 400)]
        [SwaggerOperation(OperationId = "Token")]
        [AllowAnonymous]
        // Todo: Colocar Recaptcha
        public async Task<IActionResult> Token([FromBody] UserTokenRequest userLogin)
        {
            try
            {
                var result = await _userApplication.AuthenticateAsync(userLogin);

                if (result.Success && result.Data != null)
                {
                    var userToken = SignInHelper.GenerateAccessToken(result, _jwtAuthentication);
                    var response = new BaseResponse<Object>
                    {
                        Data = userToken,
                        Success = true,
                        Error = false,
                    };

                    return new OkObjectResult(response);
                }
                else
                {
                    result.Error = true;
                    result.Messages.Add(new Error("Erro ao gerar token."));
                    return BadRequest(result);
                }

            }
            catch (Exception ex)
            {
                var result = new BaseResponse<Object>
                {
                    Data = null,
                    Success = false,
                    Error = true,
                };
                result.AddError(ex.Message);
                return BadRequest(result);
            }
        }
    }
}
