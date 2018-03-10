using Archspace2.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archspace2
{
    public enum PlanetSize
    {
        Tiny = 0,
        Small,
        Medium,
        Large,
        Huge
    };

    public enum PlanetResource
    {
        UltraPoor = 0,
        Poor,
        Normal,
        Rich,
        UltraRich
    };

    [Table("Planet")]
    public class Planet : UniverseEntity, IPowerContributor
    {
        public string Name { get; set; }
        public int ClusterId { get; set; }
        [ForeignKey("ClusterId")]
        public Cluster Cluster { get; set; }

        public int? PlayerId { get; set; }
        [ForeignKey("PlayerId")]
        public Player Player { get; set; }

        public int Order { get; set; }
        public int Population { get; set; }

        public long Power
        {
            get
            {
                return 200 + (1 + Infrastructure.Factory + Infrastructure.ResearchLab + Infrastructure.MilitaryBase);
            }
        }

        public PlanetSize Size { get; set; }
        public PlanetResource Resource { get; set; }

        public int Temperature { get; set; }
        public double Gravity { get; set; }
        public Atmosphere Atmosphere { get; set; }

        public bool Terraforming { get; set; }
        public int TerraformingTimer { get; set; }

        public int PrivateerAmount { get; set; }
        public int PrivateerTimer { get; set; }
        public int BlockadeTimer { get; set; }

        public Infrastructure Infrastructure { get; set; }
        public DistributionRatio DistributionRatio { get; set; }

        public bool UsePlanetInvestmentPool { get; set; }
        public long Investment { get; set; }

        public int WasteRate {
            get
            {
                if (Player == null)
                {
                    return 0;
                }
                else
                {
                    int efficiency = ControlModel.Efficiency;

                    if (efficiency < -5)
                    {
                        efficiency = -5;
                    }
                    else if (efficiency > 10)
                    {
                        efficiency = 10;
                    }

                    if (Order < Game.Configuration.Planet.WasteSettings[efficiency].WasteFreePlanetCount)
                    {
                        return 0;
                    }
                    else
                    {
                        int waste = 0;
                        waste = (int)((1 + Order - Game.Configuration.Planet.WasteSettings[efficiency].WasteFreePlanetCount) * Game.Configuration.Planet.WasteSettings[efficiency].WastePerPlanet);

                        if (waste > 90)
                        {
                            return 90;
                        }
                        else
                        {
                            return waste;
                        }
                    }
                }
            }
        }
        public int MaxPopulation {
            get
            {
                int result = (60 + (int)Size * 20) * 1000;

                int maxRatio = 0;
                int growth = ControlModel.Growth;
                int environment = ControlModel.Environment;

                if (growth > 10)
                {
                    maxRatio = 9;
                }
                else if (growth > 2 && growth <= 10)
                {
                    maxRatio = growth - 2;
                }
                else if (growth < -2 && growth >= -5)
                {
                    maxRatio = growth + 2;
                }
                else if (growth < -5)
                {
                    maxRatio = -3;
                }
                else
                {
                    maxRatio = 0;
                }

                result = result * (10 + maxRatio) / 10;

                if (environment < -1)
                {
                    result = result * (21 + environment) / 20;
                }

                if (result < 10000)
                {
                    result = 10000;
                }

                return result;
            }
        }

        public ControlModel ControlModel
        {
            get
            {
                ControlModel result = new ControlModel();

                result += CalculateEnvironmentModifier();
                result += Attributes.CalculateControlModelModifier();

                if (Player != null)
                {
                    result += Player.ControlModel;
                }

                return result;
            }
        }

        public string PlanetAttributeList { get; set; }
        [NotMapped]
        public List<PlanetAttribute> Attributes
        {
            get
            {
                return PlanetAttributeList.DeserializeIds().Select(x => Game.Configuration.PlanetAttributes.Single(y => y.Id == x)).ToList();
            }
            set
            {
                PlanetAttributeList = value.Select(x => x.Id).SerializeIds();
            }
        }

        public Planet(Universe aUniverse) : base(aUniverse)
        {
            Population = 0;

            Infrastructure = new Infrastructure();
            DistributionRatio = new DistributionRatio();
            
            Attributes = new List<PlanetAttribute>();
            CommercePlanets = new List<Planet>();

            Atmosphere = new Atmosphere();
        }

        public Planet AsHomePlanet(Player aPlayer)
        {
            AsRacialPlanet(aPlayer.Race);

            Order = 0;
            Population = 50000;

            Infrastructure.Factory = 30;
            Infrastructure.ResearchLab = 10;
            Infrastructure.MilitaryBase = 10;

            Size = PlanetSize.Medium;
            Resource = PlanetResource.Normal;

            Player = aPlayer;

            return this;
        }

        public Planet AsRacialPlanet(Race aRace)
        {
            Atmosphere = new Atmosphere(aRace.HomeAtmosphere);
            Gravity = aRace.HomeGravity;
            Temperature = aRace.HomeTemperature;

            return this;
        }

        public Planet AsRandomPlanet()
        {
            Atmosphere.Randomize();

            return this;
        }

        public async Task ClearCommerceAsync()
        {
            using (DatabaseContext databaseContext = Game.GetContext())
            {
                CommercePlanets.Clear();

                await databaseContext.SaveChangesAsync();
            }
        }

        public async Task ClearCommerceAsync(Planet aPlanet)
        {
            using (DatabaseContext databaseContext = Game.GetContext())
            {
                CommercePlanets.Remove(aPlanet);

                await databaseContext.SaveChangesAsync();
            }
        }

        public string CommercePlanetList { get; set; }
        [NotMapped]
        public List<Planet> CommercePlanets
        {
            get
            {
                using (DatabaseContext databaseContext = Game.GetContext())
                {
                    return CommercePlanetList.DeserializeIds().Select(x => databaseContext.Planets.Single(y => y.Id == x)).ToList();
                }
            }
            set
            {
                CommercePlanetList = value.Select(x => x.Id).SerializeIds();
            }
        }

        public bool CanPrivateer()
        {
            return PrivateerTimer <= 0;
        }

        public bool IsBlockaded()
        {
            return BlockadeTimer > 0;
        }

        public void StartPrivateer()
        {
            TimeSpan timeSpan = TimeSpan.FromHours(6);
            PrivateerTimer = (int)timeSpan.TotalSeconds / Game.Configuration.SecondsPerTurn;
        }

        public void ProcessPrivateer()
        {
            if (PrivateerTimer <= 0)
            {
                return;
            }
            else
            {
                PrivateerTimer--;
                if (BlockadeTimer == 0)
                {
                    Player.AddNews($"{Name} is now free of pirates.");
                }
            }
        }

        public void ProcessBlockade()
        {
            if (BlockadeTimer <= 0)
            {
                return;
            }
            else
            {
                BlockadeTimer--;
                if (BlockadeTimer == 0)
                {
                    Player.AddNews($"The blockade has expired on {Name}.");
                }
            }
        }

        public async Task AddAttributeAsync(PlanetAttribute aPlanetAttribute)
        {
            using (DatabaseContext databaseContext = Game.GetContext())
            {
                Attributes.Add(aPlanetAttribute);

                await databaseContext.SaveChangesAsync();
            }
        }

        public async Task RemoveAttributeAsync(PlanetAttribute aPlanetAttribute)
        {
            using (DatabaseContext databaseContext = Game.GetContext())
            {
                Attributes.Remove(aPlanetAttribute);

                await databaseContext.SaveChangesAsync();
            }
        }

        public void StartTerraforming()
        {
            Terraforming = true;
            TerraformingTimer = 0;
        }

        public void StopTerraforming()
        {
            Terraforming = false;
            TerraformingTimer = 0;
        }

        public PlanetUpdateTurnResult UpdateTurn()
        {
            PlanetUpdateTurnResult result = new PlanetUpdateTurnResult();

            if (IsParalyzed())
            {
            }
            else
            {
                int nogadaPoint, 
                    usedNogadaPoint, 
                    remainingNogadaPoint;

                ProcessPrivateer();
                ProcessBlockade();

                Resource newResources;
                Resource upkeep;

                if (Player != null)
                {
                    nogadaPoint = CalculateNogadaPoint();
                    usedNogadaPoint = ComputeUpkeepAndOutput(nogadaPoint, out newResources, out upkeep);

                    if (!IsBlockaded())
                    {
                        newResources.ProductionPoint += CalculateCommerce();
                    }

                    if (PrivateerAmount > 0)
                    {
                        newResources.ProductionPoint -= PrivateerAmount;
                        PrivateerAmount = 0;
                    }

                    remainingNogadaPoint = nogadaPoint - usedNogadaPoint;

                    BuildNewInfrastructure(remainingNogadaPoint);

                    ProcessPopulationGrowth();

                    ProcessTerraforming();

                    ProcessInvestment();

                    result.Income = newResources;
                    result.Upkeep = upkeep;
                }
            }

            return result;
        }

        public bool IsParalyzed()
        {
            if (Player != null)
            {
                if (Player.Effects.Where(x => x.Type == PlayerEffectType.ParalyzePlanet && x.Target == Id).Any())
                {
                    return true;
                }
            }

            return false;
        }

        public void BuildNewInfrastructure(int aNogadaPoint)
        {
            int militaryBaseNogadaPoint;
            int researchLabNogadaPoint;
            int factoryNogadaPoint;

            int buildingCost;

            aNogadaPoint += aNogadaPoint * WasteRate / 100;

            militaryBaseNogadaPoint = aNogadaPoint * DistributionRatio.MilitaryBase / 100;
            researchLabNogadaPoint = aNogadaPoint * DistributionRatio.ResearchLab / 100;
            factoryNogadaPoint = aNogadaPoint - researchLabNogadaPoint - militaryBaseNogadaPoint;

            Infrastructure buildingProgress = new Infrastructure()
            {
                Factory = factoryNogadaPoint,
                ResearchLab = researchLabNogadaPoint,
                MilitaryBase = militaryBaseNogadaPoint
            };

            buildingCost = Infrastructure.Total() + 5;
            buildingCost = buildingCost * (10 - ControlModel.FacilityCost) / 10;

            if (buildingCost < (Infrastructure.Total() + 5)/ 2)
            {
                buildingCost = (Infrastructure.Total() + 5) / 2;
            }

            if (buildingProgress.Factory > buildingCost)
            {
                buildingProgress.Factory -= buildingCost;
                Infrastructure.Factory++;
            }

            if (buildingProgress.MilitaryBase > buildingCost)
            {
                buildingProgress.MilitaryBase -= buildingCost;
                Infrastructure.MilitaryBase++;
            }

            if (buildingProgress.ResearchLab > buildingCost)
            {
                buildingProgress.ResearchLab -= buildingCost;
                Infrastructure.ResearchLab++;
            }
        }

        public long CalculateProductionPointPerTurn()
        {
            int nogadaPoint = CalculateNogadaPoint();
            Resource temp;
            Resource temp2;

            ComputeUpkeepAndOutput(nogadaPoint, out temp, out temp2);

            return temp.ProductionPoint;
        }

        public long CalculateResearchPointPerTurn()
        {
            int nogadaPoint = CalculateNogadaPoint();
            Resource temp;
            Resource temp2;

            ComputeUpkeepAndOutput(nogadaPoint, out temp, out temp2);

            return temp.ResearchPoint;
        }

        public long CalculateMilitaryPointPerTurn()
        {
            int nogadaPoint = CalculateNogadaPoint();
            Resource temp;
            Resource temp2;

            ComputeUpkeepAndOutput(nogadaPoint, out temp, out temp2);

            return temp.MilitaryPoint;
        }

        public long CalculateUsableInvestment()
        {
            if (Player == null)
            {
                return 0;
            }
            else
            {
                long investMaxPP = 0;

                if (Player.Traits.Contains(RacialTrait.EfficientInvestment))
                {
                    investMaxPP = CalculateMaxInvestmentProudction() / 2;
                }
                else
                {
                    investMaxPP = CalculateMaxInvestmentProudction();
                }

                if (investMaxPP != 0 && (Investment != 0 || UsePlanetInvestmentPool))
                {
                    long invest = 0;

                    if (investMaxPP < Investment)
                    {
                        invest = investMaxPP;
                    }
                    else
                    {
                        invest = Investment;

                        if (UsePlanetInvestmentPool)
                        {
                            long investPool = Player.PlanetInvestmentPool;
                            if (invest + investPool >= investMaxPP)
                            {
                                invest = investMaxPP;
                            }
                            else
                            {
                                invest += investPool;
                            }
                        }
                    }

                    return invest;
                }
                else
                {
                    return 0;
                }
            }
        }

        public long CalculateMaxInvestmentProudction()
        {
            return (long)((((double)Population) * ((double)MaxPopulation)) / 1000000.0);
        }

        public long CalculateInvestRate()
        {
            long investMaxPP = 0;

            if (Player.Traits.Contains(RacialTrait.EfficientInvestment))
            {
                investMaxPP = CalculateMaxInvestmentProudction() / 2;
            }
            else
            {
                investMaxPP = CalculateMaxInvestmentProudction();
            }

            return investMaxPP == 0 ? 0 : (long)(CalculateUsableInvestment() * (100 - WasteRate) / investMaxPP);
        }

        private long CalculateCommerce()
        {
            long productionPoint = 0;

            foreach (Planet commercePlanet in CommercePlanets)
            {
                long point = commercePlanet.CalculateProductionPointPerTurn();
                long commerce = ControlModel.Commerce;

                Player commercePlanetOwner = commercePlanet.Player;

                Council targetCommerceCouncil = commercePlanetOwner.Council;

                if (Player.Council.FromRelations.Where(x => x.Type == RelationType.Subordinary && x.ToCouncil.Id == targetCommerceCouncil.Id).Any())
                {
                    commerce++;
                }

                if (targetCommerceCouncil.FromRelations.Where(x => x.Type == RelationType.Subordinary && x.ToCouncil.Id == Player.Council.Id).Any())
                {
                    commerce++;
                }

                if (Player.FromRelations.Where(x => x.Type == RelationType.Ally && x.ToPlayer.Id == commercePlanetOwner.Id).Any())
                {
                    commerce++;
                }

                if (Player.Council.FromRelations.Where(x => x.Type == RelationType.Ally && x.ToCouncil.Id == targetCommerceCouncil.Id).Any())
                {
                    commerce++;
                }

                point /= 10;
                point = point * (100 + (commerce * 10)) / 100;

                if (point > 0)
                {
                    productionPoint += point;
                }
            }

            int maxPoint = Population * (6 + ControlModel.Commerce);

            if (productionPoint > maxPoint)
            {
                productionPoint = maxPoint;
            }
            else if (productionPoint < 0)
            {
                productionPoint = 0;
            }

            return productionPoint;
        }
        private int CalculateNogadaPoint()
        {
            int labourPoint = (Population / 1000) * 5;

            if (ControlModel.Environment <= -10)
            {
                labourPoint /= 10;
            }
            else if (ControlModel.Environment < -1)
            {
                labourPoint = (labourPoint / 10) * (11 + ControlModel.Environment);
            }

            labourPoint -= labourPoint * WasteRate / 100;

            int bonusRatio = ((int)(CalculateInvestRate() / 20)) * 10;
            labourPoint = labourPoint + (int)(labourPoint * bonusRatio / 100);

            return labourPoint;
        }

        private ControlModel CalculateEnvironmentModifier()
        {
            ControlModel result = new ControlModel();

            if (Player != null)
            {
                Atmosphere homeAtmosphere = Player.Race.HomeAtmosphere;

                if (!Player.Race.BaseTraits.Contains(RacialTrait.NoBreath))
                {
                    int difference = 0;

                    difference += Math.Abs(homeAtmosphere.H2 - Atmosphere.H2);
                    difference += Math.Abs(homeAtmosphere.Cl2 - Atmosphere.Cl2);
                    difference += Math.Abs(homeAtmosphere.CO2 - Atmosphere.CO2);
                    difference += Math.Abs(homeAtmosphere.O2 - Atmosphere.O2);
                    difference += Math.Abs(homeAtmosphere.N2 - Atmosphere.N2);
                    difference += Math.Abs(homeAtmosphere.CH4 - Atmosphere.CH4);
                    difference += Math.Abs(homeAtmosphere.H2O - Atmosphere.H2O);

                    difference /= 2;

                    result.Environment -= difference;
                }
            }

            result.Environment -= Math.Abs(Temperature - Player.Race.HomeTemperature);

            if (!Attributes.Where(x => x.Type == PlanetAttributeType.GravityControlled).Any())
            {
                result.Environment -= (int)(Math.Abs(Player.Race.HomeGravity - Gravity)/0.2);
            }

            return result;
        }
        private void ProcessPopulationGrowth()
        {
            int growthRatio;

            if (Population == 0)
            {
                growthRatio = 0;
            }
            else
            {
                growthRatio = ((MaxPopulation - Population) * 5 / Population);
            }

            if (growthRatio < -5)
            {
                growthRatio = -5;
            }
            else if (growthRatio > 5)
            {
                growthRatio = 5;
            }

            int baseGrowth;

            if (Population > MaxPopulation)
            {
                baseGrowth = 0;
            }
            else
            {
                baseGrowth = 50 + (ControlModel.Growth * 10);
            }

            if (baseGrowth < 10)
            {
                baseGrowth = 10;
            }
            else if (baseGrowth > 150)
            {
                baseGrowth = 150;
            }

            Population += (Population * growthRatio / 100) + baseGrowth;
        }

        private void ProcessTerraforming()
        {
            if (Terraforming)
            {
                TerraformingTimer++;
                long neededTurns = (24 * 3600)/(Game.Configuration.SecondsPerTurn) 
                    - (((24 * 3600) / (Game.Configuration.SecondsPerTurn)) * (CalculateInvestRate()/25)/10);

                if (TerraformingTimer >= neededTurns)
                {
                    TerraformingTimer = 0;

                    if (TryTerraformAtmosphere())
                    {
                        return;
                    }
                    else if (TryTerraformTemperature())
                    {
                        return;
                    }
                    else if (TryTerraformGravity())
                    {
                        return;
                    }
                    else if (TryTerraformNegativeAttributes())
                    {
                        return;
                    }
                    else
                    {
                        StopTerraforming();
                        Player.AddNews($"The terraforming of {Name} is completed.");
                    }
                }
            }
        }

        private void ProcessInvestment()
        {
            if (Player == null)
            {
                return;
            }
            else
            {
                long investment = CalculateUsableInvestment();

                if (investment <= Investment)
                {
                    Investment -= investment;
                }
                else
                {
                    investment -= Investment;
                    Investment = 0;

                    if (investment <= Player.PlanetInvestmentPool)
                    {
                        Player.PlanetInvestmentPool -= investment;
                    }
                    else
                    {
                        throw new InvalidOperationException("Overspending detected in investment.");
                    }
                }
            }
            
        }

        private bool TryTerraformAtmosphere()
        {
            if (Player.Traits.Contains(RacialTrait.NoBreath))
            {
                return false;
            }
            
            if (Atmosphere == Player.Race.HomeAtmosphere)
            {
                return false;
            }
            else
            {
                Dictionary<GasType, int> currentDictionary = Atmosphere.AsDictionary();
                Dictionary<GasType, int> homeDictionary = Player.Race.HomeAtmosphere.AsDictionary();
                
                GasType? decreasingGas =
                (from current in currentDictionary.AsEnumerable()
                 join home in homeDictionary.AsEnumerable()
                 on current.Key equals home.Key
                 where current.Value > home.Value
                 select current.Key).First();

                GasType? increasingGas =
                (from current in currentDictionary.AsEnumerable()
                    join home in homeDictionary.AsEnumerable()
                    on current.Key equals home.Key
                    where current.Value < home.Value
                    select current.Key).FirstOrDefault();

                if (decreasingGas != null && increasingGas != null)
                {
                    Atmosphere.Change(decreasingGas.Value, increasingGas.Value);

                    Player.AddNews($"Terraforming: {decreasingGas.Value} has been decreased and {increasingGas.Value} has been increased on {Name}");
                }
                else if (increasingGas != null)
                {
                    Atmosphere[increasingGas.Value]++;

                    Player.AddNews($"Terraforming: {increasingGas.Value} has been generated on {Name}");
                }
                else
                {
                    Atmosphere[increasingGas.Value]++;

                    Player.AddNews($"Terraforming: {decreasingGas.Value} has been removed on {Name}");
                }

                return true;
            }
        }
        private bool TryTerraformTemperature()
        {
            int homeTemperature = Player.Race.HomeTemperature;

            if (Math.Abs(Temperature - homeTemperature) < 50)
            {
                return false;
            }
            else
            {
                if (Temperature > homeTemperature)
                {
                    Temperature -= 25 + Game.Random.Next(1, 50);

                    Player.AddNews($"Terraforming: The temperature has gone down to {Temperature} K on {Name}.");
                }
                else
                {
                    Temperature += 25 + Game.Random.Next(1, 50);

                    Player.AddNews($"Terraforming: The temperature has gone up to {Temperature} K on {Name}.");

                }

                return true;
            }
        }
        private bool TryTerraformGravity()
        {
            double homeGravity = Player.Race.HomeGravity;

            if (Math.Abs(Gravity - homeGravity) < 0.2)
            {
                return false;
            }
            else if (Attributes.Contains(PlanetAttributeType.GravityControlled))
            {
                return false;
            }
            else if (!Player.Techs.Any(x => x.Name == "Gravity Control"))
            {
                return true;
            }
            else
            {
                Attributes.Add(PlanetAttributeType.GravityControlled.ToPlanetAttribute());

                Player.AddNews($"Terraforming : You set up a gravity control unit on {Name}.");

                return true;
            }
        }
        private bool TryTerraformNegativeAttributes()
        {
            if (!Attributes.Any(x => x.Type == PlanetAttributeType.Radiation || x.Type == PlanetAttributeType.HostileMonster || x.Type == PlanetAttributeType.ObstinateMicrobe))
            {
                return false;
            }
            else if (Attributes.Contains(PlanetAttributeType.Radiation) && Player.Techs.Any(x => x.Name == "Solar Manipulation"))
            {
                Attributes.RemoveAll(x => x.Type == PlanetAttributeType.Radiation);

                Player.AddNews($"Terraforming : You clear the radiation of {Name} using solar manipulation.");

                return true;
            }
            else if (Attributes.Contains(PlanetAttributeType.HostileMonster) && Player.Techs.Any(x => x.Name == "Primitive Language"))
            {
                Attributes.RemoveAll(x => x.Type == PlanetAttributeType.HostileMonster);

                Player.AddNews($"Terraforming : You control the hostile monster of {Name} using primitive language.");

                return true;
            }
            else if (Attributes.Contains(PlanetAttributeType.ObstinateMicrobe) && Player.Techs.Any(x => x.Name == "Genetic Therapy"))
            {
                Attributes.RemoveAll(x => x.Type == PlanetAttributeType.ObstinateMicrobe);

                Player.AddNews($"Terraforming : You destroy the obstinate microbe on {Name} using genetic therapy.");

                return true;
            }
            else
            {
                return true;
            }
        }

        private int ComputeUpkeepAndOutput(int aNogadaPoint, out Resource aNewResources, out Resource aUpkeep)
        {
            int upkeepRatio,
                activeFactory,
                factoryNogadaPoint,
                productionPointPerFactory,
                productionPoint,
                activeResearchLab,
                researchLabNogadaPoint,
                researchPointPerResearchLab,
                researchPoint,
                activeMilitaryBase,
                militaryBaseNogadaPoint,
                militaryPointPerMilitaryBase,
                militaryPoint;

            upkeepRatio = -ControlModel.FacilityCost;
            if (upkeepRatio < -5)
            {
                upkeepRatio = -5;
            }

            activeFactory = factoryNogadaPoint = Infrastructure.Factory;
            factoryNogadaPoint = factoryNogadaPoint * (10 + upkeepRatio) / 10;

            if (factoryNogadaPoint > aNogadaPoint)
            {
                activeFactory = activeFactory * aNogadaPoint / factoryNogadaPoint;
                factoryNogadaPoint = aNogadaPoint;
            }

            aNogadaPoint -= factoryNogadaPoint;

            productionPointPerFactory = 60 + (ControlModel.Production * 10);
            productionPointPerFactory *= Game.Configuration.Planet.ResourceMultipliers[Resource];

            productionPointPerFactory /= 100;

            if (productionPointPerFactory < 20)
            {
                productionPointPerFactory = 20;
            }

            productionPoint = activeFactory * productionPointPerFactory;

            productionPoint -= productionPoint * WasteRate / 100;

            

            activeResearchLab = researchLabNogadaPoint = Infrastructure.ResearchLab;

            researchLabNogadaPoint = researchLabNogadaPoint * (10 + upkeepRatio) / 10;

            if (researchLabNogadaPoint > aNogadaPoint)
            {
                activeResearchLab = activeResearchLab * aNogadaPoint / researchLabNogadaPoint;
                researchLabNogadaPoint = aNogadaPoint;
            }

            aNogadaPoint -= researchLabNogadaPoint;

            researchPointPerResearchLab = 10 + ControlModel.Research;

            if (researchPointPerResearchLab < 1)
            {
                researchPointPerResearchLab = 1;
            }

            researchPoint = activeResearchLab * researchPointPerResearchLab;
            researchPoint -= researchPoint * WasteRate / 100;

            activeMilitaryBase = militaryBaseNogadaPoint = ControlModel.Military;

            militaryBaseNogadaPoint = militaryBaseNogadaPoint * (10 + upkeepRatio) / 10;

            if (militaryBaseNogadaPoint > aNogadaPoint)
            {
                activeMilitaryBase = activeMilitaryBase * aNogadaPoint / militaryBaseNogadaPoint;
                militaryBaseNogadaPoint = aNogadaPoint;
            }

            aNogadaPoint -= militaryBaseNogadaPoint;

            militaryPointPerMilitaryBase = 10 + (ControlModel.Military * 2);

            militaryPoint = activeMilitaryBase * militaryPointPerMilitaryBase;

            militaryPoint -= militaryPoint * WasteRate / 200;

            if (!IsBlockaded())
            {
                aNewResources = new Resource()
                {
                    ProductionPoint = productionPoint,
                    ResearchPoint = researchPoint,
                    MilitaryPoint = militaryPoint
                };
            }
            else
            {
                aNewResources = new Resource();
            }

            aUpkeep = new Resource()
            {
                ProductionPoint = activeFactory * (10 + upkeepRatio),
                ResearchPoint = activeResearchLab * (10 + upkeepRatio),
                MilitaryPoint = activeMilitaryBase * (10 + upkeepRatio)
            };

            return factoryNogadaPoint + researchLabNogadaPoint + militaryBaseNogadaPoint;
        }
    }
}
