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

        public DateTime DateTime { get; }
        public int Turn { get; }

        public string Text { get; set; }

        public bool Seen { get; set; }

        public NewsItem(Universe aUniverse) : base(aUniverse)
        {
            DateTime = DateTime.UtcNow;
            Turn = aUniverse.CurrentTurn;
            Seen = false;
        }
    }
}
