using System.ComponentModel.DataAnnotations.Schema;

namespace Archspace2
{
    public enum CouncilMessageType
    {
        Normal,
        SuggestTruce,
        DemandTruce,
        DemandSubmission,
        DeclareTotalWar,
        DeclareWar,
        SuggestPact,
        BreakPact,
        SuggestAlly,
        BreakAlly,
        CouncilFusionOffer,
        SetFreeSubordinary,
        DeclareIndependence,
        ReplySuggestTruce,
        ReplyDemandTruce,
        ReplySuggestPact,
        ReplySuggestAlly,
        ReplyDemandSubmission,
        ReplyCouncilFusionOffer,
        Reply
    }

    [Table("CouncilMessage")]
    public class CouncilMessage : Message
    {
        [ForeignKey("FromId")]
        public Council FromCouncil { get; private set; }
        
        [ForeignKey("ToId")]
        public Council ToCouncil { get; private set; }

        [ForeignKey("ReplyToMessageId")]
        public CouncilMessage ReplyToMessage { get; set; }

        public CouncilMessageType Type { get; set; }
        
        internal CouncilMessage() : base()
        {
        }
        public CouncilMessage(Universe aUniverse) : base(aUniverse)
        {
        }
        public CouncilMessage(Council aFromCouncil, Council aToCouncil, CouncilMessageType aType, string aSubject = null, string aContent = null) : this(aFromCouncil.Universe)
        {
            FromCouncil = aFromCouncil;
            ToCouncil = aToCouncil;
            Type = aType;
            Subject = aSubject;
            Content = aContent;

            Status = MessageStatus.Unread;
        }

        public override bool IsAwaitingResponse()
        {
            if (Type == CouncilMessageType.SuggestAlly || Type == CouncilMessageType.SuggestPact || Type == CouncilMessageType.SuggestTruce ||
                Type == CouncilMessageType.CouncilFusionOffer || Type == CouncilMessageType.DemandSubmission || Type == CouncilMessageType.DemandTruce)
            {
                if (Status != MessageStatus.Answered && Status != MessageStatus.Expired)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
