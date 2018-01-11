namespace Archspace2
{
    public class ActionSettings
    {
        public int PlayerCouncilDonation { get; set; }
        public int PlayerBreakPact { get; set; }
        public int PlayerBreakAlly { get; set; }
        public int CouncilDeclareWar { get; set; }
        public int CouncilDeclareTotalWar { get; set; }
        public int CouncilBreakPact { get; set; }
        public int CouncilBreakAlly { get; set; }
        public int CouncilImproveRelation { get; set; }
        public int CouncilMergeOffer { get; set; }

        public static ActionSettings CreateDefault()
        {
            return new ActionSettings()
            {
                PlayerCouncilDonation = 96,
                PlayerBreakAlly = 288,
                PlayerBreakPact = 288,
                CouncilBreakAlly = 288,
                CouncilBreakPact = 288,
                CouncilDeclareTotalWar = 288,
                CouncilDeclareWar = 96,
                CouncilImproveRelation = 288,
                CouncilMergeOffer = 288
            };
        }
    }
}
