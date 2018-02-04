using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Archspace2
{
    [Table("User")]
    public class User : IdentityUser<int>
    {
        public User() : base()
        {
        }

        public User(ClaimsPrincipal aClaimsPrincipal)
        {
            Email = aClaimsPrincipal.FindFirstValue(ClaimTypes.Email);
        }

        public Player CreatePlayer(string aName, Race aRace)
        {
            Player player = Game.Universe.CreatePlayer(aName, aRace);
            player.User = this;
            
            return player;
        }

        public Player CreatePlayer(string aName, RaceType aRace)
        {
            return CreatePlayer(aName, Game.Configuration.Races.Single(x => x.Id == (int)aRace));
        }

        public Player GetPlayer()
        {
            return Game.Universe.Players.SingleOrDefault(x => x.User != null && x.User.Id == Id);
        }
    }
}
