using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Archspace2
{
    [Table("PlayerRelation")]
    public class PlayerRelation : Relation
    {
        public int FromPlayerId { get; private set; }
        [ForeignKey("FromPlayerId")]
        public Player FromPlayer { get; private set; }

        public int ToPlayerId { get; private set; }
        [ForeignKey("ToPlayerId")]
        public Player ToPlayer { get; private set; }

        public PlayerRelation(Universe aUniverse, Player aPlayer1, Player aPlayer2, RelationType aRelationType, int aExpiryTurn) : base(aUniverse)
        {
            FromPlayer = aPlayer1;
            ToPlayer = aPlayer2;

            Type = aRelationType;

            ExpiryTurn = aExpiryTurn;
        }
    }
}
