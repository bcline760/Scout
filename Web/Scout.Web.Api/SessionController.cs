using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scout.Core.Contract;
using Scout.Core.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Scout.Web.Api
{
    [Route("api/[controller]")]
    public class SessionController : ScoutApiController
    {
        private IAccountService _service;
        public SessionController(IAccountService service)
        {
            _service = service;
        }

        [HttpGet, Route("health"), AllowAnonymous]
        public IActionResult HealthCheck()
        {
            return Ok("Good");
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> AuthenticateAsync([FromBody]AccountAuthenticate credentials)
        {
            var authResult = await _service.AuthenticateAsync(credentials);

            ApiResponse<string> response = new ApiResponse<string>();
            IActionResult actionResult = null;
            if (!string.IsNullOrEmpty(authResult.Token))
            {
                response.Result = Core.OperationResult.Success;
                response.ResponseBody = authResult.Token;
                actionResult = Ok(response);
            }
            else
            {
                response.Result = Core.OperationResult.Error;
                response.Message = authResult.AuthenticationMessage;
                actionResult = StatusCode((int)HttpStatusCode.Unauthorized, response);
            }

            return actionResult;
        }

        [HttpPost, Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync(AccountRegister register)
        {
            var regResult = await _service.RegisterAsync(register);

            ApiResponse<string> response = new ApiResponse<string>();
            IActionResult actionResult = null;
            if (!string.IsNullOrEmpty(regResult.Token))
            {
                response.Result = Core.OperationResult.Success;
                response.ResponseBody = regResult.Token;
                actionResult = Ok(response);
            }
            else
            {
                response.Result = Core.OperationResult.Error;
                response.Message = regResult.AuthenticationMessage;
                actionResult = StatusCode((int)HttpStatusCode.Unauthorized, response);
            }

            return actionResult;
        }
    }
}
