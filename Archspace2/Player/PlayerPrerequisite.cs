using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Archspace2
{
    public enum PrerequisiteType
    {
        Race,
        RacialTrait,
        Society,
        Planet,
        Tech,
        Project,
        Power,
        Rank,
        Cluster,
        CommanderLevel,
        Fleet,
        CouncilSize,
        ResearchPoint,
        EmpireRelation,
        CouncilProject,
        HasShip,
        ShipPool,
        PopulationIncrease,
        Population,
        ConcentrationMode,
        TechAll,
        Title,
        CouncilSpeaker,
        CouncilWar,
        WarInCouncil,
        Honor,

        Environment,
        Growth,
        Research,
        Production,
        Military,
        Spy,
        Commerce,
        Efficiency,
        Genius,
        Diplomacy,
        FacilityCost
    }
    public enum PrerequisiteOperator
    {
        Equal,
        NotEqual,
        Less,
        LessEqual,
        Greater,
        GreaterEqual
    }

    public class PlayerPrerequisite
    {
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PrerequisiteType Type { get; set; }
        [JsonProperty("operator")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PrerequisiteOperator? Operator { get; set; }

        [JsonProperty("value")]
        public object Value { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
