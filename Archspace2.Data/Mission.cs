using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public enum MissionType
    {
        None,
        Train,
        StationOnPlanet,
        Patrol,
        Expedition,
        DispatchToAlly,
        ReturningWithPlanet,
        Sortie,
        Returning,
        Privateer,
        OnRoute
    };
    
    public class Mission
    {
        public MissionType Type { get; set; }
        public int Target { get; set; }

        public int TerminateTurn { get; set; }

        public void Delay(int aTurns)
        {
            TerminateTurn += aTurns;
        }

        public void Reset()
        {
            Type = MissionType.None;
            Target = 0;
            TerminateTurn = 0;
        }
    }
}
