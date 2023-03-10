using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Archspace2.Web
{
    [Authorize]
    [Route("player")]
    public class PlayerController : ControllerBase
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
                return StatusCode(StatusCodes.Status500InternalServerError);
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
                return StatusCode(StatusCodes.Status500InternalServerError);
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
                return StatusCode(StatusCodes.Status500InternalServerError);
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
                return StatusCode(StatusCodes.Status500InternalServerError);
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
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("change_investment_pool_usage")]
        public async Task<IActionResult> ChangeInvestmentPoolUsage([FromForm]int[] ids)
        {
            try
            {
                using (DatabaseContext context = Game.GetContext())
                {
                    context.Attach(Game.Universe);

                    User user = await context.GetUserAsync(User);
                    Player player = Game.Universe.Players.Where(x => x.User != null && x.User.Id == user.Id).Single();

                    foreach (Planet planet in player.Planets)
                    {
                        planet.UsePlanetInvestmentPool = false;
                    }

                    foreach (int id in ids)
                    {
                        Planet planet = player.Planets.SingleOrDefault(x => x.Id == id);
                        if (planet != null)
                        {
                            planet.UsePlanetInvestmentPool = true;
                        }
                    }

                    await context.SaveChangesAsync();

                    return RedirectToAction("PlanetInvestPool", "Archspace");
                }
            }
            catch (Exception e)
            {
                await Game.LogAsync(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("change_ship_investment")]
        public async Task<IActionResult> ChangeShipInvestment([FromForm]long amount)
        {
            try
            {
                using (DatabaseContext context = Game.GetContext())
                {
                    context.Attach(Game.Universe);

                    User user = await context.GetUserAsync(User);
                    Player player = Game.Universe.Players.Where(x => x.User != null && x.User.Id == user.Id).Single();

                    if (amount <= player.Resource.ProductionPoint && amount > 0)
                    {
                        long trueAmount = amount;

                        if (long.MaxValue - player.Shipyard.ShipProductionInvestment < amount)
                        {
                            trueAmount = long.MaxValue - player.Shipyard.ShipProductionInvestment;
                        }
                        else
                        {
                            trueAmount = amount;
                        }

                        player.Shipyard.ShipProductionInvestment += trueAmount;
                        player.Resource.ProductionPoint -= trueAmount;

                        await context.SaveChangesAsync();
                    }

                    return RedirectToAction("ShipBuilding", "Archspace");
                }
            }
            catch (Exception e)
            {
                await Game.LogAsync(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("change_research_investment")]
        public async Task<IActionResult> ChangeResearchInvestment([FromForm]long amount)
        {
            try
            {
                using (DatabaseContext context = Game.GetContext())
                {
                    context.Attach(Game.Universe);

                    User user = await context.GetUserAsync(User);
                    Player player = Game.Universe.Players.Where(x => x.User != null && x.User.Id == user.Id).Single();

                    if (amount <= player.Resource.ProductionPoint && amount > 0)
                    {
                        long trueAmount = amount;

                        if (long.MaxValue - player.ResearchInvestment < amount)
                        {
                            trueAmount = long.MaxValue - player.ResearchInvestment;
                        }
                        else
                        {
                            trueAmount = amount;
                        }

                        player.ResearchInvestment += trueAmount;
                        player.Resource.ProductionPoint -= trueAmount;

                        await context.SaveChangesAsync();
                    }

                    return RedirectToAction("Research", "Archspace");
                }
            }
            catch (Exception e)
            {
                await Game.LogAsync(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("change_investment_pool")]
        public async Task<IActionResult> ChangeInvestmentPool([FromForm]long amount)
        {
            try
            {
                using (DatabaseContext context = Game.GetContext())
                {
                    context.Attach(Game.Universe);

                    User user = await context.GetUserAsync(User);
                    Player player = Game.Universe.Players.Where(x => x.User != null && x.User.Id == user.Id).Single();

                    if (amount <= player.Resource.ProductionPoint && amount > 0)
                    {
                        long trueAmount = amount;

                        if (long.MaxValue - player.PlanetInvestmentPool < amount)
                        {
                            trueAmount = long.MaxValue - player.PlanetInvestmentPool;
                        }
                        else
                        {
                            trueAmount = amount;
                        }

                        player.PlanetInvestmentPool += trueAmount;
                        player.Resource.ProductionPoint -= trueAmount;

                        await context.SaveChangesAsync();
                    }

                    return RedirectToAction("PlanetInvestPool", "Archspace");
                }
            }
            catch (Exception e)
            {
                await Game.LogAsync(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("{id}/vote")]
        public async Task<IActionResult> ChangeVote(int id)
        {
            try
            {
                using (DatabaseContext context = Game.GetContext())
                {
                    context.Attach(Game.Universe);

                    User user = await context.GetUserAsync(User);
                    Player player = Game.Universe.Players.Where(x => x.User != null && x.User.Id == user.Id).Single();

                    player.ChangeVote(id);

                    await context.SaveChangesAsync();

                    return Ok();
                }
            }
            catch (Exception e)
            {
                await Game.LogAsync(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}