using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Archspace2
{
    public enum ActionType
    {
        None,
        PlayerCouncilDonation,
        PlayerBreakAlly,
        PlayerBreakPact,
        CouncilDeclareTotalWar,
        CouncilDeclareWar,
        CouncilBreakAlly,
        CouncilBreakPact,
        CouncilImproveRelation,
        EmpireBribe,
        CouncilMergingOffer,
        PlayerSiegeBlockadeRestriction,
        PlayerSiegeBlockadeProtection,
        PlayerEmpireInvasionHistory,
        PlayerDeclareHostile
    };

    [Table("Action")]
    public class Action: UniverseEntity
    {
        public ActionType Type { get; set; }
        public DateTime DateTime { get; private set; }
        public int Turn { get; private set; }

        internal Action()
        {
        }
        public Action(Universe aUniverse) : base(aUniverse)
        {
            DateTime = DateTime.UtcNow;
            Turn = aUniverse.CurrentTurn;
        }
    }
}
