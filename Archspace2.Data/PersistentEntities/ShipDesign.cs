using Archspace2.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Archspace2
{
    [Table("ShipDesign")]
    public class ShipDesign : UniverseEntity, IPowerContributor, IValidatable
    {
        public string Name { get; set; }

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

        public long Power
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

                totalLevel += components.Sum(x => x.TechLevel);

                components.AddRange(Devices);

                totalLevel += Devices.Count * 5;

                return (long)((ShipClass.Space / 100.0) * (2.5 + ((totalLevel / components.Count) / 2.0)));
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

        public ValidateResult Validate()
        {
            ValidateResult result = new ValidateResult();

            if (Name == null || Name == string.Empty)
            {
                result.Items.Add(new ValidateResult.Item() { Message = "Name is null or empty.", Severity = Severity.Error });
            }

            if (ShipClass == null)
            {
                result.Items.Add(new ValidateResult.Item() { Message = "ShipClass is null.", Severity = Severity.Error });
            }

            if (Armor == null)
            {
                result.Items.Add(new ValidateResult.Item() { Message = "Armor is null.", Severity = Severity.Error });
            }

            if (Computer == null)
            {
                result.Items.Add(new ValidateResult.Item() { Message = "Computer is null.", Severity = Severity.Error });
            }

            if (Engine == null)
            {
                result.Items.Add(new ValidateResult.Item() { Message = "Engine is null.", Severity = Severity.Error });
            }

            if (Shield == null)
            {
                result.Items.Add(new ValidateResult.Item() { Message = "Shield is null.", Severity = Severity.Error });
            }

            if (Weapons == null)
            {
                result.Items.Add(new ValidateResult.Item() { Message = "Weapons are null.", Severity = Severity.Error });
            }
            else if (ShipClass != null && ShipClass.WeaponSlotCount != Weapons.Count)
            {
                result.Items.Add(new ValidateResult.Item() { Message = "Weapon count does not match ship class weapon slot count.", Severity = Severity.Error });
            }

            if (Devices == null)
            {
                result.Items.Add(new ValidateResult.Item() { Message = "Devices are null.", Severity = Severity.Error });
            }
            else if (ShipClass != null && ShipClass.DeviceSlotCount < Devices.Count)
            {
                result.Items.Add(new ValidateResult.Item() { Message = "More devices are specified than are allowed on this ship class.", Severity = Severity.Error });
            }
            else if (Devices.Distinct().Count() != Devices.Count)
            {
                result.Items.Add(new ValidateResult.Item() { Message = "Duplicate device found.", Severity = Severity.Error });
            }

            return result;
        }
    }
}