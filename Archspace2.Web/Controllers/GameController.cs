using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Archspace2.Web
{
    [Authorize]
    [Route("game")]
    public class GameController : Controller
    {
        //[HttpPost]
        [Route("universe")]
        public async Task<IActionResult> CreateUniverse()
        {
            await Game.CreateNewUniverseAsync(DateTime.Now);

            return RedirectToAction("Admin", "Archspace");
        }

        [Route("start")]
        public async Task<IActionResult> Start()
        {
            try
            {
                Game.Start();

                return RedirectToAction("Admin", "Archspace");
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

        [Route("status")]
        public async Task<IActionResult> Status()
        {
            GameStatusResponse response = new GameStatusResponse();
            response.IsRunning = Game.IsRunning();
            response.CurrentUniverseId = Game.Universe == null ? (int?)null : Game.Universe.Id;
            response.CurrentTurn = Game.Universe == null ? (int?)null : Game.Universe.CurrentTurn;

            return Ok(response);
        }
    }
}