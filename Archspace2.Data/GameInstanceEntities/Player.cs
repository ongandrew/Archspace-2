using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Archspace2
{
    public class Player : GameInstanceEntity
    {
        public int CouncilId { get; set; }
        public Council Council { get; set; }
        
        public int RaceId { get; set; }
        [NotMapped]
        public Race Race
        {
            get
            {
                return Game.Configuration.Races.Single(x => x.Id == RaceId);
            }
            set
            {
                RaceId = value.Id;
            }
        }

        ICollection<Admiral> Commanders { get; set; }
    }
}
