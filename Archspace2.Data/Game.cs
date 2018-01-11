using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Archspace2
{
    public static class Game
    {
        private static bool mInitialized = false;

        private static string mConnectionString;
        public static DatabaseContext Context
        {
            get
            {
                if (mConnectionString == null)
                {
                    throw new InvalidOperationException("Game engine not running.");
                }
                else
                {
                    return new DatabaseContext(mConnectionString);
                }
            }
        }

        private static GameConfiguration mGameConfiguration;
        public static GameConfiguration Configuration {
            get
            {
                if (mGameConfiguration == null)
                {
                    throw new InvalidOperationException("Game engine not running.");
                }
                else
                {
                    return mGameConfiguration;
                }
            }
        }

        private static List<Universe> mUniverses;
        public static List<Universe> Universes
        {
            get
            {
                if (mUniverses == null)
                {
                    throw new InvalidOperationException("Game engine not running.");
                }
                else
                {
                    return mUniverses;
                }
            }
        }

        private static List<User> mUsers;
        public static List<User> Users
        {
            get
            {
                if (mUsers == null)
                {
                    throw new InvalidOperationException("Game engine not running.");
                }
                else
                {
                    return mUsers;
                }
            }
        }

        public static async Task InitializeAsync(string aConnectionString, GameConfiguration aGameConfiguration = null)
        {
            if (mInitialized)
            {
                throw new InvalidOperationException("Game engine has already been initialized.");
            }

            mConnectionString = aConnectionString ?? throw new InvalidOperationException("Invalid connection string provided.");

            if (aGameConfiguration == null)
            {
                mGameConfiguration = GameConfiguration.CreateDefault();
            }
            else
            {
                mGameConfiguration = aGameConfiguration;
            }

            await Context.Database.EnsureDeletedAsync();
            await Context.Database.EnsureCreatedAsync();

            using (DatabaseContext databaseContext = Context)
            {
                mUniverses = await databaseContext.Universes.ToListAsync();
                mUsers = await databaseContext.Users.ToListAsync();
            }
        }
    }
}
