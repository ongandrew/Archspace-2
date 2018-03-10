using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public abstract class Mailbox<T>: UniverseEntity
    {
        public int OwnerId { get; set; }

        public virtual ICollection<T> ReceivedMessages { get; set; }
        public virtual ICollection<T> SentMessages { get; set; }

        internal Mailbox()
        {
        }
        public Mailbox(Universe aUniverse): base(aUniverse)
        {
        }
    }
}
