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
    public class BonsDeBicoController : BaseController
    {
        private readonly ILogger<TokenController> _logger;
        private readonly IBonsDeBicoApplication _pageApplication;

        public BonsDeBicoController(
            ILogger<TokenController> logger,
            IBonsDeBicoApplication PageApplication)
        {
            this._logger = logger;
            this._pageApplication = PageApplication;
        }

        /// <summary>
        /// Listar BonsDeBico
        /// </summary>
        /// <response code="200">BonsDeBicoes.</response>
        /// <response code="400">
        /// </response>
        [HttpGet]
        [ProducesResponseType(typeof(BaseResponse<MatchBonsDeBico>), 200)]
        [ProducesResponseType(typeof(BaseResponse<bool>), 400)]
        [ProducesResponseType(401)]
        [SwaggerOperation(OperationId = "BonsDeBicoAsync")]
        [AllowAnonymous]
        public async Task<IActionResult> BonsDeBicoAsync()
        {
            try
            {
                var result = await _pageApplication.ListMatchs();
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

        /// <summary>
        /// Listar BonsDeBico com Atletas
        /// </summary>
        /// <response code="200">BonsDeBicoesAtletas.</response>
        /// <response code="400">
        /// </response>
        [HttpGet]
        [ProducesResponseType(typeof(BaseResponse<MatchBonsDeBico>), 200)]
        [ProducesResponseType(typeof(BaseResponse<bool>), 400)]
        [ProducesResponseType(401)]
        [SwaggerOperation(OperationId = "BonsDeBicoAsync")]
        [Route("BonsDeBicoAtletas")]
        [AllowAnonymous]
        public async Task<IActionResult> BonsDeBicoAtletasAsync(int idA, int idB)
        {
            try
            {
                var result = await _pageApplication.ListMatchs(idA, idB);
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