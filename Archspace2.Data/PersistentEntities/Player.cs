using Archspace2.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Universal.Common.Extensions;

namespace Archspace2
{
    public enum PlayerType
    {
        Normal,
        Bot
    };

    public enum ConcentrationMode
    {
        Balanced,
        Industry,
        Military,
        Research
    };

    public enum SecurityLevel
    {
        Defenseless = 1,
        Loose,
        Wary,
        Alerted,
        Impenetrable
    };

    [Table("Player")]
    public class Player : UniverseEntity, IPowerContributor
    {
        public string Name { get; set; }

        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public PlayerType Type { get; set; }
        
        public int CouncilId { get; set; }
        [ForeignKey("CouncilId")]
        public Council Council { get; set; }

        public SecurityLevel SecurityLevel { get; set; }
        public int Alertness { get; set; }

        public Resource Resource { get; set; }
        public long ResearchInvestment { get; set; }
        public long PlanetInvestmentPool { get; set; }

        public Shipyard Shipyard { get; set; }

        private int mHonor;
        public int Honor
        {
            get => mHonor;
            set
            {
                mHonor = value < 0 ? 0 : value > 100 ? 100 : value;
            }
        }

        public long Power
        {
            get
            {
                long result = 0;

                result += Shipyard.Power;
                result += Fleets.CalculateTotalPower();
                result += Planets.CalculateTotalPower();
                result += Techs.CalculateTotalPower();

                return result;
            }
        }

        public int AdmiralTimer { get; set; }
        public int HonorTimer { get; set; }

        public int RaceId { get; set; }
        public int Turn { get; set; }
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

        public string ProjectIdList
        {
            get
            {
                return mProjects.Select(x => x.Id).SerializeIds();
            }
            private set
            {
                mProjects = value.DeserializeIds().Select(x => Game.Configuration.Projects.Single(project => project.Id == x)).ToList();
            }
        }
        [NotMapped]
        private List<Project> mProjects;
        [NotMapped]
        public List<Project> Projects
        {
            get
            {
                return mProjects;
            }
            set
            {
                ProjectIdList = value.Select(x => x.Id).SerializeIds();
            }
        }

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

        public string TechIdList {
            get
            {
                return mTechs.Select(x => x.Id).SerializeIds();
            }
            private set
            {
                mTechs = value.DeserializeIds().Select(x => Game.Configuration.Techs.Single(tech => tech.Id == x)).ToList();
            }
        }
        [NotMapped]
        private List<Tech> mTechs;
        [NotMapped]
        public List<Tech> Techs
        {
            get
            {
                return mTechs;
            }
            set
            {
                TechIdList = value.Select(x => x.Id).SerializeIds();
            }
        }

        public string TraitList
        {
            get
            {
                return mTraits.Cast<int>().SerializeIds();
            }
            private set
            {
                mTraits = value.DeserializeIds().Select(x => Enum.Parse(typeof(RacialTrait), x.ToString())).Cast<RacialTrait>().ToList();
            }
        }
        [NotMapped]
        private List<RacialTrait> mTraits;
        [NotMapped]
        public List<RacialTrait> Traits
        {
            get
            {
                return mTraits;
            }
            set
            {
                TraitList = value.Cast<int>().SerializeIds();
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

                if (Planets.Any(x => x.Attributes.Select(y => y.Type).Contains(PlanetAttributeType.MajorSpaceCrossroute)))
                {
                    result.Commerce += 1;
                }

                result += ConcentrationMode.GetControlModelModifier();

                return result;
            }
        }
        
        public PlayerMailbox Mailbox { get; set; }

        public List<Fleet> GetAlliedFleets()
        {
            return FromRelations.Where(x => x.Type == RelationType.Ally).Select(x => x.ToPlayer).Distinct().SelectMany(x => x.Fleets).Where(x => x.Mission.Type == MissionType.DispatchToAlly && x.Mission.Target == Id).ToList();
        }

        public List<PlayerRelation> Relations
        {
            get
            {
                return FromRelations.Union(ToRelations).ToList();
            }
        }

        public int? VotedCouncilPlayerId { get; set; }
        [ForeignKey("VotedCouncilPlayerId")]
        public Player VotedCouncilPlayer { get; set; }

        public long VotingPower
        {
            get
            {
                long result = Techs.Count(x => x.Type == TechType.Social);
                
                return ((Power * result) / 100);
            }
        }

