using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TM.Utils.Http.Response;

namespace CartoPrime.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BaseController : ControllerBase
    {
        public IActionResult BaseResponse<T>(BaseResponse<T> response)
        {
            return StatusCode((int)response.GetStatusCode(), response);
        }
    }
}
