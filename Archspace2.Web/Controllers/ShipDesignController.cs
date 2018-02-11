using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Archspace2.Web
{
    [Authorize]
    [Route("ship_design")]
    public class ShipDesignController : ControllerBase
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
                ShipDesign design = player.ShipDesigns.SingleOrDefault(x => x.Id == id);

                if (design == null)
                {
                    // throw new NotImplementedException();
                    return RedirectToAction("Main", "Archspace");
                }
                else
                {
                    ViewData["Design"] = design;
                    return View();
                }
            }
        }

        [HttpPost]
        [Route("build")]
        public async Task<IActionResult> Build([FromForm]ShipBuildOrderForm aForm)
        {
            if (!await HasCharacter())
            {
                return RedirectToAction("Create", "Archspace");
            }
            else
            {
                Player player = await GetCharacterAsync();
                ViewData["Player"] = player;
                ShipDesign design = player.ShipDesigns.SingleOrDefault(x => x.Id == aForm.Id);

                if (design == null)
                {
                    // throw new NotImplementedException();
                    return RedirectToAction("Main", "Archspace");
                }
                else
                {
                    using (DatabaseContext context = Game.GetContext())
                    {
                        context.Attach(Game.Universe);

                        player.Shipyard.PlaceBuildOrder(aForm.Amount, design);

                        await context.SaveChangesAsync();

                        return RedirectToAction("ShipBuilding", "Archspace");
                    }
                }
            }
        }

        [HttpPost]
        [Route("{id}/cancel")]
        public async Task<IActionResult> CancelOrder(int id)
        {
            if (!await HasCharacter())
            {
                return RedirectToAction("Create", "Archspace");
            }
            else
            {
                Player player = await GetCharacterAsync();
                ViewData["Player"] = player;
                ShipBuildOrder order = player.Shipyard.ShipBuildOrders.SingleOrDefault(x => x.Id == id);

                if (order == null)
                {
                    // throw new NotImplementedException();
                    return RedirectToAction("Main", "Archspace");
                }
                else
                {
                    using (DatabaseContext context = Game.GetContext())
                    {
                        context.Attach(Game.Universe);

                        player.Shipyard.DeleteBuildOrder(order);
                        context.ShipBuildOrders.Remove(order);

                        await context.SaveChangesAsync();

                        return RedirectToAction("ShipBuilding", "Archspace");
                    }
                }
            }
        }
    }
}