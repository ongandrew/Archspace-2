using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public enum EventType
    {
        System,
        Council,
        Cluster,
        Magistrate,
        Empire,
        Galactic,
        Racial,
        Major
    };

    public class Event : Entity
    {
        public string Description { get; set; }
        public string Image { get; set; }

        public EventType Type { get; set; }
        public ListType RaceListType { get; set; }
        public List<int> RaceList { get; set; }

        public int MinDuration { get; set; }
        public int MaxDuration { get; set; }
        public int Duration
        {
            get
            {
                if (MinDuration == MaxDuration)
                {
                    return MinDuration;
                }
                else
                {
                    return MinDuration - 1 + Game.Random.Next(1, MaxDuration - MinDuration);
                }
            }
            set
            {
                MinDuration = value;
                MaxDuration = value;
            }
        }
        public int MinHonor { get; set; }
        public int MaxHonor { get; set; }

        public bool RequiresResponse { get; set; }

        public List<EventEffect> Effects { get; set; }

        public Event()
        {
            Effects = new List<EventEffect>();
            RaceList = new List<int>();
        }
    }
}
