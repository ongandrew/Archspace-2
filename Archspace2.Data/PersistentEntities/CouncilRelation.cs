using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Archspace2
{
    [Table("CouncilRelation")]
    public class CouncilRelation : Relation
    {
        public int FromCounilId { get; private set; }
        [ForeignKey("FromCounilId")]
        public Council FromCouncil { get; private set; }

        public int ToCouncilId { get; private set; }
        [ForeignKey("ToCouncilId")]
        public Council ToCouncil { get; private set; }

        public CouncilRelation(Universe aUniverse, Council aCouncil1, Council aCouncil2, RelationType aRelationType, int aExpiryTurn) : base(aUniverse)
        {
            FromCouncil = aCouncil1;
            ToCouncil = aCouncil2;

            Type = aRelationType;

            ExpiryTurn = aExpiryTurn;
        }
    }
}
