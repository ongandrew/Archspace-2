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

        public static ControlModel operator+(ControlModel lhs, ControlModel rhs)
        {
            return new ControlModel()
            {
                Environment = lhs.Environment + rhs.Environment,
                Growth = lhs.Growth + rhs.Growth,
                Research = lhs.Research + rhs.Research,
                Production = lhs.Production + rhs.Production,
                Military = lhs.Military + rhs.Military,
                Spy = lhs.Spy + rhs.Spy,
                Commerce = lhs.Commerce + rhs.Commerce,
                Efficiency = lhs.Efficiency + rhs.Efficiency,
                Genius = lhs.Genius + rhs.Genius,
                Diplomacy = lhs.Diplomacy + rhs.Diplomacy,
                FacilityCost = lhs.FacilityCost + rhs.FacilityCost
            };
        }
    }
}
