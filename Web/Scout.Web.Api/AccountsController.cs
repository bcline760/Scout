using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scout.Core.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Scout.Web.Api
{
    [Route("api/[controller]")]
    [Authorize]
    public class AccountsController : ScoutApiController
    {
        private IAccountService _service;
        public AccountsController(IAccountService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var result = await ExecuteServiceMethod(_service.GetAsync, id, "Get", Core.ApiStatusCode.OK);

            return result;
        }
    }
}
