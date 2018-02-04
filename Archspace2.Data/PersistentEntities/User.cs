using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace Archspace2
{
    [Table("User")]
    public class User : IdentityUser<int>
    {
        public User() : base()
        {
        }

        public async Task<Player> CreatePlayerAsync(string aName, Race aRace)
        {
            using (DatabaseContext databaseContext = Game.Context)
            {
                databaseContext.Attach(Game.Universe);

                Player player = Game.Universe.CreatePlayer(aName, aRace);

                await databaseContext.SaveChangesAsync();

                return player;
            }
        }
    }
}
