using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public class Fleet : GameInstanceEntity
    {
        public Player Owner { get; set; }
        public Admiral Commander { get; set; }
    }
}
