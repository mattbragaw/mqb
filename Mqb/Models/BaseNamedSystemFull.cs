using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Models
{
    public abstract class BaseNamedSystemFull : BaseNamedSystem, IBaseNamedSystemFull
    {
        public BaseNamedSystemFull(string id, string name, string nameSystem, string nameSystemFull) : base(id, name, nameSystem)
        {
            NameSystemFull = nameSystemFull;
        }

        public string NameSystemFull { get; }
    }
    public abstract class BaseNamedSystemFullMutable : BaseNamedSystemMutable, IBaseNamedSystemFullMutable
    {
        public virtual string NameSystemFull { get; set; }
    }
}
