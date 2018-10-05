using Archspace2.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Universal.Common.Extensions;

namespace Archspace2
{
    [Table("Cluster")]
    public class Cluster : UniverseEntity
    {
        public string Name { get; set; }
        public ICollection<Planet> Planets { get; set; }

        public Cluster(Universe aUniverse) : base(aUniverse)
        {
            Name = $"{Game.Configuration.Universe.ClusterNames.Random()} {RandomNumberGenerator.Next(1, 10).ToRoman()}";
            Planets = new List<Planet>();
        }

        public Planet CreatePlanet()
        {
            Planet planet = new Planet(Universe);
            planet.Cluster = this;
            planet.Name = $"{Name}-{Planets.Count + 1}";

            return planet;
        }
    }
}
