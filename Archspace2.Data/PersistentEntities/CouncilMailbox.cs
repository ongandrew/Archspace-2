using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Archspace2
{
    [Table("CouncilMailbox")]
    public class CouncilMailbox : Mailbox<CouncilMessage>
    {
        [ForeignKey("OwnerId")]
        public Council Council { get; set; }

        public CouncilMailbox()
        {
        }
        public CouncilMailbox(Council aCouncil) : base(aCouncil.Universe)
        {
            Council = aCouncil;
            ReceivedMessages = new List<CouncilMessage>();
            SentMessages = new List<CouncilMessage>();
        }
    }
}
