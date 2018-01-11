using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Archspace2
{
    public class Fleet : UniverseEntity
    {
        public int? PlayerId { get; set; }
        public int AdmiralId { get; set; }

        [ForeignKey("AdmiralId")]
        public Admiral Admiral { get; set; }
        [ForeignKey("PlayerId")]
        public Player Player { get; set; }
    }
}
