using Archspace2.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Archspace2.Web.Controllers
{
    [Authorize]
    [Route("diplomacy")]
    public class DiplomacyController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        [Route("mass_actions")]
        public async Task<IActionResult> GetMassActions()
        {
            Dictionary<PlayerMessageType, string> result = new Dictionary<PlayerMessageType, string>();

            result.Add(PlayerMessageType.SuggestTruce, PlayerMessageType.SuggestTruce.ToString().ToFriendlyString());
            result.Add(PlayerMessageType.SuggestPact, PlayerMessageType.SuggestPact.ToString().ToFriendlyString());
            result.Add(PlayerMessageType.SuggestAlly, PlayerMessageType.SuggestAlly.ToString().ToFriendlyString());
            result.Add(PlayerMessageType.BreakPact, PlayerMessageType.BreakPact.ToString().ToFriendlyString());
            result.Add(PlayerMessageType.BreakAlly, PlayerMessageType.BreakAlly.ToString().ToFriendlyString());
            result.Add(PlayerMessageType.DeclareHostility, PlayerMessageType.DeclareHostility.ToString().ToFriendlyString());
            result.Add(PlayerMessageType.DeclareWar, PlayerMessageType.DeclareWar.ToString().ToFriendlyString());
            result.Add(PlayerMessageType.DeclareTotalWar, PlayerMessageType.DeclareTotalWar.ToString().ToFriendlyString());

            return Ok(result);
        }

        [HttpPost]
        [Route("execute_mass_action")]
        public async Task<IActionResult> ExecuteMassAction([FromBody] DiplomacyManagementRequest aRequest)
        {
            try
            {
                using (DatabaseContext context = Game.GetContext())
                {
                    context.Attach(Game.Universe);

                    User user = await context.GetUserAsync(User);
                    Player player = Game.Universe.Players.Where(x => x.User != null && x.User.Id == user.Id).Single();

                    foreach (int id in aRequest.Ids)
                    {
                        try
                        {
                            player.ExecuteDiplomaticAction(aRequest.Action, id);
                        }
                        catch (InvalidOperationException e)
                        {
                            return BadRequest(e.Message);
                        }
                    }

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