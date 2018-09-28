namespace Archspace2
{
    public class PlayerSettings
    {
        public PlayerSettings()
        {
        }

        public int AdmiralMilitaryBonus { get; set; }
        public int CreateAdmiralPeriod { get; set; }
        public int MaxAdmiralCount { get; set; }
        public int MinCreateAdmiralPeriod { get; set; }
        public int StartingAdmiralCount { get; set; }

        public int HonorIncreasePeriod { get; set; }

        public static PlayerSettings CreateDefault()
        {
            PlayerSettings result = new PlayerSettings();

            result.AdmiralMilitaryBonus = 4;
            result.CreateAdmiralPeriod = 24;
            result.StartingAdmiralCount = 5;
            result.MaxAdmiralCount = 250;
            result.MinCreateAdmiralPeriod = 1;

            result.HonorIncreasePeriod = 288;

            return result;
        }
    }
}
