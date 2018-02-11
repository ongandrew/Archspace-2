using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Archspace2.Web
{
    [Authorize]
    public abstract class ControllerBase : Controller
    {
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
    }
}