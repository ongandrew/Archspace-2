namespace Archspace2
{
    public class BattleSettings
    {
        public int MaxX { get; set; }
        public int MaxY { get; set; }
        public int MaxAlliedFleets { get; set; }

        public static BattleSettings CreateDefault()
        {
            return new BattleSettings()
            {
                MaxX = 10000,
                MaxY = 10000,
                MaxAlliedFleets = 10
            };
        }
    }
}
