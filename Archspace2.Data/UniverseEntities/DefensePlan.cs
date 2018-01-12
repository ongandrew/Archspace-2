using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archspace2
{
    public class DefensePlan : UniverseEntity
    {
        protected DefensePlan()
        {
        }

        public int PlayerId { get; set; }
        public Player Player { get; set; }

        public ICollection<DefenseDeployment> DefenseDeployments { get; set; }

        public async Task AddDefenseDeploymentAsync(DefenseDeployment aDefenseDeployment)
        {
            using (DatabaseContext databaseContext = Game.Context)
            {
                DefenseDeployments.Add(aDefenseDeployment);

                await databaseContext.SaveChangesAsync();
            }
        }
    }
}
