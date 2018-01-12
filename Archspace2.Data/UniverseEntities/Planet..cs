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

    public enum GasType
    {
        H2,
        Cl2,
        CO2,
        O2,
        N2,
        CH4,
        H2O
    };

    public class Planet : UniverseEntity
    {
        public Planet()
        {
            PlanetAttributes = new List<PlanetAttribute>();
            CommercePlanets = new List<Planet>();
        }

        public int ClusterId { get; set; }
        [ForeignKey("ClusterId")]
        public Cluster Cluster { get; set; }

        public int? PlayerId { get; set; }
        [ForeignKey("PlayerId")]
        public Player Player { get; set; }

        public int Order { get; set; }
        public int Population { get; set; }

        public PlanetSize Size { get; set; }
        public PlanetResource Resource { get; set; }

        public int Temperature { get; set; }
        public double Gravity { get; set; }
        public Atmosphere Atmosphere { get; set; }

        public int Investment { get; set; }
        public int InvestRate { get
            {
                throw new NotImplementedException();
            }
        }
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
        public int MaxInvestProduction
        {
            get
            {
                return (int)((((double)Population) * ((double)MaxPopulation)) / 1000000.0);
            }
        }

        public int FactoryCount { get; set; }
        public int ResearchLabCount { get; set; }
        public int MilitaryBaseCount { get; set; }

        public ControlModel ControlModel
        {
            get
            {
                ControlModel result = new ControlModel();

                result += CalculateEnvironmentModifier();
                result += PlanetAttributes.CalculateControlModelModifier();

                if (Player != null)
                {
                    result += Player.ControlModel;
                }

                return result;
            }
        }

        public string PlanetAttributeList { get; set; }
        [NotMapped]
        public List<PlanetAttribute> PlanetAttributes
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

        public async Task ClearCommerceAsync()
        {
            using (DatabaseContext databaseContext = Game.Context)
            {
                CommercePlanets.Clear();

                await databaseContext.SaveChangesAsync();
            }
        }

        public async Task ClearCommerceAsync(Planet aPlanet)
        {
            using (DatabaseContext databaseContext = Game.Context)
            {
                CommercePlanets.Remove(aPlanet);

                await databaseContext.SaveChangesAsync();
            }
        }

        public ICollection<Planet> CommercePlanets { get; set; }

        public async Task AddAttributeAsync(PlanetAttribute aPlanetAttribute)
        {
            using (DatabaseContext databaseContext = Game.Context)
            {
                PlanetAttributes.Add(aPlanetAttribute);

                await databaseContext.SaveChangesAsync();
            }
        }

        public async Task RemoveAttributeAsync(PlanetAttribute aPlanetAttribute)
        {
            using (DatabaseContext databaseContext = Game.Context)
            {
                PlanetAttributes.Remove(aPlanetAttribute);

                await databaseContext.SaveChangesAsync();
            }
        }

        public async Task UpdateTurn()
        {
            int labourPoint, usedLabourPoint, RemainingLabourPoint;

            if (Player != null)
            {
                labourPoint = CalculateLabourPoint();

            }
        }
        
        private int CalculateLabourPoint()
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

            int bonusRatio = ((int)(InvestRate / 20)) * 10;
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

            if (!PlanetAttributes.Where(x => x.Type == PlanetAttributeType.GravityControlled).Any())
            {
                result.Environment -= (int)(Math.Abs(Player.Race.HomeGravity - Gravity)/0.2);
            }

            return result;
        }
        private void UpdatePopulation()
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
    }
}
