using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public enum EventEffectAttribute
    {
        AnswerYes,
        AnswerNo,
        Chance,
        DifferentDuration,
        BeginOnly
    };

    public class EventEffect : IPlayerEffect
    {
        protected int mDuration;
        public int Duration {
            get => mDuration;
            set
            {
                mDuration = value;
                Attributes.Add(EventEffectAttribute.DifferentDuration);
            }
        }
        public int Chance { get; set; }
        public PlayerEffectType Type { get; set; }
        public ModifierType ModifierType { get; set; }
        public int Target { get; set; }
        public int Argument1 { get; set; }
        public int Argument2 { get; set; }

        public List<EventEffectAttribute> Attributes { get; set; }

        public EventEffect()
        {
            Attributes = new List<EventEffectAttribute>();
        }
    }
}
