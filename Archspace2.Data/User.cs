using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Archspace2
{
    public class User : Entity
    {
        public User() : base()
        {
        }

        public async Task<Player> CreatePlayerAsync(string aName, Race aRace)
        {
            using (DatabaseContext databaseContext = Game.Context)
            {
                Player player = Game.Universe.CreatePlayer(aName, aRace);

                databaseContext.Universes.Update(Game.Universe);

                await databaseContext.SaveChangesAsync();

                return player;
            }
        }
    }
}
