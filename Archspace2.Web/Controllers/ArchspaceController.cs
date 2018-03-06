﻿using System;
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

        [Route("admin")]
        public async Task<IActionResult> Admin()
        {
            return View();
        }

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
    }
#pragma warning restore CS1998
}