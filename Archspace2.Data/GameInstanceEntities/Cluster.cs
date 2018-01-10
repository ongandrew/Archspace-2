using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public class Cluster : UniverseEntity
    {
        ICollection<Planet> Planets { get; set; }
    }
}
