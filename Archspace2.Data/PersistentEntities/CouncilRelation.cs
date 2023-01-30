using System.ComponentModel.DataAnnotations.Schema;

namespace Archspace2
{
    [Table("CouncilRelation")]
    public class CouncilRelation : Relation
    {
        [ForeignKey("FromId")]
        public Council FromCouncil { get; private set; }
        [ForeignKey("ToId")]
        public Council ToCouncil { get; private set; }

        public CouncilRelation()
        {
        }
        public CouncilRelation(Universe aUniverse, Council aCouncil1, Council aCouncil2, RelationType aRelationType, int aExpiryTurn = 0) : base(aUniverse)
        {
            FromCouncil = aCouncil1;
            ToCouncil = aCouncil2;

            Type = aRelationType;

            ExpiryTurn = aExpiryTurn;
        }
    }
}
