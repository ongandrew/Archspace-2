using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Archspace2
{
    public class Mailbox : UniverseEntity
    {
        public Mailbox(Universe aUniverse) : base(aUniverse)
        {
            ReceivedMail = new List<MailItem>();
        }

        public int PlayerId { get; set; }
        [ForeignKey("PlayerId")]
        public Player Player { get; set; }

        public ICollection<MailItem> ReceivedMail { get; set; }
    }
}