        public ICollection<Admiral> Admirals { get; set; }
        public ICollection<DefensePlan> DefensePlans { get; set; }
        public ICollection<PlayerEffectInstance> Effects { get; set; }
        public ICollection<Fleet> Fleets { get; set; }
        public ICollection<NewsItem> NewsItems { get; set; }
        public ICollection<Planet> Planets { get; set; }
        public ICollection<PlayerRelation> FromRelations { get; set; }
        public ICollection<PlayerRelation> ToRelations { get; set; }
        public ICollection<ShipDesign> ShipDesigns { get; set; }

        internal Player()
        {
        }
        public Player(Universe aUniverse) : base(aUniverse)
        {
            mHonor = 50;
            Resource = new Resource()
            {
                ProductionPoint = 50000
            };

            mTechs = new List<Tech>();
            mProjects = new List<Project>();
            mTraits = new List<RacialTrait>();
            
            Shipyard = new Shipyard(this);
            Mailbox = new PlayerMailbox(this);
            NewsItems = new List<NewsItem>();

            Admirals = new List<Admiral>();
            DefensePlans = new List<DefensePlan>();
            Effects = new List<PlayerEffectInstance>();
            Fleets = new List<Fleet>();
            Planets = new List<Planet>();
            FromRelations = new List<PlayerRelation>();
            ShipDesigns = new List<ShipDesign>();
            ToRelations = new List<PlayerRelation>();
        }

        public string GetDisplayName()
        {
            return $"{Name} (#{Id})";
        }

        public bool EvaluatePrerequisites(IPlayerUnlockable aPlayerUnlockable)
        {
            return EvaluatePrerequisites(aPlayerUnlockable.Prerequisites);
        }
        public bool EvaluatePrerequisites(List<PlayerPrerequisite> aPrerequisites)
        {
            return aPrerequisites.Evaluate(this);
        }

        public List<Tech> GetAvailableTechs()
        {
            return Game.Configuration.Techs.Where(x => x.Prerequisites.Evaluate(this)).Except(Techs).ToList();
        }

        public List<Tech> GetLockedTechs()
        {
            return Game.Configuration.Techs.Except(Techs).Except(GetAvailableTechs()).ToList();
        }

        public List<Project> GetAvailableProjects()
        {
            return Game.Configuration.Projects.Where(x => x.Prerequisites.Evaluate(this) && (x.Type == ProjectType.Ending || x.Type == ProjectType.Fixed || x.Type == ProjectType.Planet || x.Type == ProjectType.Secret)).Except(Projects).ToList();
        }

        public void PurchaseProject(int aProjectId)
        {
            Project project = Game.Configuration.Projects.Where(x => x.Id == aProjectId).SingleOrDefault();

            if (project != null && EvaluatePrerequisites(project))
            {
                long cost = GetProjectCost(project);

                if (Resource.ProductionPoint >= cost)
                {
                    Resource.ProductionPoint -= cost;
                    Projects.Add(project);
                }
            }
        }

        public long GetProjectCost(Project aProject)
        {
            if (aProject.Type == ProjectType.Planet)
            {
                return aProject.Cost * Planets.Count;
            }
            else
            {
                return aProject.Cost;
            }
        }

        public void AddNews(string aText)
        {
            NewsItem newsItem = new NewsItem(Universe);
            newsItem.Player = this;
            newsItem.Text = aText;

            NewsItems.Add(newsItem);
        }

        internal Admiral CreateAdmiral()
        {
            Admiral result = new Admiral(Universe).AsPlayerAdmiral(this);
            Admirals.Add(result);

            return result;
        }

        public void SpawnAdmiral()
        {
            CreateAdmiral().AsPlayerAdmiral(this);
        }

        internal Fleet CreateFleet()
        {
            Fleet result = new Fleet(Universe);
            result.Player = this;
            Fleets.Add(result);

            return result;
        }

        public Fleet CreateFleet(int aOrder, string aName, Admiral aAdmiral, ShipDesign aShipDesign, int aNumber)
        {
            Fleet fleet = CreateFleet();

            fleet.Order = aOrder;
            fleet.Name = aName;

            fleet.Admiral = aAdmiral;
            fleet.ShipDesign = aShipDesign;

            fleet.CurrentShipCount = aNumber;
            fleet.MaxShipCount = aNumber;

            return fleet;
        }

