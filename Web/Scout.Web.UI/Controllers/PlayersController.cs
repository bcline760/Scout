using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Scout.Core.Contract;
using Scout.Model.Contract;
using Scout.Service.Contract;

namespace Scout.Web.UI.Controllers
{
    [Route("api/[controller]")]
    public class PlayersController : BaseController
    {
        private IPlayerService _service = null;
        public PlayersController(IPlayerService service)
        {
            _service = service;
        }

        [HttpGet("")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<List<Player>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {

        }

        // GET api/values/5
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<Player>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(int id)
        {
            PlayerSearchRequest request = new PlayerSearchRequest
            {
                PlayerId = id
            };

            return await FindPlayers(request);
        }

        [HttpGet]
        [Route("withcode/{code:minlength(1)}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<Player>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByCode(string code)
        {
            PlayerSearchRequest request = new PlayerSearchRequest
            {
                PlayerCode = code
            };

            return await FindPlayers(request);
        }

        // POST api/values
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<ObjectModifyResult<int>>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreatePlayer([FromBody]Player value)
        {
            return await ExecuteServiceMethod(_service.CreatePlayer, value, nameof(CreatePlayer), Core.ApiStatusCode.Created);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody]Player value)
        {
            return await ExecuteServiceMethod(
                _service.UpdatePlayer,
                value,
                nameof(Put),
                Core.ApiStatusCode.Created
            );
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<int>), (int)HttpStatusCode.Created)]
        public void Delete(int id)
        {
        }

        private async Task<IActionResult> FindPlayers(PlayerSearchRequest request)
        {
            ApiResponse<Player> response = new ApiResponse<Player>
            {
                Result = Core.OperationResult.Unknown
            };
            try
            {
                var players = await _service.FindPlayers(request);
                response.Result = Core.OperationResult.Success;
                if (players.Count > 0)
                {
                    response.ResponseBody = players.First();
                    return Ok(response);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                response.Result = Core.OperationResult.Failure;
                response.Message = "Unable to retrieve the records";
                return BadRequest(response);
            }
        }
    }
}
