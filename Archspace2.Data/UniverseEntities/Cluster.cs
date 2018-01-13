using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universal.Common.Extensions;

namespace Archspace2
{
    public class Cluster : UniverseEntity
    {
        public Cluster(Universe aUniverse) : base(aUniverse)
        {
            Name = Game.Configuration.Universe.ClusterNames.Except(Game.Universe.Clusters.Select(x => x.Name)).Random();
            Planets = new List<Planet>();
        }

        public ICollection<Planet> Planets { get; set; }
    }
}
