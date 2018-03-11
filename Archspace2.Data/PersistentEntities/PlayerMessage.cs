using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace Archspace2
{
    public enum PlayerMessageType
    {
        Normal,
        SuggestTruce,
        DemandTruce,
        DeclareWar,
        SuggestPact,
        BreakPact,
        SuggestAlly,
        BreakAlly,
        ReplySuggestTruce,
        ReplyDemandTruce,
        ReplySuggestPact,
        ReplySuggestAlly,
        Reply,
        DeclareHostility
    }

    [Table("PlayerMessage")]
    public class PlayerMessage : Message
    {
        [ForeignKey("FromId")]
        public Player FromPlayer { get; set; }
        
        [ForeignKey("ToId")]
        public Player ToPlayer { get; set; }

        public PlayerMessageType Type { get; set; }
        
        public PlayerMessage(Universe aUniverse) : base(aUniverse)
        {
        }

        public PlayerMessage(Player aFromPlayer, Player aToPlayer, PlayerMessageType aType, string aSubject = null, string aContent = null) : this(aFromPlayer.Universe)
        {
            FromPlayer = aFromPlayer;
            ToPlayer = aToPlayer;
            Subject = aSubject;
            Content = aContent;
        }

        public override bool IsAwaitingResponse()
        {
            if (Type == PlayerMessageType.SuggestAlly || Type == PlayerMessageType.SuggestPact || Type == PlayerMessageType.SuggestTruce)
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
