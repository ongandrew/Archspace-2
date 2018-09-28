using Newtonsoft.Json;
using System.Collections.Generic;

namespace Archspace2.Web
{
    public class DiplomacyManagementRequest
    {
        [JsonProperty("action")]
        public PlayerMessageType Action { get; set; }
        [JsonProperty("ids")]
        public List<int> Ids { get; set; }
    }
}
