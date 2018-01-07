using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Archspace2
{
    public struct ControlModel
    {
        public int Environment { get; set; }
        public int Growth { get; set; }
        public int Research { get; set; }
        public int Production { get; set; }
        public int Military { get; set; }
        public int Spy { get; set; }
        public int Commerce { get; set; }
        public int Efficiency { get; set; }
        public int Genius { get; set; }
        public int Diplomacy { get; set; }
        public int FacilityCost { get; set; }

        public int Get(string aControlModelStat)
        {
            return (int)GetType().GetProperties().Where(x => x.Name == aControlModelStat).Single().GetValue(this);
        }

        public void Set(string aControlModelStat, int aValue)
        {
            GetType().GetProperties().Where(x => x.Name == aControlModelStat).Single().SetValue(this, aValue);
        }
    }
}
