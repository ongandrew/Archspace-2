using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Archspace2
{
    public class Shipyard : UniverseEntity
    {
        public int PlayerId { get; set; }
        public Player Player { get; set; }

        public int ShipProduction { get; set; }
        public int ShipProductionInvestment { get; set; }

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
                return JsonConvert.SerializeObject(mShipPool);
            }
            set
            {
                mShipPool = JsonConvert.DeserializeObject<Dictionary<ShipDesign, int>>(ShipPoolDictionary);
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
                ShipPoolDictionary = JsonConvert.SerializeObject(value);
            }
        }

        public Shipyard(Player aPlayer) : base(aPlayer.Universe)
        {
            Player = aPlayer;
            mShipBuildOrders = new List<ShipBuildOrder>();
            mShipPool = new Dictionary<ShipDesign, int>(new UniverseEntityComparer());
        }

        public void BuildShips(int aIncome)
        {
            if (Player != null)
            {
                ShipProductionInvestment += ShipProduction;
                ShipProduction = 0;
            }
            else
            {
                while (ShipBuildOrders.Any())
                {
                    ShipBuildOrder shipBuilderOrder = ShipBuildOrders.First();

                    int count = 0;

                    int costPerShip;
                    if (shipBuilderOrder.ShipDesign.Armor.Type == ArmorType.Bio && Player.Traits.Contains(RacialTrait.GreatSpawningPool))
                    {
                        costPerShip = shipBuilderOrder.ShipDesign.ShipClass.Cost * 80 / 100;
                    }
                    else
                    {
                        costPerShip = shipBuilderOrder.ShipDesign.ShipClass.Cost;
                    }
                    
                    while (ShipProduction > costPerShip && shipBuilderOrder.NumberToBuild > 0)
                    {
                        count++;
                        ShipProduction -= costPerShip;
                        shipBuilderOrder.NumberToBuild--;
                    }

                    if (count == 0)
                    {
                        break;
                    }
                    else
                    {
                        ShipPool[shipBuilderOrder.ShipDesign] += count;

                        Player.AddNews($"{count} {shipBuilderOrder.ShipDesign.Name} ship(s) are built and put in the ship pool.");

                        if (shipBuilderOrder.NumberToBuild == 0)
                        {
                            mShipBuildOrders.Remove(shipBuilderOrder);
                        }
                    }
                }

                ShipProductionInvestment -= (aIncome * 30 / 100);
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

        public int CalculateShipProduction(int aIncome)
        {
            int result = aIncome * (30 + (Player.ControlModel.Military * 5)) / 100;

            if (result < 0)
            {
                return 0;
            }
            else
            {
                return result;
            }
        }

        public int CalculateRealShipProduction(int aIncome)
        {
            int maxPPUsage = CalculateShipProduction(aIncome);

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
                    return (int)(CalculateShipProduction(aIncome) * (100 + (50 * bonusRatio * 2) / 100));
                }
                else
                {
                    return (int)(CalculateShipProduction(aIncome) * (100 + (50 * bonusRatio) / 100));
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
