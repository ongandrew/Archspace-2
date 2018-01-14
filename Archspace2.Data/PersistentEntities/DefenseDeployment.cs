﻿using System;
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

    public enum DefenseDeploymentCommand
    {
        Normal,
        Formation,
        Penetrate,
        Flank,
        Reserve,
        Free,
        Assault,
        StandGround
    }

    [Table("DefenseDeployment")]
    public class DefenseDeployment : UniverseEntity
    {
        [NotMapped]
        public override string Name { get; set; }

        public int FleetId { get; set; }
        [ForeignKey("FleetId")]
        public Fleet Fleet { get; set; }

        public DefenseDeploymentType Type { get; set; }
        public DefenseDeploymentCommand Command { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public DefenseDeployment(Universe aUniverse) : base(aUniverse)
        {
        }
    }
}
