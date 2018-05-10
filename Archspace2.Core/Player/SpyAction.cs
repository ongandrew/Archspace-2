using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public enum SpyId
    {
        GeneralInformationGathering = 8001,
        DetailedInformationGathering = 8002,
        StealSecretInfo = 8003,
        ComputerVirusInfiltration = 8004,
        DevastatingNetworkWorm = 8005,
        Sabotage = 8006,
        InciteRiot = 8007,
        StealCommonTechnology = 8008,
        ArtificialDisease = 8009,
        RedDeath = 8010,
        StrikeBase = 8011,
        MeteorStrike = 8012,
        EmpStorm = 8013,
        StellarBombardment = 8014,
        Assassination = 8015,
        StealImportantTechnology = 8016,
        StealSecretTechnology = 8017
    };

    public enum SpyType
    {
        Acceptable,
        Ordinary,
        Hostile,
        Atrocious
    };

    public class SpyAction : NamedEntity, IPlayerUnlockable
    {
        public SpyAction()
        {
            Prerequisites = new List<PlayerPrerequisite>();
        }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("difficulty")]
        public int Difficulty { get; set; }
        [JsonProperty("cost")]
        public int Cost { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SpyType Type { get; set; }

        [JsonProperty("prerequisites")]
        public List<PlayerPrerequisite> Prerequisites { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
