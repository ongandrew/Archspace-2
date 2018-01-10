using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Archspace2
{
    public class UniverseEntity : Entity
    {
        [JsonProperty("universeId")]
        public int UniverseId { get; set; }
        [JsonIgnore]
        public Universe Universe { get; set; }
    }
}
