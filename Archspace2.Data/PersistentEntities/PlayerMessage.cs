using System.ComponentModel.DataAnnotations.Schema;

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
        DeclareHostility,
        DeclareTotalWar
    }

    [Table("PlayerMessage")]
    public class PlayerMessage : Message
    {
        [ForeignKey("FromId")]
        public Player FromPlayer { get; private set; }
        
        [ForeignKey("ToId")]
        public Player ToPlayer { get; private set; }

        [ForeignKey("ReplyToMessageId")]
        public PlayerMessage ReplyToMessage { get; set; }

        public PlayerMessageType Type { get; set; }
        
        public PlayerMessage() : base()
        {
        }
        public PlayerMessage(Universe aUniverse) : base(aUniverse)
        {
        }
        public PlayerMessage(Player aFromPlayer, Player aToPlayer, PlayerMessageType aType, string aSubject = null, string aContent = null) : this(aFromPlayer.Universe)
        {
            FromPlayer = aFromPlayer;
            ToPlayer = aToPlayer;
            Type = aType;
            Subject = aSubject;
            Content = aContent;

            Status = MessageStatus.Unread;
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
