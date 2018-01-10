using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Archspace2
{
    public enum PlanetAttribute
    {
        Artifact,
        MassiveArtifact,
        Asteroid,
        Moon,
        Radiation,
        SevereRadiation,
        HostileMonster,
        ObstinateMicrobe,
        BeautifulLandscape,
        BlackHole,
        Nebula,
        DarkNebula,
        VolcanicActivity,
        IntenseVolcanicActivity,
        Ocean,
        IrregularClimate,
        MajorSpaceRoute,
        MajorSpaceCrossroute,
        FrontierArea,
        GravityControlled
    }

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
        public int ClusterId { get; set; }
        [ForeignKey("ClusterId")]
        public Cluster Cluster { get; set; }

        public int? OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public Player Owner { get; set; }

        public int Population { get; set; }

        public PlanetSize Size { get; set; }
        public PlanetResource Resource { get; set; }

        public int Temperature { get; set; }
        public double Gravity { get; set; }
        public Atmosphere Atmosphere { get; set; }

        public int Investment { get; set; }
    }
}
