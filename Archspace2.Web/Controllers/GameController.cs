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
        //[HttpPost]
        [Route("universe")]
        public async Task<IActionResult> CreateUniverse()
        {
            await Game.CreateNewUniverseAsync(DateTime.Now);

            return Ok();
        }

        [Route("start")]
        public async Task<IActionResult> Start()
        {
            try
            {
                Game.Start();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [Route("load")]
        public async Task<IActionResult> Load()
        {
            await Game.LoadUniverseAsync();

            return Ok();
        }
    }
}