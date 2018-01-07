using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public class Weapon : ShipComponent
    {
        public Weapon() : base(ComponentCategory.Weapon)
        {
        }

        public int Space { get; set; }
    }
}
