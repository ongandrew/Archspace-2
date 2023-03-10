using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing;

namespace Archspace2
{
    public enum RaceType
    {
        Human = 1,
        Targoid,
        Buckaneer,
        Tecanoid,
        Evintos,
        Agerus,
        Bosalian,
        Xeloss,
        Xerusian,
        Xesperados
    }

    public enum SocietyType
    {
        Unknown,
        Personalism,
        Classism,
        Totalism
    };

    public enum RacialTrait
    {
        GeneticEngineeringSpecialist,
        FragileMindStructure,
        GreatSpawningPool,
        FastManeuver,
        Stealth,
        InformationNetworkSpecialist,
        Scavenger,
        NoBreath,
        EfficientInvestment,
        DownloadableCommanderExperience,
        NoSpy,
        AsteroidManagement,
        Psi,
        EnhancedPsi,
        Diplomat,
        TrainedMind,
        Pacifist,
        FanaticFleet,
        HighMorale,
        TacticalMastery
    };

    public enum AdmiralNameStyle
    {
        Normal,
        Evintos,
        Xesperados
    };

    public class Race : NamedEntity
    {
        public Race() : base()
        {
            BaseEmpireRelation = 50;
            BaseFleetEffects = new List<FleetEffect>();
            BaseTechs = new List<int>();
            BaseTraits = new List<RacialTrait>();
            AdmiralAbilities = new List<AdmiralRacialAbility>();
            AdmiralFirstNames = new List<string>();
            AdmiralLastNames = new List<string>();
        }

        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("societyType")]
        public SocietyType SocietyType { get; set; }

        [JsonProperty("homeAtmosphere")]
        public Atmosphere HomeAtmosphere { get; set; }
        [JsonProperty("homeGravity")]
        public double HomeGravity { get; set; }
        [JsonProperty("homeTemperature")]
        public int HomeTemperature { get; set; }

        [JsonProperty("baseEmpireRelation")]
        public int BaseEmpireRelation { get; set; }
        [JsonProperty("baseControlModel")]
        public ControlModel BaseControlModel { get; set; }
        [JsonProperty("baseTechs")]
        public List<int> BaseTechs { get; set; }
        [JsonProperty("baseTraits")]
        public List<RacialTrait> BaseTraits { get; set; }
        [JsonProperty("baseFleetEffects")]
        public List<FleetEffect> BaseFleetEffects { get; set; }
        [JsonProperty("admiralAbilities")]
        public List<AdmiralRacialAbility> AdmiralAbilities { get; set; }
        [JsonProperty("admiralFirstNames")]
        public List<string> AdmiralFirstNames { get; set; }
        [JsonProperty("admiralLastNames")]
        public List<string> AdmiralLastNames { get; set; }
        [JsonProperty("admiralNameStyle")]
        public AdmiralNameStyle AdmiralNameStyle { get; set; }

        [JsonProperty("color")]
        public Color Color { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
