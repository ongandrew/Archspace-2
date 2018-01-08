using System.Collections.Generic;
using System.Linq;

namespace Archspace2
{
    public static class Game
    {
        public static GameConfiguration Configuration { get; }

        public static List<GameInstance> GameInstances
        {
            get
            {
                using (DatabaseContext databaseContext = new DatabaseContext())
                {
                    return databaseContext.GameInstances.ToList();
                }
            }
        }
        public static List<User> Users { get
            {
                using (DatabaseContext databaseContext = new DatabaseContext())
                {
                    return databaseContext.Users.ToList();
                }
            }
        }

        static Game()
        {
            Configuration = new GameConfiguration();
        }
    }
}
