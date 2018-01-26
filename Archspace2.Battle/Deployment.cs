using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2.Battle
{
    public class Deployment : IUnit
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Direction { get; set; }

        public Fleet Fleet;

        public void Deploy()
        {
            Fleet.X = X;
            Fleet.Y = Y;
            Fleet.Direction = Direction;
        }
    }
}
