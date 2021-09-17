using CartoPrime.Application.Interfaces;
using CartoPrime.Application.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TM.Utils.Http.Response;

namespace CartoPrime.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BonsDeBicoBController : BaseController
    {
        private readonly ILogger<TokenController> _logger;
        private readonly IBonsDeBicoApplication _pageApplication;

        public BonsDeBicoBController(
            ILogger<TokenController> logger,
            IBonsDeBicoApplication PageApplication)
        {
            this._logger = logger;
            this._pageApplication = PageApplication;
        }

        /// <summary>
        /// Listar BonsDeBicoB
        /// </summary>
        /// <response code="200">BonsDeBicoesB.</response>
        /// <response code="400">
        /// </response>
        [HttpGet]
        [ProducesResponseType(typeof(BaseResponse<MatchBonsDeBico>), 200)]
        [ProducesResponseType(typeof(BaseResponse<bool>), 400)]
        [ProducesResponseType(401)]
        [SwaggerOperation(OperationId = "BonsDeBicoBAsync")]
        [AllowAnonymous]
        public async Task<IActionResult> BonsDeBicoBAsync()
        {
            try
            {
                var result = await _pageApplication.ListMatchsB();
                return new OkObjectResult(result);

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
