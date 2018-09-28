using Newtonsoft.Json;
using System.Collections.Generic;

namespace Archspace2.Web
{
    public class SaveDefensePlanRequest
    {
        [JsonProperty("deployments")]
        public List<Deployment> Deployments { get; set; }
    }
}
