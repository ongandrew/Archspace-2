namespace Archspace2.Battle
{
    public class RecordBattlefield : NamedEntity
    {
        public RecordBattlefield(int aId, string aName)
        {
            Id = aId;
            Name = aName;
        }

        public RecordBattlefield(Battlefield aBattlefield) : this(aBattlefield.Id, aBattlefield.Name)
        {
        }
    }
}
