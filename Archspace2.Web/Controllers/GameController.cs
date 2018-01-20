using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Archspace2.Web.Controllers
{
    //[Authorize]
    [Route("game")]
    public class GameController : Controller
    {
        [HttpPost]
        [Route("universe")]
        public async Task<IActionResult> CreateUniverse()
        {
            await Game.CreateNewUniverseAsync(DateTime.Now);

            return Ok();
        }

        [Route("start")]
        public async Task<IActionResult> Start()
        {
            Game.Start();
            return Ok();
        }
    }
}