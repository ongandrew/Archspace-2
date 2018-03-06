using Archspace2.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Archspace2
{
    [Table("Council")]
    public class Council : UniverseEntity
    {
        public string Name { get; set; }
        public int Honor { get; set; }

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
        public Resource Resource { get; set; }

        public string ProjectIdList
        {
            get
            {
                return mProjects.Select(x => x.Id).SerializeIds();
            }
            private set
            {
                mProjects = value.DeserializeIds().Select(x => Game.Configuration.Projects.Single(project => project.Id == x)).ToList();
            }
        }
        [NotMapped]
        private List<Project> mProjects;
        [NotMapped]
        public List<Project> Projects
        {
            get
            {
                return mProjects;
            }
            set
            {
                ProjectIdList = value.Select(x => x.Id).SerializeIds();
                mProjects = value;
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

        public Council(Universe aUniverse) : base(aUniverse)
        {
            mProjects = new List<Project>();

            Resource = new Resource();

            FromRelations = new List<CouncilRelation>();
            Players = new List<Player>();
            ToRelations = new List<CouncilRelation>();
        }

        public string GetDisplayName()
        {
            return $"{Name} (#{Id})";
        }

        public long CalculateTotalPower()
        {
            return Players.CalculateTotalPower();
        }

        public int CalculateTotalPlanetCount()
        {
            return Players.Sum(x => x.Planets.Count);
        }
    }
}
