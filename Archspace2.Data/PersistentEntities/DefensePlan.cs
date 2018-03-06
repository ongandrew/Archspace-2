using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archspace2
{
    [Table("DefensePlan")]
    public class DefensePlan : UniverseEntity
    {
        public DefensePlan(Universe aUniverse) : base(aUniverse)
        {
            DefenseDeployments = new List<DefenseDeployment>();
        }

        public int PlayerId { get; set; }
        public Player Player { get; set; }

        public ICollection<DefenseDeployment> DefenseDeployments { get; set; }

        public async Task AddDefenseDeploymentAsync(DefenseDeployment aDefenseDeployment)
        {
            using (DatabaseContext databaseContext = Game.GetContext())
            {
                DefenseDeployments.Add(aDefenseDeployment);

                await databaseContext.SaveChangesAsync();
            }
        }
    }
}