        public void FormNewFleet(int aOrder, string aName, int aAdmiralId, int aShipDesignId, int aNumber)
        {
            if (Fleets.Select(x => x.Order).Contains(aOrder))
            {
                throw new InvalidOperationException("Fleet order must be unique.");
            }

            if (aName.Trim().IsNullOrEmpty())
            {
                throw new InvalidOperationException("Fleet name cannot be null or empty.");
            }
            
            Admiral admiral = Admirals.SingleOrDefault(x => x.Id == aAdmiralId);
            if (admiral == null)
            {
                throw new InvalidOperationException("Admiral does not exist.");
            }
            else if (admiral.Fleet != null)
            {
                throw new InvalidOperationException("Admiral is current attached to a fleet.");
            }

            ShipDesign design = ShipDesigns.SingleOrDefault(x => x.Id == aShipDesignId);
            if (design == null)
            {
                throw new InvalidOperationException("Ship design does not exist or is not owned by player.");
            }
            else if (Shipyard.GetDockedShipCount(design) < 1 || Shipyard.GetDockedShipCount(design) < aNumber)
            {
                throw new InvalidOperationException("Cannot form a fleet with that number of ships.");
            }
            else if (admiral.FleetCapacity < aNumber)
            {
                throw new InvalidOperationException("Admiral cannot command that number of ships.");
            }

            Fleet fleet = CreateFleet(aOrder, aName, admiral, design, aNumber);
            fleet.Experience = 25 + (ControlModel.Military * 3);
            Shipyard.ChangeDockedShip(design, -aNumber);
        }

        public Fleet GetExpeditionFleet()
        {
            return Fleets.SingleOrDefault(x => x.Mission.Type == MissionType.Expedition || x.Mission.Type == MissionType.ReturningWithPlanet);
        }

        public List<Fleet> GetStandByFleets()
        {
            return Fleets.Where(x => x.Status == FleetStatus.StandBy).ToList();
        }

        public void SendExpedition(int aFleetId)
        {
            Fleet fleet = Fleets.SingleOrDefault(x => x.Id == aFleetId);
            if (fleet == null)
            {
                throw new InvalidOperationException($"Player does not own fleet with id {aFleetId}.");
            }
            else if (GetExpeditionFleet() != null)
            {
                throw new InvalidOperationException("There is already a fleet on expedition.");
            }
            else if (fleet.Mission.Type != MissionType.None)
            {
                throw new InvalidOperationException("The fleet is already on a mission.");
            }
            else if (fleet.Status != FleetStatus.StandBy)
            {
                throw new InvalidCastException("The selected fleet is not on stand-by.");
            }
            else
            {
                fleet.BeginMission(MissionType.Expedition);
            }
        }

        public void RecallFleet(int aFleetId)
        {
            Fleet fleet = Fleets.SingleOrDefault(x => x.Id == aFleetId);

            if (fleet == null)
            {
                throw new InvalidOperationException("Fleet not found.");
            }

            if (!fleet.CanBeRecalled())
            {
                throw new InvalidOperationException("Fleet is on a mission that cannot be terminated early.");
            }

            fleet.AbortMission();
        }

        public void DisbandFleet(int aFleetId)
        {
            Fleet fleet = Fleets.SingleOrDefault(x => x.Id == aFleetId);

            if (fleet == null)
            {
                throw new InvalidOperationException("Fleet not found.");
            }

            if (!fleet.CanBeDisbanded())
            {
                throw new InvalidOperationException("Fleet cannot be disbanded at this point in time.");
            }

            Fleets.Remove(fleet);
            fleet.Admiral.Fleet = null;
            fleet.Admiral = null;

            Shipyard.ChangeDockedShip(fleet.ShipDesign, fleet.CurrentShipCount);
        }

        public void CreateDefensePlan(List<DefenseDeployment> aDeployments)
        {
            DefensePlan defensePlan = new DefensePlan(Game.Universe);
            
            defensePlan.DefenseDeployments = aDeployments.Select(x => new DefenseDeployment(Game.Universe)
            {
                DefensePlan = defensePlan,
                FleetId = x.FleetId,
                Command = x.Command,
                Type = x.Type,
                X = x.X,
                Y = x.Y
            }).ToList();

            ValidateResult result = defensePlan.Validate();

            if (result.IsPassResult())
            {
                DefensePlans.Add(defensePlan);
            }
            else
            {
                throw new InvalidOperationException(result.Items.Where(x => x.Severity == Severity.Error).First().Message);
            }
        }

