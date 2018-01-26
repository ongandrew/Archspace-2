using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

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

        [JsonProperty("Description")]
        public string Description { get; set; }
        [JsonProperty("SocietyType")]
        public SocietyType SocietyType { get; set; }

        [JsonProperty("HomeAtmosphere")]
        public Atmosphere HomeAtmosphere { get; set; }
        [JsonProperty("HomeGravity")]
        public double HomeGravity { get; set; }
        [JsonProperty("HomeTemperature")]
        public int HomeTemperature { get; set; }

        [JsonProperty("BaseEmpireRelation")]
        public int BaseEmpireRelation { get; set; }
        [JsonProperty("BaseControlModel")]
        public ControlModel BaseControlModel { get; set; }
        [JsonProperty("BaseTechs")]
        public List<int> BaseTechs { get; set; }
        [JsonProperty("BaseTraits")]
        public List<RacialTrait> BaseTraits { get; set; }
        [JsonProperty("BaseFleetEffects")]
        public List<FleetEffect> BaseFleetEffects { get; set; }
        [JsonProperty("AdmiralAbilities")]
        public List<AdmiralRacialAbility> AdmiralAbilities { get; set; }
        [JsonProperty("AdmiralFirstNames")]
        public List<string> AdmiralFirstNames { get; set; }
        [JsonProperty("AdmiralLastNames")]
        public List<string> AdmiralLastNames { get; set; }
        [JsonProperty("AdmiralNameStyle")]
        public AdmiralNameStyle AdmiralNameStyle { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
