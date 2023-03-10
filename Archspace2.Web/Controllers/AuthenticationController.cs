using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;

namespace Archspace2.Web
{
    [Route("authentication")]
    public class AuthenticationController : Controller
    {
        [Route("microsoft")]
        public async Task<IActionResult> MicrosoftSignIn()
        {
            AuthenticateResult result = await HttpContext.AuthenticateAsync(MicrosoftAccountDefaults.AuthenticationScheme);

            if (result.Succeeded)
            {
                await HttpContext.SignInAsync(result.Principal);
                return RedirectToAction("Main", "Archspace");
            }
            else
            {
                return Unauthorized();
            }
        }

        [Route("signout")]
        public async Task<IActionResult> Signout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Main", "Archspace");
        }
    }
}