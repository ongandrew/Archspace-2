using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Archspace2.Web.Controllers
{
    [Authorize]
    [Route("archspace")]
    public class ArchspaceController : Controller
    {
        [Route("")]
        [Route("main")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [Route("admin")]
        public async Task<IActionResult> Admin()
        {
            return View();
        }
    }
}