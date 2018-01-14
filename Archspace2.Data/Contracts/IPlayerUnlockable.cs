using System.Collections.Generic;

namespace Archspace2
{
    public interface IPlayerUnlockable
    {
        List<PlayerPrerequisite> Prerequisites { get; set; }
    }
}
