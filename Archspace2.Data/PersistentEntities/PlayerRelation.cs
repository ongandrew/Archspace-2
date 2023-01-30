using System.ComponentModel.DataAnnotations.Schema;

namespace Archspace2
{
    [Table("PlayerRelation")]
    public class PlayerRelation : Relation
    {
        [ForeignKey("FromId")]
        public Player FromPlayer { get; private set; }
        [ForeignKey("ToId")]
        public Player ToPlayer { get; private set; }

        public PlayerRelation()
        {
        }
        public PlayerRelation(Universe aUniverse, Player aPlayer1, Player aPlayer2, RelationType aRelationType, int aExpiryTurn = 0) : base(aUniverse)
        {
            FromPlayer = aPlayer1;
            ToPlayer = aPlayer2;

            Type = aRelationType;

            ExpiryTurn = aExpiryTurn;
        }
    }
}