        internal ShipDesign CreateShipDesign()
        {
            ShipDesign result = new ShipDesign(Universe);
            result.Player = this;

            return result;
        }

        public List<Admiral> GetAdmiralPool()
        {
            return Admirals.Where(x => x.Fleet == null).Except(Fleets.Select(x => x.Admiral)).ToList();
        }

        public long GetTechCost(Tech aTech)
        {
            long baseCost = aTech.GetBaseCost();

            long discount = 0;
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

            long cost = (int)(baseCost * ((100 - discount) / 100.0));

            cost = (int)(cost / Game.Configuration.TechRateModifier);

            if (cost < 0)
            {
                cost = long.MaxValue;
            }

            return cost;
        }

        public void Update()
        {
            while (Turn < Universe.CurrentTurn)
            {
                UpdateTurn();
                Turn++;
            }
        }

        public void UpdateTurn()
        {
            Effects = Effects.Where(x => x.RemainingDuration <= 0).ToList();

            if (Effects.Where(x => x.Type == PlayerEffectType.SkipTurn).Any())
            {
                return;
            }

            ApplyInstantEffects();

            UpdateFleets();
            UpdateResearch();
            PlanetUpdateTurnResult planetResult = UpdatePlanets();
            Resource totalIncome = CalculateTotalIncome(planetResult.Income);
            Resource totalUpkeep = CalculateTotalUpkeep(planetResult.Upkeep);

            Resource balance = totalIncome;

            Resource.ResearchPoint += totalIncome.ResearchPoint;

            totalUpkeep += CalculateSecurityUpkeep(balance);
            balance.ProductionPoint -= totalUpkeep.ProductionPoint;
            totalUpkeep.ProductionPoint = 0;
            
            balance.ProductionPoint -= Shipyard.CalculateShipProduction(totalIncome.ProductionPoint);

            Shipyard.ShipProduction += Shipyard.CalculateRealShipProduction(totalIncome.ProductionPoint);

            Shipyard.BuildShips(totalIncome.ProductionPoint);

            totalUpkeep += CalculateFleetUpkeep();

            PayFleetUpkeep(balance, totalUpkeep);

            // RepairDamageShips(); throw new NotImplementedException();

            PayCouncilTax(balance);

            Resource.ProductionPoint += balance.ProductionPoint;

            UpdateTech();

            UpdateAdmiralPool();

            UpdateHonor();

            UpdateEffects();
        }

        private void ApplyInstantEffects()
        {
            foreach (PlayerEffectInstance effect in Effects)
            {
                effect.Apply();
            }

            foreach (PlayerEffectInstance instant in Effects.Where(x => x.IsInstant))
            {
                Effects.Remove(instant);
            }
        }

        private void UpdateAdmiralPool()
        {
            if (Admirals.Count >= Game.Configuration.Player.MaxAdmiralCount)
            {
                return;
            }
            else
            {
                AdmiralTimer++;

                int military = ControlModel.Military;

                int createTime = Game.Configuration.Player.CreateAdmiralPeriod - (military * Game.Configuration.Player.AdmiralMilitaryBonus);

                if (createTime < Game.Configuration.Player.MinCreateAdmiralPeriod)
                {
                    createTime = Game.Configuration.Player.MinCreateAdmiralPeriod;
                }

                if (AdmiralTimer < createTime)
                {
                    return;
                }
                else
                {
                    Admiral admiral = CreateAdmiral().AsPlayerAdmiral(this);
                    Admirals.Add(admiral);

                    AdmiralTimer = 0;
                }
            }
        }

        private void UpdateEffects()
        {
            foreach (PlayerEffectInstance effect in Effects)
            {
                effect.UpdateTurn();
            }
        }

        private void UpdateFleets()
        {
            foreach (Fleet fleet in Fleets)
            {
                fleet.UpdateTurn();
            }
        }

        private void UpdateHonor()
        {
            HonorTimer++;

            if (HonorTimer > Game.Configuration.Player.HonorIncreasePeriod)
            {
                Honor++;
                HonorTimer = 0;
            }
        }

