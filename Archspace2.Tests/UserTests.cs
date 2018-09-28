using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Archspace2
{
    [TestClass]
    [TestCategory("User")]
    public class UserTests
    {
        [TestMethod]
        public async Task CanCreateNewUser()
        {
            User user = await Game.CreateNewUserAsync();

            Assert.IsNotNull(user);
        }
    }
}
