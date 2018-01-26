using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Archspace2
{
    [Table("Mailbox")]
    public class Mailbox : UniverseEntity
    {
        public int PlayerId { get; set; }
        [ForeignKey("PlayerId")]
        public Player Player { get; set; }

        public ICollection<MailItem> ReceivedMail { get; set; }

        public Mailbox(Player aPlayer) : base(aPlayer.Universe)
        {
            Player = aPlayer;
            ReceivedMail = new List<MailItem>();
        }
    }
}
