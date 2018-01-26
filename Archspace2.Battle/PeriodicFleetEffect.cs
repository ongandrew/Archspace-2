using System;
using System.Collections.Generic;
using System.Text;
using Universal.Common.Extensions;

namespace Archspace2.Battle
{
    public class PeriodicFleetEffect : FleetEffect
    {
        protected int mCharge;
        public void Charge()
        {
            mCharge++;
        }
        public void Drain()
        {
            mCharge = 0;
        }

        public bool IsReady()
        {
            return mCharge >= Period;
        }

        public PeriodicFleetEffect()
        {
        }

        public PeriodicFleetEffect(FleetEffect aFleetEffect)
        {
            this.Bind(aFleetEffect);
            mCharge = 0;
        }
    }
}
