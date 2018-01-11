using System.Collections.Generic;
using System.Linq;

namespace Archspace2
{
    public enum Severity
    {
        None,
        Trivial,
        Warning,
        Error
    };

    public class ValidateResult
    {
        public ValidateResult()
        {
            Items = new List<Item>();
        }

        public bool IsPassResult()
        {
            return !Items.Where(x => x.Severity == Severity.Error).Any();
        }

        public List<Item> Items;

        public class Item {
            public Severity Severity { get; set; }
            public string Message { get; set; }
        }
    }
}
