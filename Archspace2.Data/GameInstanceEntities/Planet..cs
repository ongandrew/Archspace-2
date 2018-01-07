using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Archspace2
{
    public class Planet : GameInstanceEntity
    {
        public int ClusterId { get; set; }

        [ForeignKey("ClusterId")]
        public Cluster Cluster { get; set; }
    }
}
