using System.ComponentModel.DataAnnotations.Schema;

namespace Archspace2
{
    [Table("Empire")]
    public class Empire : UniverseEntity
	{
		public Empire() : this(null) { }
		public Empire(Universe aUniverse) : base(aUniverse)
        {
        }
    }
}
