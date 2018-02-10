using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Archspace2.Web
{
    [Authorize]
    [Route("planet")]
    public class PlanetController : ControllerBase
    {
        [Route("{id}/view_detail")]
        public async Task<IActionResult> ViewDetail(int id)
        {
            if (!await HasCharacter())
            {
                return RedirectToAction("Create", "Archspace");
            }
            else
            {
                Player player = await GetCharacterAsync();
                ViewData["Player"] = player;
                Planet planet = player.Planets.SingleOrDefault(x => x.Id == id);

                if (planet == null)
                {
                    // throw new NotImplementedException();
                    return RedirectToAction("Main", "Archspace");
                }
                else
                {
                    ViewData["Planet"] = planet;
                    return View();
                }
            }
        }

        [HttpPost]
        [Route("{id}/change_ratio")]
        public async Task<IActionResult> ChangeDistributionRatio(int id, [FromBody]ChangeDistributionRatioForm aForm)
        {
            if (!await HasCharacter())
            {
                return RedirectToAction("Create", "Archspace");
            }
            else
            {
                using (DatabaseContext context = Game.GetContext())
                {
                    context.Attach(Game.Universe);

                    Player player = await GetCharacterAsync();
                    ViewData["Player"] = player;
                    Planet planet = player.Planets.SingleOrDefault(x => x.Id == id);

                    if (planet == null)
                    {
                        // throw new NotImplementedException();
                        return RedirectToAction("Main", "Archspace");
                    }
                    else
                    {
                        planet.DistributionRatio.Set(aForm.Factory, aForm.ResearchLab, aForm.MilitaryBase);
                        await context.SaveChangesAsync();

                        return RedirectToAction("ViewDetail", "Planet");
                    }
                }
            }
        }

        [HttpPost]
        [Route("{id}/change_investment")]
        public async Task<IActionResult> ChangeInvestment(int id, [FromForm]int amount)
        {
            if (!await HasCharacter())
            {
                return RedirectToAction("Create", "Archspace");
            }
            else
            {
                using (DatabaseContext context = Game.GetContext())
                {
                    context.Attach(Game.Universe);

                    Player player = await GetCharacterAsync();
                    ViewData["Player"] = player;
                    Planet planet = player.Planets.SingleOrDefault(x => x.Id == id);

                    if (planet == null)
                    {
                        // throw new NotImplementedException();
                        return RedirectToAction("Main", "Archspace");
                    }
                    else
                    {
                        if (amount <= player.Resource.ProductionPoint && amount > 0)
                        {
                            int trueAmount = amount;

                            if (int.MaxValue - planet.Investment < amount)
                            {
                                trueAmount = int.MaxValue - planet.Investment;
                            }
                            else
                            {
                                trueAmount = amount;
                            }

                            planet.Investment += trueAmount;
                            player.Resource.ProductionPoint -= trueAmount;

                            await context.SaveChangesAsync();
                        }

                        return RedirectToAction("ViewDetail", "Planet");
                    }
                }
            }
        }
    }
}