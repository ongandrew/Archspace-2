using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Archspace2.Web.Controllers
{
    [Route("encyclopedia")]
    public class EncyclopediaController : Controller
    {
        [Route("")]
        public async Task<IActionResult> Encyclopedia()
        {
            return View();
        }

        [Route("{type}/{id}")]
        public async Task<IActionResult> Entry(string type, int id)
        {
            ViewData["Type"] = type.ToLowerInvariant();
            ViewData["Id"] = id;

            return View();
        }
    }
}