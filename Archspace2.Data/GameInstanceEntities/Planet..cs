using Archspace2.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Archspace2
{
    public enum PlanetSize
    {
        Tiny,
        Small,
        Medium,
        Large,
        Huge
    };

    public enum PlanetResource
    {
        UltraPoor,
        Poor,
        Normal,
        Rich,
        UltraRich
    };

    public enum GasType
    {
        H2,
        Cl2,
        CO2,
        O2,
        N2,
        CH4,
        H2O
    };

    public class Planet : UniverseEntity
    {
        public Planet()
        {
            PlanetAttributes = new List<PlanetAttribute>();
        }

        public int ClusterId { get; set; }
        [ForeignKey("ClusterId")]
        public Cluster Cluster { get; set; }

        public int? PlayerId { get; set; }
        [ForeignKey("PlayerId")]
        public Player Player { get; set; }

        public int Population { get; set; }

        public PlanetSize Size { get; set; }
        public PlanetResource Resource { get; set; }

        public int Temperature { get; set; }
        public double Gravity { get; set; }
        public Atmosphere Atmosphere { get; set; }

        public int Investment { get; set; }

        public string PlanetAttributeList { get; set; }

        [NotMapped]
        public List<PlanetAttribute> PlanetAttributes
        {
            get
            {
                return PlanetAttributeList.DeserializeIds().Select(x => Game.Configuration.PlanetAttributes.Single(y => y.Id == x)).ToList();
            }
            set
            {
                PlanetAttributeList = value.Select(x => x.Id).SerializeIds();
            }
        }
    }
}
