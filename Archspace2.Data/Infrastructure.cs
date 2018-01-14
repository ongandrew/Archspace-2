using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public class Infrastructure : IBuildings
    {
        public int Factory { get; set; }
        public int ResearchLab { get; set; }
        public int MilitaryBase { get; set; }

        public int Total()
        {
            return Factory + ResearchLab + MilitaryBase;
        }
    }
}
