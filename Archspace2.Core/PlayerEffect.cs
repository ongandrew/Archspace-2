using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Archspace2
{
    public enum PlayerEffectSource
    {
        Spy,
        Event,
        Project,
        Admin
    };
    
    public class PlayerEffect : IPlayerEffect
    {
        public PlayerEffectType Type { get; set; }
        public ModifierType ModifierType { get; set; }

        public int Target { get; set; }
        public int Argument1 { get; set; }
        public int Argument2 { get; set; }
        public ControlModel ControlModelModifier { get; set; }

        public bool IsInstant { get; set; }
    }
}
