using System.Collections.Generic;
using System.Linq;

namespace Archspace2
{
    public abstract class Mailbox<T>: UniverseEntity where T : Message
    {
        public int OwnerId { get; set; }

        public ICollection<T> ReceivedMessages { get; set; }
        public ICollection<T> SentMessages { get; set; }

        internal Mailbox()
        {
        }
        public Mailbox(Universe aUniverse): base(aUniverse)
        {
        }

        public T FindMessageById(int aId)
        {
            return ReceivedMessages.Union(SentMessages).SingleOrDefault(x => x.Id == aId);
        }
    }
}
