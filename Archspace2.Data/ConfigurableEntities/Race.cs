using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public enum SocietyType
    {
        Unknown,
        Personalism,
        Classism,
        Totalism
    }

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

    public class Race : Entity
    {
        public Race() : base()
        {
            BaseEmpireRelation = 50;
            BaseFleetEffects = new List<FleetEffect>();
            BaseTechs = new List<int>();
            BaseTraits = new List<RacialTrait>();
        }

        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("societyType")]
        public SocietyType SocietyType { get; set; }

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

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
