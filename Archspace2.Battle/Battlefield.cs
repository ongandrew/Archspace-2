namespace Archspace2.Battle
{
    public class Battlefield : NamedEntity
    {
        public Battle Battle { get; set; }

        public Battlefield(int aId, string aName)
        {
            Id = aId;
            Name = aName;
        }
    }
}
