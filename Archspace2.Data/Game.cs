﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Archspace2
{
    public static class Game
    {
        private static bool mInitialized = false;
        private static bool mRunning = false;
        private static Thread mMainThread;
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

        private static Random mRandom = new Random();
        public static Random Random { get => mRandom; }

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

        public static async Task LoadUniverseAsync()
        {
            CheckState();

            using (DatabaseContext databaseContext = Context)
            {
                Universe universe = await databaseContext.Universes.Where(x => x.FromDate <= DateTime.UtcNow && (DateTime.UtcNow < x.ToDate || x.ToDate == null)).SingleAsync();

                if (universe != null)
                {
                    Universe = universe;
                }
                else
                {
                    throw new InvalidOperationException("No valid universe exists in the database.");
                }
            }
        }

        private static void CheckState()
        {
            if (!mInitialized)
            {
                throw new InvalidOperationException("Game engine not initialized.");
            }
        }

        public static async Task LogAsync(string aMessage, LogType aLogType = LogType.Information, [CallerFilePath]string aCallerFilePath = null, [CallerMemberName]string aCallerMemberName = null, [CallerLineNumber]int aCallerLineNumber = 0)
        {
            using (DatabaseContext databaseContext = Context)
            {
                databaseContext.SystemLogs.Add(new SystemLog(aMessage, aLogType, aCallerFilePath, aCallerMemberName, aCallerLineNumber));

                await databaseContext.SaveChangesAsync();
            }
        }

        public static void Start()
        {
            CheckState();

            mRunning = true;

            if (mMainThread != null)
            {
                mMainThread.Abort();
            }

            mMainThread = new Thread(() => Run());
            mMainThread.Start();
        }

        private static void Run()
        {
            if (mRunning)
            {
                Timer timer = new Timer();
                timer.Elapsed += new ElapsedEventHandler(UpdateUniverseEvent);

                timer.Interval = 15000;
                //timer.Interval = Configuration.SecondsPerTurn * 1000;
                timer.AutoReset = true;
                timer.Start();
            }
        }

        private static async void UpdateUniverseEvent(object source, ElapsedEventArgs e)
        {
            try
            {
                using (DatabaseContext context = Context)
                {
                    context.Attach(Universe);

                    Universe.UpdateTurn();

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception exception)
            {
                await LogAsync(exception.ToString(), LogType.Error);
            }
        }
    }
}
