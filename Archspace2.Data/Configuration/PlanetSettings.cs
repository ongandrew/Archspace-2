namespace Archspace2
{
    public class PlanetSettings
    {
        public int UltraPoorMultiplier { get; set; }
        public int PoorMultiplier { get; set; }
        public int NormalMultiplier { get; set; }
        public int RichMultiplier { get; set; }
        public int UltraRichMultiplier { get; set; }

        public static PlanetSettings CreateDefault()
        {
            return new PlanetSettings()
            {
                UltraPoorMultiplier = 50,
                PoorMultiplier = 75,
                NormalMultiplier = 100,
                RichMultiplier = 175,
                UltraRichMultiplier = 250
            };
        }
    }
}
