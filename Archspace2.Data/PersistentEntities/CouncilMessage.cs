using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

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
        public Council FromCouncil { get; set; }
        
        [ForeignKey("ToId")]
        public Council ToCouncil { get; set; }

        public PlayerMessageType Type { get; set; }
        
        public CouncilMessage(Universe aUniverse) : base(aUniverse)
        {
        }

        public CouncilMessage(Council aFromCouncil, Council aToCouncil, PlayerMessageType aType, string aSubject = null, string aContent = null) : this(aFromCouncil.Universe)
        {
            FromCouncil = aFromCouncil;
            ToCouncil = aToCouncil;
            Subject = aSubject;
            Content = aContent;
        }
    }
}
