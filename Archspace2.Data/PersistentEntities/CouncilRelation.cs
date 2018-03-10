using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Archspace2
{
    [Table("CouncilRelation")]
    public class CouncilRelation : Relation
    {
        [ForeignKey("FromId")]
        public Council FromCouncil { get; private set; }
        [ForeignKey("ToId")]
        public Council ToCouncil { get; private set; }

        internal CouncilRelation()
        {
        }
        public CouncilRelation(Universe aUniverse, Council aCouncil1, Council aCouncil2, RelationType aRelationType, int aExpiryTurn) : base(aUniverse)
        {
            FromCouncil = aCouncil1;
            ToCouncil = aCouncil2;

            Type = aRelationType;

            ExpiryTurn = aExpiryTurn;
        }
    }
}
