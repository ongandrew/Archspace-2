using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public interface IValidatable
    {
        ValidateResult Validate();
    }
}
