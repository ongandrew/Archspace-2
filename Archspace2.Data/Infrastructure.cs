namespace Archspace2
{
    public class Infrastructure
    {
        public long Factory { get; set; }
        public long ResearchLab { get; set; }
        public long MilitaryBase { get; set; }

        public long Total()
        {
            return Factory + ResearchLab + MilitaryBase;
        }
    }
}
