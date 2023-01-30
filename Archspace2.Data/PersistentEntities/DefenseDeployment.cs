using System.ComponentModel.DataAnnotations.Schema;

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

		public DefenseDeployment() : this(null) { }
		public DefenseDeployment(Universe aUniverse) : base(aUniverse)
        {
        }

        public Battle.Fleet ToBattleFleet(Side aSide = Side.Defense)
        {
            Battle.Fleet result = Fleet.ToBattleFleet();

            if (aSide == Side.Defense)
            {
                result.X = X;
                result.Y = Y;
                result.Direction = 180;
            }
            else
            {
                result.X = 5000 - (X - 5000);
                result.Y = 5000 - (Y - 5000);
                result.Direction = 0;
            }
            
            result.IsCapital = Type == DefenseDeploymentType.Capital;
            result.Command = Command;

            return result;
        }
    }
}
