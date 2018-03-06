using Newtonsoft.Json;
using System.Collections.Generic;

namespace Archspace2.Web
{
    public class DisbandFleetsRequest
    {
        [JsonProperty("ids")]
        public List<int> Ids { get; set; }
    }
}
