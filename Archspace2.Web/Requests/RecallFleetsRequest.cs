using Newtonsoft.Json;
using System.Collections.Generic;

namespace Archspace2.Web
{
    public class RecallFleetsRequest
    {
        [JsonProperty("ids")]
        public List<int> Ids { get; set; }
    }
}
