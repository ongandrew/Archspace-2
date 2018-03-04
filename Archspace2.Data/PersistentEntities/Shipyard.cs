using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Archspace2
{
    public class Shipyard : UniverseEntity, IPowerContributor
    {
        public int PlayerId { get; set; }
        public Player Player { get; set; }

        public long ShipProduction { get; set; }
        public long ShipProductionInvestment { get; set; }

        private ICollection<ShipBuildOrder> mShipBuildOrders { get; set; }
        public List<ShipBuildOrder> ShipBuildOrders
        {
            get
            {
                return mShipBuildOrders.OrderBy(x => x.OrderTime).ToList();
            }
        }

        public string ShipPoolDictionary
        {
            get
            {
                Dictionary<int, int> result = new Dictionary<int, int>();

                foreach (KeyValuePair<ShipDesign, int> entry in mShipPool)
                {
                    result.Add(entry.Key.Id, entry.Value);
                }

                return JsonConvert.SerializeObject(result);
            }
            set
            {
                Dictionary<ShipDesign, int> result = new Dictionary<ShipDesign, int>();

                foreach (KeyValuePair<int, int> entry in JsonConvert.DeserializeObject<Dictionary<int, int>>(value))
                {
                    ShipDesign design = Player.ShipDesigns.Single(x => x.Id == entry.Key);

                    result.Add(design, entry.Value);
                }

                mShipPool = result;
            }
        }
        [NotMapped]
        private Dictionary<ShipDesign, int> mShipPool { get; set; }
        [NotMapped]
        public Dictionary<ShipDesign, int> ShipPool
        { 
            get
            {
                return mShipPool;
            }
            set
            {
                Dictionary<int, int> result = new Dictionary<int, int>();

                foreach (KeyValuePair<ShipDesign, int> entry in value)
                {
                    result.Add(entry.Key.Id, entry.Value);
                }

                ShipPoolDictionary = JsonConvert.SerializeObject(result);
            }
        }

        public long Power
        {
            get
            {
                long result = 0;

                foreach (KeyValuePair<ShipDesign, int> entry in ShipPool)
                {
                    result += entry.Key.Power * entry.Value;
                }

                return result;
            }
        }

        public Shipyard(Player aPlayer) : base(aPlayer.Universe)
        {
            Player = aPlayer;
            mShipBuildOrders = new List<ShipBuildOrder>();
            mShipPool = new Dictionary<ShipDesign, int>();
        }

        public int GetDockedShipCount(ShipDesign aDesign)
        {
            if (ShipPool.ContainsKey(aDesign))
            {
                return ShipPool[aDesign];
            }
            else
            {
                return 0;
            }
        }

        public void ChangeDockedShip(ShipDesign aDesign, int aAmount)
        {
            if (!mShipPool.ContainsKey(aDesign))
            {
                mShipPool[aDesign] = aAmount;
            }
            else
            {
                ShipPool[aDesign] = ShipPool[aDesign] + aAmount;

                if (ShipPool[aDesign] == 0)
                {
                    ShipPool.Remove(aDesign);
                }
            }
        }

        public void ScrapDockedShip(ShipDesign aDesign, int aAmount)
        {
            if (aAmount <= 0)
            {
                return;
            }
            else
            {
                int numberToScrap = aAmount;
                if (numberToScrap > GetDockedShipCount(aDesign))
                {
                    numberToScrap = GetDockedShipCount(aDesign);
                }

                ChangeDockedShip(aDesign, -aAmount);
                int amountEarned = aAmount * aDesign.ShipClass.Cost / 10;
                Player.Resource.ProductionPoint += amountEarned;

                Player.AddNews($"You scrapped {numberToScrap} units of {aDesign.Name} and earned {amountEarned}PP.");
            }
        }

        public void ChangeShipProductionInvestment(long aAmount)
        {
            ShipProductionInvestment += aAmount;
            if (ShipProductionInvestment < 0)
            {
                ShipProductionInvestment = 0;
            }
        }

        public void BuildShips(long aIncome)
        {
            if (!ShipBuildOrders.Any())
            {
                ShipProductionInvestment += ShipProduction;
                ShipProduction = 0;
            }
            else
            {
                while (ShipBuildOrders.Any())
                {
                    ShipBuildOrder shipBuildOrder = ShipBuildOrders.First();

                    int built = 0;

                    int costPerShip;
                    if (shipBuildOrder.ShipDesign.Armor.Type == ArmorType.Bio && Player.Traits.Contains(RacialTrait.GreatSpawningPool))
                    {
                        costPerShip = shipBuildOrder.ShipDesign.ShipClass.Cost * 80 / 100;
                    }
                    else
                    {
                        costPerShip = shipBuildOrder.ShipDesign.ShipClass.Cost;
                    }

                    built = (int)(ShipProduction / costPerShip);
                    if (built > shipBuildOrder.NumberToBuild)
                    {
                        built = shipBuildOrder.NumberToBuild;
                    }

                    ShipProduction -= (built * costPerShip);
                    shipBuildOrder.NumberToBuild -= built;

                    if (built == 0)
                    {
                        break;
                    }
                    else
                    {
                        ChangeDockedShip(shipBuildOrder.ShipDesign, built);

                        Player.AddNews($"{built} {shipBuildOrder.ShipDesign.Name} ship(s) are built and put in the ship pool.");

                        if (shipBuildOrder.NumberToBuild <= 0)
                        {
                            mShipBuildOrders.Remove(shipBuildOrder);
                        }
                    }
                }

                if (!ShipBuildOrders.Any())
                {
                    ShipProductionInvestment += ShipProduction;
                    ShipProduction = 0;
                }

                ChangeShipProductionInvestment(-(aIncome * 30 / 100));
            }
        }

        public void PlaceBuildOrder(int aNumberToBuild, ShipDesign aShipDesign)
        {
            if (aNumberToBuild > 0)
            {
                mShipBuildOrders.Add(new ShipBuildOrder(this, aNumberToBuild, aShipDesign));
            }
        }

        public void DeleteBuildOrder(ShipBuildOrder aShipBuildOrder)
        {
            mShipBuildOrders.Remove(aShipBuildOrder);
        }

        public long CalculateShipProduction(long aIncome)
        {
            long result = aIncome * (30 + (Player.ControlModel.Military * 5)) / 100;

            if (result < 0)
            {
                return 0;
            }
            else
            {
                return result;
            }
        }

        public long CalculateRealShipProduction(long aIncome)
        {
            long maxPPUsage = CalculateShipProduction(aIncome);

            if (maxPPUsage == 0)
            {
                return 0;
            }
            else
            {
                double bonusRatio = ShipProductionInvestment / maxPPUsage;

                if (bonusRatio > 1)
                {
                    bonusRatio = 1;
                }

                if (Player.Traits.Contains(RacialTrait.EfficientInvestment))
                {
                    return (long)(CalculateShipProduction(aIncome) * ((100 + (50 * bonusRatio * 2)) / 100));
                }
                else
                {
                    return (long)(CalculateShipProduction(aIncome) * ((100 + (50 * bonusRatio)) / 100));
                }
            }
        }

        public void ChangeShipPool(ShipDesign aShipDesign, int aAmount)
        {
            ShipPool[aShipDesign] += aAmount;

            if (ShipPool[aShipDesign] == 0)
            {
                ShipPool.Remove(aShipDesign);
            }
            else if (ShipPool[aShipDesign] < 0)
            {
                throw new InvalidOperationException();
            }
        }
    }
}
