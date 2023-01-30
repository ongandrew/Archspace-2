using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Archspace2
{
    [Table("NewsItem")]
    public class NewsItem : UniverseEntity
    {
        public int PlayerId { get; set; }
        [ForeignKey("PlayerId")]
        public Player Player { get; set; }

        public DateTime DateTime { get; private set; }
        public int Turn { get; private set; }

        public string Text { get; set; }

        public bool Seen { get; set; }

		public NewsItem() : this(null) { }
		public NewsItem(Universe aUniverse) : base(aUniverse)
        {
            DateTime = DateTime.UtcNow;
            Turn = aUniverse.CurrentTurn;
            Seen = false;
        }
    }
}
