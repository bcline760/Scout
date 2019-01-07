using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Scout.Core.Contract;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Scout.Web.Api
{
    [Route("api/[controller]")]
    public class ScoutPingController : ScoutApiController
    {
        // GET: api/values
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.OK)]
        public IActionResult Get()
        {
            var response = new ApiResponse<string>
            {
                Message = "API OK",
                Result = Core.OperationResult.Success
            };

            return Ok(response);
        }
    }
}
