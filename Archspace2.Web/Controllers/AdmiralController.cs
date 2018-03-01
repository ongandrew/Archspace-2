using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Archspace2.Web
{
    [Authorize]
    [Route("admiral")]
    public class AdmiralController : Controller
    {
        [HttpPost]
        [Route("dismiss")]
        public async Task<IActionResult> Dismiss([FromForm]int[] ids)
        {
            try
            {
                using (DatabaseContext context = Game.GetContext())
                {
                    context.Attach(Game.Universe);

                    User user = await context.GetUserAsync(User);
                    Player player = Game.Universe.Players.Where(x => x.User != null && x.User.Id == user.Id).Single();

                    List<Admiral> admiralsToDismiss = player.Admirals.Where(x => ids.Contains(x.Id)).ToList();
                    foreach (Admiral admiral in admiralsToDismiss)
                    {
                        if (admiral.Fleet == null)
                        {
                            player.Admirals.Remove(admiral);
                            context.Admirals.Remove(admiral);
                        }
                    }

                    await context.SaveChangesAsync();

                    return RedirectToAction("Admiral", "Archspace");
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