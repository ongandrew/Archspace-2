using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public class Council : GameInstanceEntity
    {
        public string Slogan { get; set; }
        ICollection<Player> Characters { get; set; }
    }
}
