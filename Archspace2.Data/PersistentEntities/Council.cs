using Archspace2.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Archspace2
{
    [Table("Council")]
    public class Council : UniverseEntity
    {
        public Council(Universe aUniverse) : base(aUniverse)
        {
            FromRelations = new List<CouncilRelation>();
            Players = new List<Player>();
            ToRelations = new List<CouncilRelation>();
        }

        public int? SpeakerId { get; set; }
        [NotMapped]
        public Player Speaker
        {
            get
            {
                if (SpeakerId == null)
                {
                    return null;
                }
                else
                {
                    return Universe.Players.SingleOrDefault(x => x.Id == SpeakerId);
                }
            }
            set
            {
                SpeakerId = value.Id;
            }
        }

        public string Slogan { get; set; }

        public string ProjectIdList { get; private set; }
        [NotMapped]
        public List<Project> Projects
        {
            get
            {
                return ProjectIdList.DeserializeIds().Select(x => Game.Configuration.Projects.Single(project => project.Id == x)).ToList();
            }
            set
            {
                ProjectIdList = value.Select(x => x.Id).SerializeIds();
            }
        }

        public List<CouncilRelation> Relations
        {
            get
            {
                return FromRelations.Union(ToRelations).ToList();
            }
        }
        
        public ICollection<CouncilRelation> FromRelations { get; set; }
        public ICollection<CouncilRelation> ToRelations { get; set; }
        public ICollection<Player> Players { get; set; }
    }
}
