using Archspace2.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Archspace2
{
    public class Player : UniverseEntity
    {
        public Player()
        {
            Techs = new List<Tech>();
            Admirals = new List<Admiral>();
            Planets = new List<Planet>();
        }

        public int UserId { get; set; }
        public User User { get; set; }

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

        public string TechIdList { get; private set; }
        [NotMapped]
        public List<Tech> Techs
        {
            get
            {
                return TechIdList.DeserializeIds().Select(x => Game.Configuration.Techs.Single(tech => tech.Id == x)).ToList();
            }
            set
            {
                TechIdList = value.Select(x => x.Id).SerializeIds();
            }
        }

        public ControlModel ControlModel
        {
            get
            {
                return Race.BaseControlModel + Techs.Select(x => x.ControlModelModifier).Aggregate(new ControlModel(), (a, b) => a + b);
            }
        }
        
        ICollection<Admiral> Admirals { get; set; }
        ICollection<Planet> Planets { get; set; }
    }
}
