using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public class GameInstance : Entity
    {
        public GameInstance()
        {
            TickId = 0;
            LastTick = DateTime.UtcNow;

        }

        public int TickId { get; private set; }
        public DateTime LastTick { get; private set; }

        public void RunTick()
        {

        }
        
        public ICollection<Player> Characters { get; set; }
    }
}
