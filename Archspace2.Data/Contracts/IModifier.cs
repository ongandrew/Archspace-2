using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public interface IModifier
    {
        ModifierType ModifierType { get; set; }
    }
}
