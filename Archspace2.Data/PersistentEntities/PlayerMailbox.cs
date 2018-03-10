using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
        }
    }
}