        private void UpdateResearch()
        {
            if (ResearchInvestment > 0)
            {
                long invest;

                if (Race.BaseTraits.Contains(RacialTrait.EfficientInvestment))
                {
                    invest = Planets.GetTotalResearchLabCount() * 10 * 2;
                }
                else
                {
                    invest = Planets.GetTotalResearchLabCount() * 10;
                }

                long rp = 0;

                if (ResearchInvestment < invest)
                {
                    invest = ResearchInvestment;
                }

                ResearchInvestment -= invest;

                rp = invest / 20;
                Resource.ResearchPoint += rp;
            }
        }

        private void UpdateSecurity(int aIncome)
        {
            Alertness -= 5;
            if (Alertness < 0)
            {
                Alertness = 0;
            }
        }

        private void UpdateTech()
        {
            Tech target = TargetTech;

            if (target == null)
            {
                IEnumerable<Tech> availableTechs = Game.Configuration.Techs.Where(x => x.Prerequisites.Evaluate(this)).Except(Techs);
                if (availableTechs.Any())
                {
                    target = availableTechs.Random();
                }
            }

            if (target != null)
            {
                long cost = GetTechCost(target);

                if (Resource.ResearchPoint >= cost)
                {
                    Resource.ResearchPoint -= cost;
                    DiscoverTech(target);
                }
            }
        }

        public void DiscoverTech(Tech aTech)
        {
            Techs.Add(aTech);
            AddNews($"You have discovered {aTech.Name}.");
        }
        
        private PlanetUpdateTurnResult UpdatePlanets()
        {
            PlanetUpdateTurnResult result = new PlanetUpdateTurnResult();

            foreach (Planet planet in Planets)
            {
                result += planet.UpdateTurn();
            }

            return result;
        }

        private Resource CalculateFleetUpkeep()
        {
            Resource result = new Resource();

            long upkeep = 0;
            foreach (Fleet fleet in Fleets)
            {
                if (fleet.Status == FleetStatus.Deactivated)
                {
                    if (long.MaxValue - upkeep < fleet.Upkeep/10)
                    {
                        upkeep = long.MaxValue;
                        break;
                    }
                    else
                    {
                        upkeep += (int)fleet.Upkeep / 10;
                    }
                }
                else
                {
                    if (long.MaxValue - upkeep < fleet.Upkeep)
                    {
                        upkeep = long.MaxValue;
                        break;
                    }
                    else
                    {
                        upkeep += (int)fleet.Upkeep;
                    }
                }
            }

            foreach (KeyValuePair<ShipDesign, int> dockedShipItem in Shipyard.ShipPool)
            {
                int shipUpkeep = dockedShipItem.Value * (int)dockedShipItem.Key.ShipClass.Upkeep;

                if (long.MaxValue - upkeep < shipUpkeep)
                {
                    upkeep = long.MaxValue;
                    break;
                }
                else
                {
                    upkeep += shipUpkeep;
                }
            }

            result.MilitaryPoint = upkeep;

            return result;
        }

        private void PayFleetUpkeep(Resource aIncome, Resource aUpkeep)
        {
            long upkeep = aUpkeep.MilitaryPoint;
            long income = aIncome.MilitaryPoint;
            long balance = aIncome.ProductionPoint;

            if (upkeep > income)
            {
                upkeep -= income;
                aIncome.MilitaryPoint = 0;

                upkeep *= 20;

                if (balance + Resource.ProductionPoint < upkeep)
                {
                    List<Fleet> candidateFleetList = Fleets.Where(x => x.Status != FleetStatus.Deactivated).ToList();

                    if (candidateFleetList.Any())
                    {
                        Fleet deactivated = candidateFleetList.Random();

                        deactivated.Status = FleetStatus.Deactivated;

                        AddNews($"You are in financial difficulty to supply the whole upkeep of your fleets.\nFleet {deactivated.Name} has been deactivated.");
                    }
                    else
                    {
                        AddNews($"You are in financial difficulty to supply the whole upkeep of your fleets.\nShips are being scrapped for income.");

                        int scrap = 0;

                        if (Shipyard.ShipPool.Any())
                        {
                            foreach (KeyValuePair<ShipDesign, int> dockedShipType in Shipyard.ShipPool)
                            {
                                int numberScrapped = dockedShipType.Value / 10;

                                if (numberScrapped < 0)
                                {
                                    numberScrapped = 1;
                                }

                                scrap += dockedShipType.Key.ShipClass.Cost * numberScrapped / 10;

                                Shipyard.ChangeShipPool(dockedShipType.Key, -numberScrapped);

                                aIncome.ProductionPoint += scrap;
                            }
                        }
                        else
                        {
                            Fleet disbandFleet = Fleets.Random();
                            Shipyard.ShipPool[disbandFleet.ShipDesign] += disbandFleet.CurrentShipCount;

                            disbandFleet.Player = null;
                            disbandFleet.Admiral = null;
                            Fleets.Remove(disbandFleet);
                            disbandFleet.Admiral.Fleet = null;
                        }
                    }

                    foreach (Fleet fleet in Fleets.Where(x => x.Status ==  FleetStatus.Deactivated && x.Mission.Type != MissionType.None))
                    {
                        fleet.Mission.Delay(1);
                    }
                }
                else
                {
                    ReactivateFleets();
                    if (aIncome.ProductionPoint >= upkeep)
                    {
                        aIncome.ProductionPoint -= upkeep;
                    }
                    else if (aIncome.ProductionPoint + Resource.ProductionPoint >= upkeep)
                    {
                        upkeep -= aIncome.ProductionPoint;
                        aIncome.ProductionPoint = 0;

                        Resource.ProductionPoint -= upkeep;
                    }
                    aIncome.MilitaryPoint = 0;
                }
            }
            else
            {
                ReactivateFleets();
                aIncome.MilitaryPoint = 0;
            }
        }

