namespace Archspace2
{
    public interface IPlayerEffect : IControlModelModifier, IModifier
    {
        PlayerEffectType Type { get; set; }
        int Target { get; set; }
        int Argument1 { get; set; }
        int Argument2 { get; set; }

        bool IsInstant { get; set; }
    }
}
