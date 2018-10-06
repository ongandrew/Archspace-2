using Newtonsoft.Json;
using System.Collections.Generic;
using Universal.Common.Serialization;

namespace Archspace2
{
    public class Configuration : JsonSerializable<Configuration>
    {
        [JsonProperty("Armors")]
        public List<Armor> Armors { get; internal set; }
        [JsonProperty("Computers")]
        public List<Computer> Computers { get; internal set; }
        [JsonProperty("Devices")]
        public List<Device> Devices { get; internal set; }
        [JsonProperty("Engines")]
        public List<Engine> Engines { get; internal set; }
        [JsonProperty("Events")]
        public List<Event> Events { get; internal set; }
        [JsonProperty("PlanetAttributes")]
        public List<PlanetAttribute> PlanetAttributes { get; internal set; }
        [JsonProperty("Projects")]
        public List<Project> Projects { get; internal set; }
        [JsonProperty("Races")]
        public List<Race> Races { get; internal set; }
        [JsonProperty("Shields")]
        public List<Shield> Shields { get; internal set; }
        [JsonProperty("ShipClasses")]
        public List<ShipClass> ShipClasses { get; internal set; }
        [JsonProperty("SpyActions")]
        public List<SpyAction> SpyActions { get; internal set; }
        [JsonProperty("Techs")]
        public List<Tech> Techs { get; internal set; }
        [JsonProperty("Weapons")]
        public List<Weapon> Weapons { get; internal set; }

        public Configuration()
        {
            Armors = new List<Armor>();
            Computers = new List<Computer>();
            Devices = new List<Device>();
            Engines = new List<Engine>();
            Events = new List<Event>();
            PlanetAttributes = new List<PlanetAttribute>();
            Projects = new List<Project>();
            Races = new List<Race>();
            Shields = new List<Shield>();
            ShipClasses = new List<ShipClass>();
            SpyActions = new List<SpyAction>();
            Techs = new List<Tech>();
            Weapons = new List<Weapon>();
        }
    }
}