        private void PayCouncilTax(Resource aBalance)
        {
            if (aBalance.ProductionPoint > 100)
            {
                long tax = aBalance.ProductionPoint * 5 / 100;

                Council.Resource.ProductionPoint += tax;
                aBalance.ProductionPoint -= tax;
            }
        }
        
        private Resource CalculateTotalIncome(Resource aBaseIncome)
        {
            Resource total = new Resource(aBaseIncome);

            total.ResearchPoint = Effects.Where(x => x.Type == PlayerEffectType.ProduceRpPerTurn).CalculateTotalEffect(aBaseIncome.ResearchPoint, x => x.Argument1);

            total.MilitaryPoint = Effects.Where(x => x.Type == PlayerEffectType.ProduceMpPerTurn).CalculateTotalEffect(aBaseIncome.MilitaryPoint, x => x.Argument1);

            return total;
        }

        private Resource CalculateTotalUpkeep(Resource aBaseUpkeep)
        {
            return new Resource(aBaseUpkeep);
        }

        private Resource CalculateSecurityUpkeep(Resource aBalance)
        {
            long upkeep = 0;
            long income = aBalance.ProductionPoint;

            switch (SecurityLevel)
            {
                case SecurityLevel.Loose:
                    upkeep = (int)(income * 2.5 / 100);
                    break;
                case SecurityLevel.Wary:
                    upkeep = (income * 5 / 100);
                    break;
                case SecurityLevel.Alerted:
                    upkeep = (income * 10 / 100);
                    break;
                case SecurityLevel.Impenetrable:
                    upkeep = (income * 20 / 100);
                    break;
                case SecurityLevel.Defenseless:
                default:
                    upkeep = 0;
                    break;
            }

            return new Resource() { ProductionPoint = upkeep };
        }

        private void ReactivateFleets()
        {
            foreach (Fleet fleet in Fleets)
            {
                if (fleet.Status == FleetStatus.Deactivated)
                {
                    if (fleet.Mission.Type == MissionType.None)
                    {
                        fleet.Status = FleetStatus.StandBy;
                    }
                    else
                    {
                        fleet.Status = FleetStatus.UnderMission;
                    }
                }
            }
        }
        
        public int CalculateTotalEffect(int aBase, PlayerEffectType aPlayerEffectType)
        {
            return Effects.Where(x => x.Type == aPlayerEffectType).CalculateTotalEffect(aBase, x => x.Argument1);
        }

        public long CalculateTotalShips()
        {
            long result = 0;

            result += Fleets.Sum(x => x.CurrentShipCount);
            result += Shipyard.ShipPool.Sum(x => x.Value);

            return result;
        }

        public int CalculateRank()
        {
            return Universe.Players.OrderByDescending(x => x.Power).ThenBy(x => x.Id).ToList().IndexOf(this);
        }

        public long CalculateTotalVotes()
        {
            long result = 0;

            result += Council.Players.Where(x => x.VotedCouncilPlayer == this).Sum(x => x.VotingPower);

            return result;
        }

