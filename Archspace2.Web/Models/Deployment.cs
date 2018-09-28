using Newtonsoft.Json;

namespace Archspace2.Web
{
    public class Deployment
    {
        [JsonProperty("fleetId")]
        public int FleetId { get; set; }
        [JsonProperty("fleetDisplayName")]
        public string FleetDisplayName { get; set; }
        [JsonProperty("x")]
        public double X { get; set; }
        [JsonProperty("y")]
        public double Y { get; set; }
        [JsonProperty("angle")]
        public double Angle { get; set; }
        [JsonProperty("isCapitalFleet")]
        public bool IsCapitalFleet { get; set; }
        [JsonProperty("command")]
        public Command Command { get; set; }
    }
}
