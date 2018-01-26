using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Archspace2
{
    public enum DefenseDeploymentType
    {
        Capital,
        Normal
    };

    [Table("DefenseDeployment")]
    public class DefenseDeployment : UniverseEntity
    {
        public int DefensePlanId { get; set; }
        [ForeignKey("DefensePlanId")]
        public DefensePlan DefensePlan { get; set; }

        public int FleetId { get; set; }
        [ForeignKey("FleetId")]
        public Fleet Fleet { get; set; }

        public DefenseDeploymentType Type { get; set; }
        public Command Command { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public DefenseDeployment(Universe aUniverse) : base(aUniverse)
        {
        }
    }
}