        public void ChangeVote(int aPlayerId)
        {
            Player votedPlayer = Council.Players.SingleOrDefault(x => x.Id == aPlayerId);

            if (votedPlayer != null)
            {
                if (votedPlayer != VotedCouncilPlayer && VotedCouncilPlayer != null)
                {
                    Honor--;
                }

                VotedCouncilPlayer = votedPlayer;
            }
            else
            {
                if (VotedCouncilPlayer != null)
                {
                    Honor--;
                }

                VotedCouncilPlayer = null;
            }
        }

        public RelationType GetMostSignificantRelation(Player aOther)
        {
            return Relations.Where(x => x.ToPlayer == aOther).OrderByDescending(x => x.Significance).Select(x => x.Type).FirstOrDefault();
        }

        public List<PlayerRelation> GetRelations(Player aOther)
        {
            return GetRelations(aOther, x => true);
        }

        public List<PlayerRelation> GetRelations(Player aOther, Func<PlayerRelation, bool> aPredicate)
        {
            return Relations.Where(x => x.FromPlayer == aOther || x.ToPlayer == aOther).Where(aPredicate).ToList();
        }

        public void ClearRelations(Player aOther)
        {
            ClearRelations(aOther, x => true);
        }

        public void ClearRelations(Player aOther, Func<PlayerRelation, bool> aPredicate)
        {
            foreach (PlayerRelation relation in GetRelations(aOther, aPredicate))
            {
                FromRelations.Remove(relation);
                ToRelations.Remove(relation);
            }

            Mailbox.ExpiryOutstandingMessages(aOther);
        }

        private void CreateRelations(Player aOther, RelationType aType, int aExpiryTurn = 0)
        {
            PlayerRelation relation = new PlayerRelation(Universe, this, aOther, aType, aExpiryTurn);

            FromRelations.Add(relation);
            aOther.ToRelations.Add(relation);
        }

        public void FormPact(Player aOther)
        {
            RelationType currentRelation = GetMostSignificantRelation(aOther);

            if (currentRelation != RelationType.None && currentRelation != RelationType.Truce)
            {
                throw new InvalidOperationException("Pacts can only be formed while having no existing relationship or during a truce.");
            }
            if (Council != aOther.Council)
            {
                throw new InvalidOperationException("Pacts can only be formed between players in the same council.");
            }
            
            CreateRelations(aOther, RelationType.Peace);

            AddNews($"You have formed a pact with {aOther.GetDisplayName()}.");
            aOther.AddNews($"{GetDisplayName()} has formed a pact with you.");
        }

        public void BreakPact(Player aOther)
        {
            RelationType currentRelation = GetMostSignificantRelation(aOther);

            if (currentRelation != RelationType.Peace)
            {
                throw new InvalidOperationException($"A pact between {GetDisplayName()} and {aOther.GetDisplayName()} does not exist.");
            }

            ClearRelations(aOther, x => x.Type == RelationType.Peace);

            Honor -= 3;

            AddNews($"You have broken a pact with {aOther.GetDisplayName()}.");
            aOther.AddNews($"{GetDisplayName()} has broken a pact with you.");
        }

        public void FormAlly(Player aOther)
        {
            RelationType currentRelation = GetMostSignificantRelation(aOther);

            if (currentRelation != RelationType.Peace)
            {
                throw new InvalidOperationException("Alliances can only be formed between parties having a pact.");
            }
            if (Council != aOther.Council)
            {
                throw new InvalidOperationException("Alliances can only be formed between players of the same council.");
            }
            
            CreateRelations(aOther, RelationType.Ally);

            AddNews($"You have formed an alliance with {aOther.GetDisplayName()}.");
            aOther.AddNews($"{GetDisplayName()} has formed an alliance with you.");
        }

        public void BreakAlly(Player aOther)
        {
            RelationType currentRelation = GetMostSignificantRelation(aOther);

            if (currentRelation != RelationType.Ally)
            {
                throw new InvalidOperationException($"An alliance between {GetDisplayName()} and {aOther.GetDisplayName()} does not exist.");
            }

            ClearRelations(aOther, x => x.Type == RelationType.Ally);

            Honor -= 5;

            AddNews($"You have broken an alliance with {aOther.GetDisplayName()}.");
            aOther.AddNews($"{GetDisplayName()} has broken an alliance with you.");
        }

