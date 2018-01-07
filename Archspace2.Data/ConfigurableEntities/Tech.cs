using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public class Tech : Entity
    {
        public Tech()
        {
            Prerequisites = new List<PlayerPrerequisite>();
        }

        public int TechLevel { get; set; }
        public List<PlayerPrerequisite> Prerequisites { get; set; }
    }
}
