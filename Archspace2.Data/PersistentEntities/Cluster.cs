﻿using Archspace2.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Name = $"{Game.Configuration.Universe.ClusterNames.Random()} {Game.Random.Next(1, 10).ToRoman()}";
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
