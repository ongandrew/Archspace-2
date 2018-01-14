namespace Archspace2
{
    public interface IPlayerEffect
    {
        PlayerEffectType Type { get; set; }
        ModifierType ModifierType { get; set; }
        int Target { get; set; }
        int Argument1 { get; set; }
        int Argument2 { get; set; }
    }
}
