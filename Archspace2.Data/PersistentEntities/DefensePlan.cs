using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Archspace2
{
    [Table("DefensePlan")]
    public class DefensePlan : UniverseEntity, IValidatable
    {
        public DefensePlan(Universe aUniverse) : base(aUniverse)
        {
            DefenseDeployments = new List<DefenseDeployment>();
        }

        public int PlayerId { get; set; }
        [ForeignKey("PlayerId")]
        public Player Player { get; set; }

        public DefenseDeployment CapitalDeployment
        {
            get
            {
                return DefenseDeployments.Single(x => x.Type == DefenseDeploymentType.Capital);
            }
        }
        public ICollection<DefenseDeployment> DefenseDeployments { get; set; }
        
        public ValidateResult Validate()
        {
            ValidateResult result = new ValidateResult();
            
            if (!DefenseDeployments.Any(x => x.Type == DefenseDeploymentType.Capital))
            {
                result.Items.Add(new ValidateResult.Item()
                {
                    Severity = Severity.Error,
                    Message = "No capital deployment selected."
                });
            }
            else if (Math.Abs(CapitalDeployment.X - 8000) > 10 || Math.Abs(CapitalDeployment.Y - 5000) > 10)
            {
                result.Items.Add(new ValidateResult.Item()
                {
                    Severity = Severity.Error,
                    Message = $"Capital deployment not at (8000, 5000). Found at ({CapitalDeployment.X}, {CapitalDeployment.Y})"
                });
            }
            
            if (DefenseDeployments.Any(x => x.X < 7000 || x.X > 9000 || x.Y < 2000 | x.Y > 8000))
            {
                result.Items.Add(new ValidateResult.Item()
                {
                    Severity = Severity.Error,
                    Message = "Fleet deployment out of bounds."
                });
            }

            if (DefenseDeployments.Count > 20)
            {
                result.Items.Add(new ValidateResult.Item()
                {
                    Severity = Severity.Error,
                    Message = "More than 20 fleets selected."
                });
            }

            return result;
        }

        public Battle.Armada ToBattleArmada(Side aSide = Side.Defense)
        {
            Battle.Armada result = new Battle.Armada(Player.ToBattlePlayer());

            foreach (DefenseDeployment deployment in DefenseDeployments)
            {
                Battle.Fleet fleet = deployment.ToBattleFleet(aSide);
                fleet.Armada = result;

                result.Add(fleet);
            }

            result.Side = aSide;

            return result;
        }
    }
}
