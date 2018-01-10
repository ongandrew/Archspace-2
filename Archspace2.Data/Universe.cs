using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public sealed class Universe : Entity
    {
        public Universe()
        {
        }
        
        public Player CreateNewPlayer(string aName, Race aRace)
        {
            Player player = new Player()
            {
                Name = aName,
                UniverseId = Id,
                Race = aRace
            };

            Planet planet = new Planet()
            {
                Size = PlanetSize.Medium
            };

            return player;
        }

        public ICollection<Player> Players { get; set; }
        public ICollection<Cluster> Clusters { get; set; }
    }
}
