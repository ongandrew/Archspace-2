using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public class Fleet : UniverseEntity
    {
        public Player Owner { get; set; }
        public Admiral Commander { get; set; }
    }
}
