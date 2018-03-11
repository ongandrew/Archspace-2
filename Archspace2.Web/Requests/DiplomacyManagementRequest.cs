using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
