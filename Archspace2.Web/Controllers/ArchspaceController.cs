using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Archspace2.Web
{
#pragma warning disable CS1998
    [Authorize]
    [Route("archspace")]
    public class ArchspaceController : ControllerBase
    {
        [Route("")]
        [Route("main")]
        public async Task<IActionResult> Main()
        {
            return await RedirectToCreationOrReturnViewWithPlayerDataAsync();
        }

        [Route("create")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [Route("create_result")]
        public async Task<IActionResult> CreateResult()
        {
            if (await HasCharacter())
            {
                ViewData["Message"] = "Success";
            }
            else
            {
                ViewData["Message"] = "Failure";
            }
            return View();
        }

        [Route("concentration_mode")]
        public async Task<IActionResult> ConcentrationMode()
        {
            return await RedirectToCreationOrReturnViewWithPlayerDataAsync();
        }

        [Route("planet_invest_pool")]
        public async Task<IActionResult> PlanetInvestPool()
        {
            return await RedirectToCreationOrReturnViewWithPlayerDataAsync();
        }

        [Route("planet_management")]
        public async Task<IActionResult> PlanetManagement()
        {
            return await RedirectToCreationOrReturnViewWithPlayerDataAsync();
        }

        [Route("research")]
        public async Task<IActionResult> Research()
        {
            return await RedirectToCreationOrReturnViewWithPlayerDataAsync();
        }

        [Route("project")]
        public async Task<IActionResult> Project()
        {
            return await RedirectToCreationOrReturnViewWithPlayerDataAsync();
        }

        [Route("whitebook")]
        public async Task<IActionResult> Whitebook()
        {
            return await RedirectToCreationOrReturnViewWithPlayerDataAsync();
        }

        [Route("diplomacy_status_list")]
        public async Task<IActionResult> DiplomacyStatusList()
        {
            return await RedirectToCreationOrReturnViewWithPlayerDataAsync();
        }

        [Route("diplomacy_management")]
        public async Task<IActionResult> DiplomacyManagement()
        {
            return await RedirectToCreationOrReturnViewWithPlayerDataAsync();
        }

        [Route("read_messages")]
        public async Task<IActionResult> ReadMessages()
        {
            return await RedirectToCreationOrReturnViewWithPlayerDataAsync();
        }

        [Route("view_player_message/{id}")]
        public async Task<IActionResult> ViewPlayerMessage(int id)
        {
            if (!await HasCharacter())
            {
                return RedirectToAction("Create", "Archspace");
            }
            else
            {
                Player player = await GetCharacterAsync();
                ViewData["Player"] = player;

                PlayerMessage message = player.Mailbox.FindMessageById(id);
                
                if (message == null)
                {
                    return RedirectToErrorPage("There is no message with that ID.");
                }
                else
                {
                    ViewData["Message"] = message;
                    return View();
                }
            }
        }

        [Route("send_message")]
        public async Task<IActionResult> SendMessage()
        {
            return await RedirectToCreationOrReturnViewWithPlayerDataAsync();
        }

        [Route("inspect_player")]
        public async Task<IActionResult> InspectPlayer()
        {
            return await RedirectToCreationOrReturnViewWithPlayerDataAsync();
        }

        [Route("player_info/{id}")]
        public async Task<IActionResult> PlayerInfo(int id)
        {
            if (!await HasCharacter())
            {
                return RedirectToAction("Create", "Archspace");
            }
            else
            {
                Player player = await GetCharacterAsync();
                ViewData["Player"] = player;

                Player other = Game.Universe.Players.SingleOrDefault(x => x.Id == id);

                if (other == null)
                {
                    return RedirectToErrorPage("There is no player with that ID.");
                }
                else
                {
                    ViewData["TargetPlayer"] = other;
                    return View();
                }
            }
        }

        [Route("special_operation")]
        public async Task<IActionResult> SpecialOperation()
        {
            return await RedirectToCreationOrReturnViewWithPlayerDataAsync();
        }

        [Route("ship_design")]
        public async Task<IActionResult> ShipDesign()
        {
            return await RedirectToCreationOrReturnViewWithPlayerDataAsync();
        }

        [Route("ship_building")]
        public async Task<IActionResult> ShipBuilding()
        {
            return await RedirectToCreationOrReturnViewWithPlayerDataAsync();
        }

        [Route("admiral")]
        public async Task<IActionResult> Admiral()
        {
            return await RedirectToCreationOrReturnViewWithPlayerDataAsync();
        }

        [Route("ship_pool")]
        public async Task<IActionResult> ShipPool()
        {
            return await RedirectToCreationOrReturnViewWithPlayerDataAsync();
        }

        [Route("form_new_fleet")]
        public async Task<IActionResult> FormNewFleet()
        {
            return await RedirectToCreationOrReturnViewWithPlayerDataAsync();
        }

        [Route("disband_fleet")]
        public async Task<IActionResult> DisbandFleet()
        {
            return await RedirectToCreationOrReturnViewWithPlayerDataAsync();
        }

        [Route("reassignment")]
        public async Task<IActionResult> Reassignment()
        {
            return await RedirectToCreationOrReturnViewWithPlayerDataAsync();
        }

        [Route("expedition")]
        public async Task<IActionResult> Expedition()
        {
            return await RedirectToCreationOrReturnViewWithPlayerDataAsync();
        }

        [Route("mission")]
        public async Task<IActionResult> Mission()
        {
            return await RedirectToCreationOrReturnViewWithPlayerDataAsync();
        }

        [Route("recall")]
        public async Task<IActionResult> Recall()
        {
            return await RedirectToCreationOrReturnViewWithPlayerDataAsync();
        }

        [Route("council_vote")]
        public async Task<IActionResult> CouncilVote()
        {
            return await RedirectToCreationOrReturnViewWithPlayerDataAsync();
        }

        [Route("speaker_menu")]
        public async Task<IActionResult> SpeakerMenu()
        {
            return await RedirectToCreationOrReturnViewWithPlayerDataAsync();
        }

        [Route("defense_plan_fleet_selection")]
        public async Task<IActionResult> DefensePlanFleetSelection()
        {
            return await RedirectToCreationOrReturnViewWithPlayerDataAsync();
        }

        [Route("defense_plan")]
        public async Task<IActionResult> DefensePlan(DefensePlanRequest aRequest)
        {
            if (!await HasCharacter())
            {
                return RedirectToAction("Create", "Archspace");
            }
            else
            {
                Player player = await GetCharacterAsync();
                ViewData["Player"] = player;
                
                Fleet capitalFleet = player.Fleets.SingleOrDefault(x => x.Id == aRequest.CapitalFleetId);
                if (capitalFleet == null)
                {
                    return BadRequest("Invalid capital fleet selected.");
                }

                List<int> remainingFleets = aRequest.Fleets;
                remainingFleets.RemoveAll(x => x == aRequest.CapitalFleetId);

                List<Fleet> otherFleets = player.Fleets.Where(fleet => remainingFleets.Contains(fleet.Id)).ToList();

                List<Deployment> deployments = new List<Deployment>();

                DefensePlan defensePlan = player.DefensePlans.FirstOrDefault();
                if (defensePlan != null && defensePlan.CapitalDeployment.Fleet.Id == capitalFleet.Id)
                {
                    deployments.Add(new Deployment()
                    {
                        FleetId = capitalFleet.Id,
                        FleetDisplayName = capitalFleet.GetDisplayName(),
                        Angle = 180,
                        X = 8000,
                        Y = 5000,
                        IsCapitalFleet = true,
                        Command = defensePlan.CapitalDeployment.Command
                    });
                }
                else
                {
                    deployments.Add(new Deployment()
                    {
                        FleetId = capitalFleet.Id,
                        FleetDisplayName = capitalFleet.GetDisplayName(),
                        Angle = 180,
                        X = 8000,
                        Y = 5000,
                        IsCapitalFleet = true,
                        Command = Command.Normal
                    });
                }

                List<Tuple<int, int>> gridPoints = new List<Tuple<int, int>>();
                for (int y = 8000; y >= 2000; y -= 200)
                {
                    for (int x = 7000; x <= 9000; x += 200)
                    {
                        if (x != 8000 && y != 5000)
                        {
                            gridPoints.Add(new Tuple<int, int>(x, y));
                        }
                    }
                }
                
                foreach (Fleet fleet in otherFleets)
                {
                    if (defensePlan != null && defensePlan.DefenseDeployments.Where(x => x.Type != DefenseDeploymentType.Capital).Any(x => x.Fleet.Id == fleet.Id))
                    {
                        DefenseDeployment defenseDeployment = defensePlan.DefenseDeployments.Where(x => x.Type != DefenseDeploymentType.Capital).Single(x => x.Fleet.Id == fleet.Id);

                        deployments.Add(new Deployment()
                        {
                            Angle = 180,
                            FleetId = fleet.Id,
                            FleetDisplayName = fleet.GetDisplayName(),
                            IsCapitalFleet = false,
                            X = defenseDeployment.X,
                            Y = defenseDeployment.Y,
                            Command = defenseDeployment.Command
                        });
                    }
                    else
                    {
                        Tuple<int, int> point = gridPoints.First();
                        

                        deployments.Add(new Deployment()
                        {
                            Angle = 180,
                            FleetId = fleet.Id,
                            FleetDisplayName = fleet.GetDisplayName(),
                            IsCapitalFleet = false,
                            X = point.Item1,
                            Y = point.Item2,
                            Command = Command.Normal
                        });

                        gridPoints.Remove(point);
                    }
                }

                ViewData["Deployments"] = deployments;

                return View();
            }
        }

        [Route("clusters")]
        public async Task<IActionResult> Clusters()
        {
            return await RedirectToCreationOrReturnViewWithPlayerDataAsync();
        }

        [Route("ranking")]
        public async Task<IActionResult> Ranking()
        {
            return await RedirectToCreationOrReturnViewWithPlayerDataAsync();
        }

        [AllowAnonymous]
        [Route("encyclopedia")]
        public async Task<IActionResult> Encyclopedia()
        {
            return View();
        }

        [Route("error")]
        public async Task<IActionResult> Error(string message)
        {
            ViewData["Message"] = message;

            return View();
        }

        [Route("admin")]
        public async Task<IActionResult> Admin()
        {
            return View();
        }

        [NonAction]
        protected async Task<IActionResult> RedirectToCreationOrReturnViewWithPlayerDataAsync()
        {
            if (!await HasCharacter())
            {
                return RedirectToAction("Create", "Archspace");
            }
            else
            {
                ViewData["Player"] = await GetCharacterAsync();
                return View();
            }
        }

        [NonAction]
        public IActionResult RedirectToErrorPage(string aMessage)
        {
            return RedirectToAction("Error", "Archspace", new { message = aMessage });
        }
    }
#pragma warning restore CS1998
}