        public void DeclareTruce(Player aOther)
        {
            RelationType currentRelation = GetMostSignificantRelation(aOther);

            if (currentRelation != RelationType.Hostile && currentRelation != RelationType.TotalWar && currentRelation != RelationType.War)
            {
                throw new InvalidOperationException($"There is no negative relation between {GetDisplayName()} and {aOther.GetDisplayName()}.");
            }

            ClearRelations(aOther);
            CreateRelations(aOther, RelationType.Truce);

            AddNews($"You and {aOther.GetDisplayName()} have agreed to a truce.");
            aOther.AddNews($"You and {GetDisplayName()} have agreed to a truce.");
        }

        public void DeclareHostile(Player aOther)
        {
            RelationType currentRelation = GetMostSignificantRelation(aOther);

            if (currentRelation != RelationType.None && currentRelation != RelationType.Truce)
            {
                throw new InvalidOperationException("Declaring hostility can only be done when there is relationship in place.");
            }

            ClearRelations(aOther);
            CreateRelations(aOther, RelationType.Hostile);

            AddNews($"You have declared hostility against {aOther.GetDisplayName()}.");
            aOther.AddNews($"{GetDisplayName()} has declared hostility against you.");
        }

        public void DeclareWar(Player aOther)
        {
            RelationType currentRelation = GetMostSignificantRelation(aOther);

            if (currentRelation != RelationType.None && currentRelation != RelationType.Truce && currentRelation != RelationType.Hostile)
            {
                throw new InvalidOperationException("Declaring war can only be done when there is no relationship in place or you have hostile relations with the other player.");
            }
            if (Council != aOther.Council)
            {
                throw new InvalidOperationException("Declaring war can only be done between players in the same council.");
            }

            ClearRelations(aOther, x => x.Type != RelationType.Hostile);
            CreateRelations(aOther, RelationType.War);

            AddNews($"You have declared war on {aOther.GetDisplayName()}.");
            aOther.AddNews($"{GetDisplayName()} has declared war on you.");
        }

        public void DeclareTotalWar(Player aOther)
        {
            RelationType currentRelation = GetMostSignificantRelation(aOther);

            if (currentRelation != RelationType.War)
            {
                throw new InvalidOperationException("Declaring total war requires the parties to already be at war.");
            }
            if (Council != aOther.Council)
            {
                throw new InvalidOperationException("Declaring total war can only be done between players in the same council.");
            }

            CreateRelations(aOther, RelationType.TotalWar);

            AddNews($"You have declared total war on {aOther.GetDisplayName()}.");
            aOther.AddNews($"{GetDisplayName()} has declared total war on you.");
        }

        public void ExecuteDiplomaticAction(PlayerMessageType aType, int aPlayerId)
        {
            Player other = Universe.Players.SingleOrDefault(x => x.Id == aPlayerId);

            if (other == null)
            {
                throw new InvalidOperationException($"Player {aPlayerId} does not exist.");
            }

            switch (aType)
            {
                case PlayerMessageType.SuggestTruce:
                    Mailbox.SendSuggestTruce(other);
                    break;
                case PlayerMessageType.SuggestPact:
                    Mailbox.SendSuggestPact(other);
                    break;
                case PlayerMessageType.SuggestAlly:
                    Mailbox.SendSuggestAlly(other);
                    break;
                case PlayerMessageType.BreakAlly:
                    Mailbox.SendBreakAlly(other);
                    BreakAlly(other);
                    break;
                case PlayerMessageType.BreakPact:
                    Mailbox.SendBreakPact(other);
                    BreakPact(other);
                    break;
                case PlayerMessageType.DeclareHostility:
                    Mailbox.SendDeclareHostility(other);
                    DeclareHostile(other);
                    break;
                case PlayerMessageType.DeclareWar:
                    Mailbox.SendDeclareWar(other);
                    DeclareWar(other);
                    break;
                case PlayerMessageType.DeclareTotalWar:
                    Mailbox.SendDeclarTotaleWar(other);
                    DeclareTotalWar(other);
                    break;
                default:
                    break;
            }
        }

        public bool IsDead()
        {
            return Planets.Any();
        }

        public Battle.Player ToBattlePlayer()
        {
            Battle.Player result = new Battle.Player(Id, Name, Race, Traits);

            return result;
        }
    }
}
