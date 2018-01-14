using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public enum PlanetAttributeType
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

    public class PlanetAttribute : Entity, IControlModelModifier
    {
        [JsonProperty("Type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PlanetAttributeType? Type { get; set; }
        [JsonProperty("ControlModelModifier")]
        public ControlModel ControlModelModifier { get; set; }
    }
}
