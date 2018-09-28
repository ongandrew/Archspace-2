using Newtonsoft.Json;

namespace Archspace2.Web
{
    public class FormNewFleetForm
    {
        [JsonProperty("order")]
        public int Order { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("admiralId")]
        public int AdmiralId { get; set; }
        [JsonProperty("shipDesignId")]
        public int ShipDesignId { get; set; }
        [JsonProperty("number")]
        public int Number { get; set; }
    }
}
