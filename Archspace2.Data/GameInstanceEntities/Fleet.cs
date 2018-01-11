using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
    }

    public class Fleet : UniverseEntity
    {
        public int? PlayerId { get; set; }
        public int AdmiralId { get; set; }

        public MissionType Mission { get; set; }
        

        [ForeignKey("AdmiralId")]
        public Admiral Admiral { get; set; }
        [ForeignKey("PlayerId")]
        public Player Player { get; set; }
    }
}
