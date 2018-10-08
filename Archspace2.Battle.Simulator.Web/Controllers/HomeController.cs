using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Archspace2.Battle.Simulator.Web.Models;

namespace Archspace2.Battle.Simulator.Web
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
