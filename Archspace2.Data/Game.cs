using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
