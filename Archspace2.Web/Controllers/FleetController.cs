﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Archspace2.Web.Controllers
{
    [Authorize]
    [Route("fleet")]
    public class FleetController : Controller
    {
        [HttpPost]
        [Route("form_new_fleet")]
        public async Task<IActionResult> FormNewFleet([FromBody]FormNewFleetForm aForm)
        {
            try
            {
                using (DatabaseContext context = Game.GetContext())
                {
                    context.Attach(Game.Universe);

                    User user = await context.GetUserAsync(User);
                    Player player = Game.Universe.Players.Where(x => x.User != null && x.User.Id == user.Id).Single();

                    try
                    {
                        player.FormNewFleet(aForm.Order, aForm.Name, aForm.AdmiralId, aForm.ShipDesignId, aForm.Number);
                    }
                    catch (InvalidOperationException e)
                    {
                        return BadRequest(e.Message);
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

        [HttpPost]
        [Route("disband_fleets")]
        public async Task<IActionResult> DisbandFleets([FromBody]DisbandFleetsRequest aRequest)
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
                            player.DisbandFleet(id);
                        }
                        catch (InvalidOperationException e)
                        {
                            return BadRequest(e.Message);
                        }

                        context.Fleets.Remove(await context.Fleets.SingleAsync(x => x.Id == id));
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