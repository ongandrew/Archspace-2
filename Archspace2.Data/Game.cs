using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
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
        public static bool IsRunning()
        {
            return mRunning;
        }

        private static Thread mMainThread;
        private static Universe mUniverse;
        
        private static string mConnectionString;

        private static Random mRandom = new Random();
        public static Random Random { get => mRandom; }

        private static GameConfiguration mGameConfiguration;
        public static GameConfiguration Configuration {
            get
            {
                return mGameConfiguration;
            }
        }
        
        public static Universe Universe
        {
            get
            {
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

            using (DatabaseContext context = GetContext())
            {
                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();

                try
                {
                    Universe universe = await context.Universes.Where(x => x.FromDate <= DateTime.UtcNow && (x.ToDate == null || DateTime.UtcNow < x.ToDate)).SingleOrDefaultAsync();
                    if (universe == null)
                    {
                        await CreateNewUniverseAsync(DateTime.UtcNow);
                    }
                }
                catch (Exception e)
                {
                    throw new InvalidOperationException("Could not retrieve entities from database. Check database connection string.", e);
                }
            }
        }

        public static async Task<User> CreateNewUserAsync()
        {
            using (DatabaseContext databaseContext = GetContext())
            {
                User user = new User();

                databaseContext.Users.Add(user);

                await databaseContext.SaveChangesAsync();

                return user;
            }
        }

        public static async Task<Universe> CreateNewUniverseAsync(DateTime aFromDate, DateTime? aToDate = null)
        {
            using (DatabaseContext databaseContext = GetContext())
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
            using (DatabaseContext databaseContext = GetContext())
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

        public static async Task LogAsync(object aObject, LogType aLogType = LogType.Information, [CallerFilePath]string aCallerFilePath = null, [CallerMemberName]string aCallerMemberName = null, [CallerLineNumber]int aCallerLineNumber = 0)
        {
            using (DatabaseContext databaseContext = GetContext())
            {
                databaseContext.SystemLogs.Add(new SystemLog(aObject.ToString(), aLogType, aCallerFilePath, aCallerMemberName, aCallerLineNumber));

                await databaseContext.SaveChangesAsync();
            }
        }

        public static async Task LogAsync(Exception aException, LogType aLogType = LogType.Error, [CallerFilePath]string aCallerFilePath = null, [CallerMemberName]string aCallerMemberName = null, [CallerLineNumber]int aCallerLineNumber = 0)
        {
            using (DatabaseContext databaseContext = GetContext())
            {
                databaseContext.SystemLogs.Add(new SystemLog(aException.ToString(), aLogType, aCallerFilePath, aCallerMemberName, aCallerLineNumber));

                await databaseContext.SaveChangesAsync();
            }
        }

        public static async Task LogAsync(string aMessage, LogType aLogType = LogType.Information, [CallerFilePath]string aCallerFilePath = null, [CallerMemberName]string aCallerMemberName = null, [CallerLineNumber]int aCallerLineNumber = 0)
        {
            using (DatabaseContext databaseContext = GetContext())
            {
                databaseContext.SystemLogs.Add(new SystemLog(aMessage, aLogType, aCallerFilePath, aCallerMemberName, aCallerLineNumber));

                await databaseContext.SaveChangesAsync();
            }
        }

        public static void Start()
        {
            if (mMainThread != null)
            {
                throw new InvalidOperationException("Universe already running.");
            }

            mRunning = true;
            mMainThread = new Thread(() => Run());
            mMainThread.Start();
        }

        public static async Task AddOrUpdateUserAsync(ClaimsPrincipal aClaimsPrincipal)
        {
            using (DatabaseContext context = GetContext())
            {
                string email = aClaimsPrincipal.FindFirstValue(ClaimTypes.Email);

                if (email == null)
                {
                    throw new InvalidOperationException("No email claim in claims principal.");
                }
                else
                {
                    if (context.Users.Any(x => x.Email == email))
                    {
                        User user = context.Users.Single(x => x.Email == email);
                        user.Email = email;
                    }
                    else
                    {
                        context.Users.Add(new User(aClaimsPrincipal));
                    }
                }

                await context.SaveChangesAsync();
            }
        }

        public static DatabaseContext GetContext()
        {
            return new DatabaseContext(mConnectionString);
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
                using (DatabaseContext context = GetContext())
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
