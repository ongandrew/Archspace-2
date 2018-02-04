using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;

namespace Archspace2.Web.Controllers
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
                return Ok();
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
            return Ok();
        }
    }
}