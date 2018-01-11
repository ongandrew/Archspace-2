namespace Archspace2
{
    public class BlackMarketSettings
    {
        public int ItemRegenerationDelay { get; set; }
        public int BidExpiryTime { get; set; }

        public static BlackMarketSettings CreateDefault()
        {
            return new BlackMarketSettings()
            {
                ItemRegenerationDelay = 3600,
                BidExpiryTime = 15
            };
        }
    }
}
