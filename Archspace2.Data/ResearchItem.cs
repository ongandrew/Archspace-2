using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public class ResearchItem : Entity
    {
        public ResearchItem()
        {
            Dependencies = new List<ResearchItem>();
        }

        public int TechLevel { get; set; }
        public List<ResearchItem> Dependencies { get; set; }
    }
}
