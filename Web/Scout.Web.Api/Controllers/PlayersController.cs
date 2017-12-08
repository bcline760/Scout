using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Scout.Services.Player.API.Repository;
using System.Net;
using Scout.Services.Player.API.Model;

namespace Scout.Services.Player.API.Controllers
{
    [Route("api/[controller]")]
    public class PlayersController : Controller
    {
        private IPlayerRepository _repo = null;
        public PlayersController(IPlayerRepository repository)
        {
            _repo = repository;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Model.Player),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(int id)
        {
            var player = await _repo.GetPlayer(id);
            if (player != null)
                return Ok(player);

            return NotFound();
        }

        [HttpGet]
        [Route("withcode/{code:minlength(1)}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Model.Player), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByCode(string code)
        {
            var player = await _repo.GetPlayer(code);
            if (player != null)
                return Ok(player);

            return NotFound();
        }

        // POST api/values
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreatePlayer([FromBody]Model.Player value)
        {
            int recordsModified = await _repo.CreatePlayer(value);

            return CreatedAtAction(nameof(CreatePlayer), new
            {
                id = value.PlayerId,
                modified = recordsModified
            });
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateBatting([FromBody]PlayerBattingStatistics value)
        {
            int recordsModified = await _repo.CreatePlayerBattingStatistics(value);

            return CreatedAtAction(nameof(CreateBatting), new { id = value.PlayerBattingStatisticsId, modified = recordsModified });
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreatePitching([FromBody]PlayerPitchingStatistics value)
        {
            int recordsModified = await _repo.CreatePlayerPitchingStatistics(value);

            return CreatedAtAction(nameof(CreatePitching), new
            {
                id = value.PlayerId,
                modified = recordsModified
            });
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
