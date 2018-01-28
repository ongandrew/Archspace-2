using System;
using System.Collections.Generic;
using System.Text;
using Universal.Common.Extensions;

namespace Archspace2.Battle
{
    public class Turret : Weapon
    {
        public int Number { get; set; }
        public int RemainingCooldown { get; set; }

        public Turret(Weapon aWeapon, int aNumber)
        {
            this.Bind(aWeapon);
            Number = aNumber;
            RemainingCooldown = 0;
        }

        public bool IsReady()
        {
            return RemainingCooldown <= 0;
        }

        public void Cool()
        {
            if (RemainingCooldown > 0)
            {
                RemainingCooldown--;
            }
        }
    }
}
