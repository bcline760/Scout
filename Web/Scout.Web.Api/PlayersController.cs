using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Scout.Core.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Scout.Web.Api
{
    [Route("api/[controller]")]
    public class PlayersController : ScoutApiController
    {
        private IPlayerService m_service;
        public PlayersController(IPlayerService service)
        {
            m_service = service;
        }
    }
}
