using Archspace2.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Archspace2
{
    public enum ConcentrationMode
    {
        Balanced,
        Industry,
        Military,
        Research
    }

    public enum SecurityLevel
    {
        Defenseless = 1,
        Loose,
        Wary,
        Alerted,
        Impenetrable
    };

    public class Player : UniverseEntity
    {
        public Player() : base()
        {
            Techs = new List<Tech>();
            Admirals = new List<Admiral>();
            Planets = new List<Planet>();
        }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public int CouncilId { get; set; }
        [ForeignKey("CouncilId")]
        public Council Council { get; set; }

        public SecurityLevel SecurityLevel { get; set; }
        public int Alertness { get; set; }

        public int ProductionPoint { get; set; }
        public int ResearchPoint { get; set; }
        public int ResearchInvestment { get; set; }
        public int ShipProductionInvestment { get; set; }
        public int PlanetInvestmentPool { get; set; }

        public int RaceId { get; set; }
        [NotMapped]
        public Race Race
        {
            get
            {
                return Game.Configuration.Races.Single(x => x.Id == RaceId);
            }
            set
            {
                RaceId = value.Id;
            }
        }

        public ConcentrationMode ConcentrationMode { get; set; }

        public int? TargetTechId { get; set; }
        [NotMapped]
        public Tech TargetTech
        {
            get
            {
                return TargetTechId == null ? null : Game.Configuration.Techs.Single(tech => tech.Id == TargetTechId);
            }
            set
            {
                TargetTechId = value == null ? null : (int?)value.Id;
            }
        }

        public string TechIdList { get; private set; }
        [NotMapped]
        public List<Tech> Techs
        {
            get
            {
                return TechIdList.DeserializeIds().Select(x => Game.Configuration.Techs.Single(tech => tech.Id == x)).ToList();
            }
            set
            {
                TechIdList = value.Select(x => x.Id).SerializeIds();
            }
        }

        public string ProjectIdList { get; private set; }
        [NotMapped]
        public List<Project> Projects
        {
            get
            {
                return ProjectIdList.DeserializeIds().Select(x => Game.Configuration.Projects.Single(project => project.Id == x)).ToList();
            }
            set
            {
                ProjectIdList = value.Select(x => x.Id).SerializeIds();
            }
        }

        [NotMapped]
        public ControlModel ControlModel
        {
            get
            {
                ControlModel result = Race.BaseControlModel;

                result += Techs.CalculateControlModelModifier();
                result += Projects.CalculateControlModelModifier();
                result += Council.Projects.CalculateControlModelModifier();

                if (ResearchInvestment > 0)
                {
                    int maxPerTurn = Planets.GetTotalResearchLabCount() * 10;

                    if (ResearchInvestment > maxPerTurn)
                    {
                        result.Research += 3;
                    }
                    else
                    {
                        int ratio = (int)(ResearchInvestment * 100 / maxPerTurn);

                        if (ratio > 66)
                        {
                            result.Research += 2;
                        }
                        else if (ratio > 33)
                        {
                            result.Research += 1;
                        }
                    }
                }

                if (Planets.Any(x => x.PlanetAttributes.Select(y => y.Type).Contains(PlanetAttributeType.MajorSpaceCrossroute)))
                {
                    result.Commerce += 1;
                }

                result += ConcentrationMode.GetControlModelModifier();

                return result;
            }
        }

        public int MailboxId { get; set; }
        [ForeignKey("MailboxId")]
        public Mailbox Mailbox { get; set; }

        public bool EvaluatePrerequisites(IPlayerUnlockable aPlayerUnlockable)
        {
            return EvaluatePrerequisites(aPlayerUnlockable.Prerequisites);
        }
        public bool EvaluatePrerequisites(List<PlayerPrerequisite> aPrerequisites)
        {
            return aPrerequisites.Evaluate(this);
        }
        
        public ICollection<Admiral> Admirals { get; set; }
        public ICollection<DefensePlan> DefensePlans { get; set; }
        public ICollection<Planet> Planets { get; set; }

        public async Task AddPlanetAsync(Planet aPlanet)
        {
            using (DatabaseContext databaseContext = Game.Context)
            {
                aPlanet.Player = this;
                Planets.Add(aPlanet);

                await databaseContext.SaveChangesAsync();
            }
        }

        public async Task RemovePlanetAsync(Planet aPlanet)
        {
            using (DatabaseContext databaseContext = Game.Context)
            {
                aPlanet.Player = null;
                await aPlanet.ClearCommerceAsync();
                Planets.Add(aPlanet);

                await databaseContext.SaveChangesAsync();
            }
        }

        public int GetTargetTechCost()
        {
            int baseCost = TargetTech.GetBaseCost();

            int discount = 0;
            if (TargetTech == null)
            {
                discount = 20 + (ControlModel.Research * 4);
            }

            if (discount < 0)
            {
                discount = 0;
            }
            else if (discount > 40)
            {
                discount = 40;
            }

            int cost = (int)(baseCost * ((100 - discount) / 100.0));

            cost = (int)(cost / Game.Configuration.TechRateModifier);

            if (cost < 0)
            {
                cost = int.MaxValue;
            }

            return cost;
        }

        public async Task UpdateTurnAsync()
        {
            using (DatabaseContext databaseContext = Game.Context)
            {
                UpdateResearch();

                UpdateSecurity();

                await databaseContext.SaveChangesAsync();
            }
        }

        private void UpdateResearch()
        {
            int invest;

            if (Race.BaseTraits.Contains(RacialTrait.EfficientInvestment))
            {
                invest = Planets.GetTotalResearchLabCount() * 10 * 2;
            }
            else
            {
                invest = Planets.GetTotalResearchLabCount();
            }

            int rp = 0;

            if (ResearchInvestment < invest)
            {
                invest = ResearchInvestment;
            }

            ResearchInvestment = invest;

            rp = invest / 20;
            ResearchPoint += rp;
        }

        private void UpdateSecurity()
        {
            Alertness -= 5;
            if (Alertness < 0)
            {
                Alertness = 0;
            }
        }
    }
}
