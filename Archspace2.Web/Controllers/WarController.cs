using Archspace2.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Archspace2.Web.Controllers
{
    [Authorize]
    [Route("war")]
    public class WarController : Controller
    {
        [HttpPost]
        [Route("select_defense_plan_fleets")]
        public async Task<IActionResult> SelectDefensePlanFleets([FromBody] DefensePlanRequest aRequest)
        {
            return RedirectToAction("DefensePlan", "Archspace", aRequest);
        }

        [HttpPost]
        [Route("save_defense_Plan")]
        public async Task<IActionResult> SaveDefensePlan([FromBody] SaveDefensePlanRequest aRequest)
        {
            try
            {
                using (DatabaseContext context = Game.GetContext())
                {
                    context.Attach(Game.Universe);

                    User user = await context.GetUserAsync(User);
                    Player player = Game.Universe.Players.Where(x => x.User != null && x.User.Id == user.Id).Single();

                    // Allow only one defense plan...?
                    context.DefenseDeployments.RemoveRange(player.DefensePlans.SelectMany(x => x.DefenseDeployments));
                    context.DefensePlans.RemoveRange(player.DefensePlans);
                    player.DefensePlans.Clear();

                    List<DefenseDeployment> deployments = aRequest.Deployments.Select(x => new DefenseDeployment(Game.Universe)
                    {
                        X = (int)x.X,
                        Y = (int)x.Y,
                        FleetId = x.FleetId,
                        Fleet = player.Fleets.Single(y => y.Id == x.FleetId),
                        Type = x.IsCapitalFleet ? DefenseDeploymentType.Capital : DefenseDeploymentType.Normal,
                        Command = x.Command
                    }).ToList();

                    player.CreateDefensePlan(deployments);

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