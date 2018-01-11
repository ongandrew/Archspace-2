using Archspace2.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Archspace2
{
    public class ShipDesign : UniverseEntity
    {
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

        public string DeviceIdList { get; private set; }
        [NotMapped]
        public List<Device> Devices
        {
            get
            {
                return WeaponIdList.DeserializeIds().Select(x => Game.Configuration.Devices.Single(device => device.Id == x)).ToList();
            }
            set
            {
                DeviceIdList = value.Select(x => x.Id).SerializeIds();
            }
        }

        public string WeaponIdList { get; private set; }
        [NotMapped]
        public List<Weapon> Weapons
        {
            get
            {
                return WeaponIdList.DeserializeIds().Select(x => Game.Configuration.Weapons.Single(weapon => weapon.Id == x)).ToList();
            }
            set
            {
                WeaponIdList = value.Select(x => x.Id).SerializeIds();
            }
        }
    }
}