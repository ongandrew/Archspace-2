using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Archspace2.Web
{
    public class SaveDefensePlanRequest
    {
        [JsonProperty("deployments")]
        public List<Deployment> Deployments { get; set; }
    }
}
