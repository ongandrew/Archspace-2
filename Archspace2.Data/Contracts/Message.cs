using System;

namespace Archspace2
{
    public enum MessageStatus
    {
        Unread,
        Read,
        Answered,
        Expired
    }

    public abstract class Message: UniverseEntity
    {
        public int? FromId { get; set; }
        public int? ToId { get; set; }

        public string Subject { get; set; }
        public string Content { get; set; }

        public MessageStatus Status { get; set; }

        public DateTime DateTime { get; private set; }
        public int Turn { get; private set; }

        public int? ReplyToMessageId { get; set; }

        public abstract bool IsAwaitingResponse();

        internal Message() : base()
        {
        }
        public Message(Universe aUniverse): base(aUniverse)
        {
            DateTime = DateTime.UtcNow;
            Turn = aUniverse.CurrentTurn;
        }
    }
}
