using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Archspace2
{
    [Table("PlayerMailbox")]
    public class PlayerMailbox : Mailbox<PlayerMessage>
    {
        [ForeignKey("OwnerId")]
        public Player Player { get; set; }

        internal PlayerMailbox()
        {
        }
        public PlayerMailbox(Player aPlayer) : base(aPlayer.Universe)
        {
            Player = aPlayer;
            ReceivedMessages = new List<PlayerMessage>();
            SentMessages = new List<PlayerMessage>();
        }

        public void ExpiryOutstandingMessages(Player aOther)
        {
            foreach (PlayerMessage message in ReceivedMessages.Where(x => x.IsAwaitingResponse() && x.FromPlayer == aOther))
            {
                message.Status = MessageStatus.Expired;
            }

            foreach (PlayerMessage message in SentMessages.Where(x => x.IsAwaitingResponse() && x.ToPlayer == aOther))
            {
                message.Status = MessageStatus.Expired;
            }

            foreach (PlayerMessage message in aOther.Mailbox.ReceivedMessages.Where(x => x.IsAwaitingResponse() && x.FromPlayer == Player))
            {
                message.Status = MessageStatus.Expired;
            }

            foreach (PlayerMessage message in aOther.Mailbox.SentMessages.Where(x => x.IsAwaitingResponse() && x.ToPlayer == Player))
            {
                message.Status = MessageStatus.Expired;
            }
        }
    }
}
