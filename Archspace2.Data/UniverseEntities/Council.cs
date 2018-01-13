using Archspace2.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Archspace2
{
    public class Council : UniverseEntity
    {
        public Council(Universe aUniverse) : base(aUniverse)
        {
            Players = new List<Player>();
        }

        public int? SpeakerId { get; set; }
        [ForeignKey("SpeakerId")]
        public Player Speaker { get; set; }

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
        
        public ICollection<Player> Players { get; set; }
    }
}
