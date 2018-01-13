﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Archspace2
{
    public static class Game
    {
        private static bool mInitialized = false;
        private static Universe mUniverse;

        private static string mConnectionString;
        public static DatabaseContext Context
        {
            get
            {
                CheckState();
                
                return new DatabaseContext(mConnectionString);
            }
        }

        private static GameConfiguration mGameConfiguration;
        public static GameConfiguration Configuration {
            get
            {
                CheckState();

                return mGameConfiguration;
            }
        }
        
        public static Universe Universe
        {
            get
            {
                CheckState();

                return mUniverse;
            }
            private set
            {
                mUniverse = value;
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

            mInitialized = true;

            await Context.Database.EnsureDeletedAsync();
            await Context.Database.EnsureCreatedAsync();

            try
            {
                Universe universe = await Context.Universes.Where(x => x.FromDate <= DateTime.UtcNow && (x.ToDate == null || DateTime.UtcNow < x.ToDate)).SingleOrDefaultAsync();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Could not retrieve entities from database. Check database connection string.", e);
            }
        }

        public static async Task<User> CreateNewUserAsync()
        {
            CheckState();

            using (DatabaseContext databaseContext = Context)
            {
                User user = new User();

                databaseContext.Users.Add(user);

                await databaseContext.SaveChangesAsync();

                return user;
            }
        }

        public static async Task<Universe> CreateNewUniverseAsync(DateTime aFromDate, DateTime? aToDate = null)
        {
            CheckState();

            using (DatabaseContext databaseContext = Context)
            {
                Universe universe = new Universe(aFromDate, aToDate);

                if (Universe != null)
                {
                    Universe.ToDate = DateTime.UtcNow;
                }

                Universe = universe;

                databaseContext.Universes.Add(universe);
                await databaseContext.SaveChangesAsync();

                universe.Initialize();
                await databaseContext.SaveChangesAsync();

                return universe;
            }
        }

        private static void CheckState()
        {
            if (!mInitialized)
            {
                throw new InvalidOperationException("Game engine not initialized.");
            }
        }
    }
}
