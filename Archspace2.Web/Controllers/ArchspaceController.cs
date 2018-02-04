using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Archspace2.Web
{
    [Authorize]
    [Route("archspace")]
    public class ArchspaceController : Controller
    {
        [Route("")]
        [Route("main")]
        public async Task<IActionResult> Index()
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

        [Route("admin")]
        public async Task<IActionResult> Admin()
        {
            return View();
        }
        
        protected async Task<bool> HasCharacter()
        {
            User user = await GetUserAsync();
            return user.GetPlayer() != null;
        }

        protected async Task<Player> GetCharacterAsync()
        {
            return (await GetUserAsync()).GetPlayer();
        }

        protected async Task<User> GetUserAsync()
        {
            using (DatabaseContext context = Game.GetContext())
            {
                return await context.GetUserAsync(User);
            }
        }

        protected async Task<IActionResult> RedirectToCreationOrReturnViewWithPlayerDataAsync()
        {
            if (!await HasCharacter())
            {
                return RedirectToAction("Create");
            }
            else
            {
                ViewData["Player"] = await GetCharacterAsync();
                return View();
            }
        }
    }
}