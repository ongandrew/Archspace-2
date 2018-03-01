using Newtonsoft.Json;

namespace Archspace2.Web
{
    public class GameStatusResponse
    {
        [JsonProperty("isRunning")]
        public bool IsRunning { get; set; }
        [JsonProperty("currentUniverseId")]
        public int? CurrentUniverseId { get; set; }
        [JsonProperty("currentTurn")]
        public int? CurrentTurn { get; set; }
    }
}
