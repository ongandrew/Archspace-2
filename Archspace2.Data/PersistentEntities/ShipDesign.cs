using Archspace2.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Universal.Common.Extensions;

namespace Archspace2
{
    [Table("ShipDesign")]
    public class ShipDesign : UniverseEntity
    {
        public int? PlayerId { get; set; }
        [ForeignKey("PlayerId")]
        public Player Player { get; set; }

        public int ShipClassId { get; set; }
        [NotMapped]
        public ShipClass ShipClass
        {
            get
            {
                return Game.Configuration.ShipClasses.Single(x => x.Id == ShipClassId);
            }
            set
            {
                ShipClassId = value.Id;
            }
        }

        public int ArmorID { get; set; }
        [NotMapped]
        public Armor Armor
        {
            get
            {
                return Game.Configuration.Armors.Single(x => x.Id == ArmorID);
            }
            set
            {
                ArmorID = value.Id;
            }
        }

        public int ComputerId { get; set; }
        [NotMapped]
        public Computer Computer
        {
            get
            {
                return Game.Configuration.Computers.Single(x => x.Id == ComputerId);
            }
            set
            {
                ComputerId = value.Id;
            }
        }

        public int EngineId { get; set; }
        [NotMapped]
        public Engine Engine
        {
            get
            {
                return Game.Configuration.Engines.Single(x => x.Id == EngineId);
            }
            set
            {
                EngineId = value.Id;
            }
        }

        public int ShieldId { get; set; }
        [NotMapped]
        public Shield Shield
        {
            get
            {
                return Game.Configuration.Shields.Single(x => x.Id == ShieldId);
            }
            set
            {
                ShieldId = value.Id;
            }
        }

        public string DeviceIdList
        {
            get
            {
                return mDevices.Select(x => x.Id).SerializeIds();
            }
            set
            {
                mDevices = value.DeserializeIds().Select(x => Game.Configuration.Devices.Single(device => device.Id == x)).ToList();
            }
        }
        [NotMapped]
        private List<Device> mDevices;
        [NotMapped]
        public List<Device> Devices
        {
            get
            {
                return mDevices;
            }
            set
            {
                DeviceIdList = value.Select(x => x.Id).SerializeIds();
                mDevices = value;
            }
        }

        public string WeaponIdList
        {
            get
            {
                return mWeapons.Select(x => x.Id).SerializeIds();
            }
            set
            {
                mWeapons = value.DeserializeIds().Select(x => Game.Configuration.Weapons.Single(weapon => weapon.Id == x)).ToList();
            }
        }
        [NotMapped]
        private List<Weapon> mWeapons;
        [NotMapped]
        public List<Weapon> Weapons
        {
            get
            {
                return mWeapons;
            }
            set
            {
                WeaponIdList = value.Select(x => x.Id).SerializeIds();
                mWeapons = value;
            }
        }

        public int Power
        {
            get
            {
                int totalLevel = 0;

                List<ShipComponent> components = new List<ShipComponent>()
            {
                Armor,
                Engine,
                Computer,
                Shield
            };
                components.AddRange(Weapons);
                components.AddRange(Devices);

                foreach (ShipComponent component in components)
                {
                    totalLevel += component.TechLevel;
                }

                return ShipClass.Class * totalLevel;
            }
        }

        public ShipDesign(Universe aUniverse) : base(aUniverse)
        {
            mDevices = new List<Device>();
            mWeapons = new List<Weapon>();
        }

        public ShipDesign AsInitialShipDesign(int aIndex)
        {
            if (aIndex == 0)
            {
                Name = "Patrol Boat Mk.I";
                ShipClassId = 4001;
                ArmorID = 5101;
                EngineId = 5401;
                ComputerId = 5201;
                ShieldId = 5301;
                WeaponIdList = "6101";
            }
            else if (aIndex == 1)
            {
                Name = "Star Corvette Mk.I";
                ShipClassId = 4002;
                ArmorID = 5101;
                EngineId = 5401;
                ComputerId = 5201;
                ShieldId = 5301;
                WeaponIdList = "6301,6201";
            }

            return this;
        }
    }
}