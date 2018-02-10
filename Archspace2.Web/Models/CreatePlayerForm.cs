using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Archspace2.Web
{
    public class ChangeDistributionRatioForm
    {
        [JsonProperty("factory")]
        public int Factory { get; set; }
        [JsonProperty("researchLab")]
        public int ResearchLab { get; set; }
        [JsonProperty("militaryBase")]
        public int MilitaryBase { get; set; }
    }
}
