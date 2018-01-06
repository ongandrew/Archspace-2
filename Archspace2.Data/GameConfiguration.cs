using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public class GameConfiguration
    {
        public GameConfiguration()
        {
            Armors = new List<Armor>();
            Computers = new List<Computer>();
            Engines = new List<Engine>();
            Races = new List<Race>();
            Shields = new List<Shield>();
            ShipClasses = new List<ShipClass>();
            Weapons = new List<Weapon>();
        }

        public List<Armor> Armors { get; set; }
        public List<Computer> Computers { get; set; }
        public List<Device> Devices { get; set; }
        public List<Engine> Engines { get; set; }
        public List<Race> Races { get; set; }
        public List<Shield> Shields { get; set; }
        public List<ShipClass> ShipClasses { get; set; }
        public List<Weapon> Weapons { get; set; }
    }
}
