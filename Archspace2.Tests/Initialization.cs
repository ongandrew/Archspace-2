using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Archspace2
{
    [TestClass]
    public class Initialization
    {
        [AssemblyInitialize]
        public static async Task AssemblyInit(TestContext context)
        {
            await Game.InitializeAsync("Server=.;Initial Catalog=Archspace 2 Tests;Integrated Security=True;TrustServerCertificate=True;");
			await Game.CreateNewUniverseAsync(DateTime.UtcNow, DateTime.UtcNow.AddYears(1));
        }
    }
}
