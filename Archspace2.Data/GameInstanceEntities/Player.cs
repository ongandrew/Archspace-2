using Archspace2.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Archspace2
{
    public enum ConcentrationMode
    {
        Balanced,
        Industry,
        Military,
        Research
    }

    public class Player : UniverseEntity
    {
        public Player()
        {
            Techs = new List<Tech>();
            Admirals = new List<Admiral>();
            Planets = new List<Planet>();
        }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public int CouncilId { get; set; }
        [ForeignKey("CouncilId")]
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

        public ConcentrationMode ConcentrationMode { get; set; }

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

        public ControlModel ControlModel
        {
            get
            {
                ControlModel result = Race.BaseControlModel;

                result += Techs.CalculateControlModelModifier();
                result += Projects.CalculateControlModelModifier();
                result += Council.Projects.CalculateControlModelModifier();

                if (Planets.Any(x => x.PlanetAttributes.Select(y => y.Type).Contains(PlanetAttributeType.MajorSpaceCrossroute)))
                {
                    result.Commerce += 1;
                }

                result += ConcentrationMode.GetControlModelModifier();

                return result;
            }
        }

        public int MailboxId { get; set; }
        [ForeignKey("MailboxId")]
        public Mailbox Mailbox { get; set; }

        public bool EvaluatePrerequisites(IPlayerUnlockable aPlayerUnlockable)
        {
            return EvaluatePrerequisites(aPlayerUnlockable.Prerequisites);
        }
        public bool EvaluatePrerequisites(List<PlayerPrerequisite> aPrerequisites)
        {
            return aPrerequisites.Evaluate(this);
        }
        
        public ICollection<Admiral> Admirals { get; set; }
        public ICollection<Planet> Planets { get; set; }
    }
}
