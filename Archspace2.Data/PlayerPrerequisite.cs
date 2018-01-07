using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public bool Evaluate(Player aPlayer)
        {
            switch(Type)
            {
                case PrerequisiteType.Race:
                    return EvaluateRacePrerequisite(aPlayer);
                case PrerequisiteType.RacialTrait:
                    return EvaluateRacialTraitPrerequisite(aPlayer);
                case PrerequisiteType.Society:
                    return EvaluateSocietyPrerequisite(aPlayer);
                case PrerequisiteType.Planet:
                    throw new NotImplementedException();
                case PrerequisiteType.Tech:
                    return EvaluateTechPrerequisite(aPlayer);
                default:
                    return false;
            }
        }

        private bool EvaluateRacePrerequisite(Player aPlayer)
        {
            return aPlayer.Race.Id == (int)Value;
        }

        private bool EvaluateRacialTraitPrerequisite(Player aPlayer)
        {
            return aPlayer.Race.BaseTraits.Contains((RacialTrait)Value);
        }

        private bool EvaluateSocietyPrerequisite(Player aPlayer)
        {
            return aPlayer.Race.SocietyType == (SocietyType)Value;
        }

        private bool EvaluateTechPrerequisite(Player aPlayer)
        {
            return aPlayer.Techs.Any(x => x.Id == (int)Value);
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
