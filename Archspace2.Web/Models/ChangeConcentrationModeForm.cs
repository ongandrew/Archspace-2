using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Archspace2.Web
{
    public class ChangeConcentrationModeForm
    {
        [JsonProperty("mode")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ConcentrationMode Mode { get; set; }
    }
}
