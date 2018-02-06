using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Archspace2.Web
{
    [Authorize]
    [Route("player")]
    public class PlayerController : Controller
    {
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            try
            {
                using (DatabaseContext context = Game.GetContext())
                {
                    User user = await context.GetUserAsync(User);
                    Player player = user.GetPlayer();

                    return Ok(player);
                }
            }
            catch (Exception e)
            {
                await Game.LogAsync(e);
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create([FromForm]CreatePlayerForm aForm)
        {
            try
            {
                using (DatabaseContext context = Game.GetContext())
                {
                    context.Attach(Game.Universe);

                    User user = await context.GetUserAsync(User);
                    Player player = user.CreatePlayer(aForm.Name, aForm.Race);

                    await context.SaveChangesAsync();
                }

                return RedirectToAction("Main", "Archspace");
            }
            catch (Exception e)
            {
                await Game.LogAsync(e);
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("concentration_mode")]
        public async Task<IActionResult> ChangeConcentrationMode([FromForm]ChangeConcentrationModeForm aForm)
        {
            try
            {
                using (DatabaseContext context = Game.GetContext())
                {
                    context.Attach(Game.Universe);

                    User user = await context.GetUserAsync(User);
                    Player player = Game.Universe.Players.Where(x => x.User != null && x.User.Id == user.Id).Single();

                    player.ConcentrationMode = aForm.Mode;

                    await context.SaveChangesAsync();

                    return RedirectToAction("ConcentrationMode", "Archspace");
                }
            }
            catch (Exception e)
            {
                await Game.LogAsync(e);
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("change_target_tech")]
        public async Task<IActionResult> ChangeTargetTech([FromForm]int id)
        {
            try
            {
                using (DatabaseContext context = Game.GetContext())
                {
                    context.Attach(Game.Universe);

                    User user = await context.GetUserAsync(User);
                    Player player = Game.Universe.Players.Where(x => x.User != null && x.User.Id == user.Id).Single();

                    Tech tech = Game.Configuration.Techs.Where(x => x.Id == id).SingleOrDefault();
                    if (tech == null || player.Techs.Any(x => x.Id == tech.Id))
                    {
                        player.TargetTech = null;
                    }
                    else
                    {
                        player.TargetTech = tech;
                    }

                    await context.SaveChangesAsync();

                    return RedirectToAction("Research", "Archspace");
                }
            }
            catch (Exception e)
            {
                await Game.LogAsync(e);
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("purchase_projects")]
        public async Task<IActionResult> PurchaseProjects([FromForm]int[] ids)
        {
            try
            {
                using (DatabaseContext context = Game.GetContext())
                {
                    context.Attach(Game.Universe);

                    User user = await context.GetUserAsync(User);
                    Player player = Game.Universe.Players.Where(x => x.User != null && x.User.Id == user.Id).Single();

                    foreach (int id in ids)
                    {
                        player.PurchaseProject(id);
                    }

                    await context.SaveChangesAsync();

                    return RedirectToAction("Project", "Archspace");
                }
            }
            catch (Exception e)
            {
                await Game.LogAsync(e);
                return BadRequest();
            }
        }
    }
}