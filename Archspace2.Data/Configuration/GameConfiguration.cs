using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Universal.Common.Reflection.Extensions;

namespace Archspace2
{
    public enum Language
    {
        English,
        Korean
    };

    public class GameConfiguration : Configuration, IValidatable
    {
        public GameConfiguration() : base()
        {
            Action = new ActionSettings();
            BlackMarket = new BlackMarketSettings();
            Mission = new MissionSettings();
            Planet = new PlanetSettings();
            Player = new PlayerSettings();
            Universe = new UniverseSettings();
        }
        
        [JsonProperty("Language")]
        public Language Language { get; set; }
        [JsonProperty("SecondsPerTurn")]
        public int SecondsPerTurn { get; set; }
        [JsonProperty("MaxUsers")]
        public int MaxUsers { get; set; }
        [JsonProperty("TechRateModifier")]
        public double TechRateModifier { get; set; }

        [JsonProperty("Action")]
        public ActionSettings Action { get; set; }
        [JsonProperty("Admiral")]
        public AdmiralSettings Admiral { get; set; }
        [JsonProperty("Battle")]
        public BattleSettings Battle { get; set; }
        [JsonProperty("BlackMarket")]
        public BlackMarketSettings BlackMarket { get; set; }
        [JsonProperty("Mission")]
        public MissionSettings Mission { get; set; }
        [JsonProperty("Planet")]
        public PlanetSettings Planet { get; set; }
        [JsonProperty("Player")]
        public PlayerSettings Player { get; set; }
        [JsonProperty("Universe")]
        public UniverseSettings Universe { get; set; }

        public static GameConfiguration CreateDefault()
        {
            GameConfiguration result = new GameConfiguration();
            result.UseDefaults();

            return result;
        }

        private void UseDefaults()
        {
            UseDefaultSettings();

            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder().UseDefaults();
            this.Bind(configurationBuilder.Configuration);
        }

        private void UseDefaultSettings()
        {
            Language = Language.English;
            SecondsPerTurn = 300;
            MaxUsers = 10000;
            TechRateModifier = 1;
            
            Action = ActionSettings.CreateDefault();
            Admiral = AdmiralSettings.CreateDefault();
            Battle = BattleSettings.CreateDefault();
            BlackMarket = BlackMarketSettings.CreateDefault();
            Mission = MissionSettings.CreateDefault();
            Planet = PlanetSettings.CreateDefault();
            Player = PlayerSettings.CreateDefault();
            Universe = UniverseSettings.CreateDefault();
        }

        public ValidateResult Validate()
        {
            ValidateResult result = new ValidateResult();

            List<Entity> entities = new List<Entity>();
            entities.AddRange(Armors);
            entities.AddRange(Computers);
            entities.AddRange(Devices);
            entities.AddRange(Engines);
            entities.AddRange(Events);
            entities.AddRange(Projects);
            entities.AddRange(Races);
            entities.AddRange(Shields);
            entities.AddRange(ShipClasses);
            entities.AddRange(SpyActions);
            entities.AddRange(Techs);
            entities.AddRange(Weapons);

            IEnumerable<int> repeatedIds = entities.GroupBy(x => x.Id).Where(x => x.Count() > 1).Select(x => x.Key);
            if (repeatedIds.Any())
            {
                result.Items.AddRange(repeatedIds.Select(x => new ValidateResult.Item() { Severity = Severity.Warning, Message = $"Id {x} is repeated across all configurable entities." }));
            }

            foreach (Type type in entities.Select(x => x.GetType()).Distinct())
            {
                repeatedIds = entities.Where(x => x.GetType().Equals(type)).GroupBy(x => x.Id).Where(x => x.Count() > 1).Select(x => x.Key);
                if (repeatedIds.Any())
                {
                    result.Items.AddRange(repeatedIds.Select(x => new ValidateResult.Item() { Severity = Severity.Error, Message = $"Id {x} is repeated within {type.Name} entities." }));
                }
            }

            IEnumerable<IPlayerUnlockable> unlockables = entities.Where(x => x.GetType().GetInterfaces().Contains(typeof(IPlayerUnlockable))).Cast<IPlayerUnlockable>();
            IEnumerable<IPlayerUnlockable> unobtainable = unlockables.Where(x => x.Prerequisites.Where(y => y.Type == PrerequisiteType.Tech).Select(y => y.Value).Cast<int>().Except(Techs.Select(z => z.Id)).Any());

            if (unobtainable.Any())
            {
                result.Items.AddRange(unobtainable.Select(x => new ValidateResult.Item() { Severity = Severity.Error, Message = $"{x.GetType().Name} {((Entity)x).Id} requires a tech that is not defined." }));
            }

            return result;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
