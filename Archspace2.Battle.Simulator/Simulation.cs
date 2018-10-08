using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Universal.Common.Serialization;

namespace Archspace2.Battle.Simulator
{
    public interface ISimulationBattlefield
    {
        Battlefield Battlefield { get; }
    }

    public interface ISimulationAttacker
    {
        Player AttackingPlayer { get; }
        Armada AttackingArmada { get; }
    }

    public interface ISimulationDefender
    {
        Player DefendingPlayer { get; }
        Armada DefendingArmada { get; }
    }

    public class Simulation : JsonSerializable<Simulation>, ISimulationAttacker, ISimulationDefender, ISimulationBattlefield
    {
        [JsonProperty("Configuration")]
        public Configuration Configuration { get; protected set; }
        [JsonProperty("Battlefield")]
        public Battlefield Battlefield { get; protected set; }
        [JsonProperty("AttackingPlayer")]
        public Player AttackingPlayer { get; protected set; }
        [JsonProperty("AttackingArmada")]
        public Armada AttackingArmada { get; protected set; }
        [JsonProperty("DefendingPlayer")]
        public Player DefendingPlayer { get; protected set; }
        [JsonProperty("DefendingArmada")]
        public Armada DefendingArmada { get; protected set; }

        public Simulation(Configuration configuration)
        {
            Configuration = configuration;
        }

        public Simulation SetBattlefield(Battlefield battlefield)
        {
            Battlefield = battlefield;
            return this;
        }

        public Simulation SetAttacker(Player player, Armada armada)
        {
            AttackingPlayer = player;
            AttackingArmada = armada;
            return this;
        }

        public Simulation SetDefender(Player player, Armada armada)
        {
            DefendingPlayer = player;
            DefendingArmada = armada;
            return this;
        }

        public Battle Build()
        {
            Archspace2.Battle.Player attackingPlayer = CreateBattlePlayer(AttackingPlayer);
            Archspace2.Battle.Player defendingPlayer = CreateBattlePlayer(DefendingPlayer);
            Archspace2.Battle.Battlefield battlefield = CreateBattleBattlefield(Battlefield);
            Archspace2.Battle.Armada attackingArmada = CreateBattleArmada(attackingPlayer, AttackingArmada);
            Archspace2.Battle.Armada defendingArmada = CreateBattleArmada(defendingPlayer, DefendingArmada);

            return new Battle(BattleType.Siege, attackingPlayer, defendingPlayer, battlefield, attackingArmada, defendingArmada);
        }

        protected Archspace2.Battle.Player CreateBattlePlayer(Player player)
        {
            return new Archspace2.Battle.Player(player.Id, player.Name, Configuration.Races.Single(x => x.Id == player.Race), Configuration.Races.Single(x => x.Id == player.Race).BaseTraits);
        }

        protected Archspace2.Battle.Battlefield CreateBattleBattlefield(Battlefield battlefield)
        {
            return new Archspace2.Battle.Battlefield(0, battlefield.Name);
        }

        protected Archspace2.Battle.Armada CreateBattleArmada(Archspace2.Battle.Player player, Armada armada)
        {
            Archspace2.Battle.Armada result = new Archspace2.Battle.Armada(player);
            foreach (Deployment deployment in armada)
            {
                result.Add(CreateBattleFleet(player, deployment));
            }
            return result;
        }

        protected Archspace2.Battle.Fleet CreateBattleFleet(Archspace2.Battle.Player player, Deployment deployment)
        {
            Archspace2.Battle.Fleet result = new Archspace2.Battle.Fleet(
                deployment.Fleet.Id,
                deployment.Fleet.Name,
                player,
                Configuration.ShipClasses.Single(x => x.Id == deployment.Fleet.Design.ShipClass),
                Configuration.Armors.Single(x => x.Id == deployment.Fleet.Design.Armor),
                Configuration.Computers.Single(x => x.Id == deployment.Fleet.Design.Computer),
                Configuration.Engines.Single(x => x.Id == deployment.Fleet.Design.Engine),
                Configuration.Shields.Single(x => x.Id == deployment.Fleet.Design.Shield),
                deployment.Fleet.Design.Devices.Select(x => Configuration.Devices.Single(y => y.Id == x)).ToList(),
                deployment.Fleet.Design.Weapons.Select(x => Configuration.Weapons.Single(y => y.Id == x)).ToList(),
                CreateBattleAdmiral(deployment.Fleet.Admiral),
                deployment.Fleet.ShipCount,
                0,
                deployment.IsCapital,
                deployment.X,
                deployment.Y,
                deployment.Direction,
                (Command)deployment.Command
                );

            int totalLevel = 0;

            List<ShipComponent> components = new List<ShipComponent>()
                {
                    result.Armor,
                    result.Engine,
                    result.Computer,
                    result.Shield
                };

            components.AddRange(result.Turrets);

            totalLevel += components.Sum(x => x.TechLevel);

            components.AddRange(result.Devices);

            totalLevel += result.Devices.Count * 5;

            result.Power = (long)((result.ShipClass.Space / 100.0) * (2.5 + ((totalLevel / components.Count) / 2.0)));

            return result;
        }

        protected Archspace2.Battle.Admiral CreateBattleAdmiral(Admiral admiral)
        {
            Archspace2.Battle.Admiral result = new Archspace2.Battle.Admiral(
                0,
                "Admiral",
                admiral.Level,
                (AdmiralSpecialAbility)admiral.SpecialAbility,
                (AdmiralRacialAbility)admiral.RacialAbility,
                100,
                admiral.Level,
                admiral.Level,
                new AdmiralSkills()
                {
                    Blockade = admiral.Level,
                    BreakBlockade = admiral.Level,
                    Detection = admiral.Level,
                    Interpretation = admiral.Level,
                    Maneuver = admiral.Level,
                    PreventRaid = admiral.Level,
                    Privateer = admiral.Level,
                    Raid = admiral.Level,
                    SiegePlanet = admiral.Level,
                    SiegeRepel = admiral.Level
                },
                ArmadaClass.A);
            return result;
        }
    }
}
