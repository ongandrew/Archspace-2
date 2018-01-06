using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public class Admiral : GameInstanceEntity
    {
        public Player Owner { get; set; }

        public int AttackRating { get; set; }
        public int DefenseRating { get; set; }
        public int DetectionRating { get; set; }
    }
}
