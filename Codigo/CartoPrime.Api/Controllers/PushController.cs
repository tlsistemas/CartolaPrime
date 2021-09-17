using CartoPrime.Application.Interfaces;
using CartoPrime.Application.Parameters;
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
    public class PushController : BaseController
    {
        private readonly ILogger<TokenController> _logger;
        private readonly IPushApplication _pageApplication;

        public PushController(
            ILogger<TokenController> logger,
            IPushApplication PageApplication)
        {
            this._logger = logger;
            this._pageApplication = PageApplication;
        }

        /// <summary>
        /// Listar Push
        /// </summary>
        /// <response code="200">Pushes.</response>
        /// <response code="400">
        /// </response>
        [HttpGet]
        [ProducesResponseType(typeof(BaseResponse<PushResponse>), 200)]
        [ProducesResponseType(typeof(BaseResponse<bool>), 400)]
        [ProducesResponseType(401)]
        [SwaggerOperation(OperationId = "PushAsync")]
        public async Task<IActionResult> PushAsync([FromQuery] PushParams Push)
        {
            try
            {
                var result = await _pageApplication.ListPushAsync(Push);
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
        /// Create Push
        /// </summary>
        /// <response code="200">Pushe criado com sucesso.</response>
        /// <response code="400">
        /// </response>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse<bool>), 200)]
        [ProducesResponseType(typeof(BaseResponse<bool>), 400)]
        [SwaggerOperation(OperationId = "PushRequest")]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] PushRequest create)
        {
            try
            {
                var result = await _pageApplication.Create(create);
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
        /// Remover Push
        /// </summary>
        /// <response code="200">Pushe removida com sucesso.</response>
        /// <response code="400">
        /// </response>
        [HttpDelete]
        [ProducesResponseType(typeof(BaseResponse<bool>), 200)]
        [ProducesResponseType(typeof(BaseResponse<bool>), 400)]
        [SwaggerOperation(OperationId = "PushRemove")]
        public async Task<IActionResult> Remove(String key)
        {
            try
            {
                var result = await _pageApplication.Remove(key);
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