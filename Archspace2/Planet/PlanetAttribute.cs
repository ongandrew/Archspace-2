using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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

    public class PlanetAttribute : NamedEntity, IControlModelModifier
    {
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PlanetAttributeType? Type { get; set; }
        [JsonProperty("controlModelModifier")]
        public ControlModel ControlModelModifier { get; set; }
    }
}
