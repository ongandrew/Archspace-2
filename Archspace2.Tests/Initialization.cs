using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Archspace2
{
    [TestClass]
    public class Initialization
    {
        [AssemblyInitialize]
        public static async Task AssemblyInit(TestContext context)
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
            sqlConnectionStringBuilder.DataSource = ".";
            sqlConnectionStringBuilder.InitialCatalog = "Archspace 2 Tests";
            sqlConnectionStringBuilder.IntegratedSecurity = true;
            
            await Game.InitializeAsync(sqlConnectionStringBuilder.ToString());
            await Game.CreateNewUniverseAsync(DateTime.UtcNow, DateTime.UtcNow.AddYears(1));
        }
    }
}
