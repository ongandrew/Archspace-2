using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2.Battle
{
    public class Admiral : NamedEntity
    {
        public int Level { get; set; }
        public AdmiralSpecialAbility SpecialAbility { get; set; }
        public AdmiralRacialAbility RacialAbility { get; set; }
        public int Efficiency { get; set; }
        public AdmiralSkills Skills { get; set; }

        public int GainedExperience { get; set; }

        public Admiral(int aId, string aName, int aLevel, AdmiralSpecialAbility aSpecialAbility, AdmiralRacialAbility aRacialAbility, int aEfficiency, AdmiralSkills aSkills)
        {
            Id = aId;
            Name = aName;
            Level = aLevel;
            SpecialAbility = aSpecialAbility;
            RacialAbility = aRacialAbility;
            Efficiency = aEfficiency;
            Skills = aSkills;

            GainedExperience = 0;
        }
    }
}
