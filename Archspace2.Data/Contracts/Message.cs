﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public enum MessageStatus
    {
        Unread,
        Read,
        Answered,
        Well
    }

    public abstract class Message: UniverseEntity
    {
        public int? FromId { get; set; }
        public int ToId { get; set; }

        public string Subject { get; set; }
        public string Content { get; set; }

        public DateTime DateTime { get; private set; }
        public int Turn { get; private set; }

        public int? ReplyToMessageId { get; set; }
        public Message ReplyToMessage { get; set; }

        internal Message()
        {
        }
        public Message(Universe aUniverse): base(aUniverse)
        {
            DateTime = DateTime.UtcNow;
            Turn = aUniverse.CurrentTurn;
        }
    }
}
