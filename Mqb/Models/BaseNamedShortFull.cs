using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Models
{
    public abstract class BaseNamedShortFull : BaseNamedShort, IBaseNamedShortFull
    {
        public BaseNamedShortFull(string id, string name, string nameShort, string nameShortFull) : base(id, name, nameShort)
        {
            NameShortFull = nameShortFull;
        }

        public string NameShortFull { get; }
    }
    public abstract class BaseNamedShortFullMutable : BaseNamedShortMutable, IBaseNamedShortFullMutable
    {
        public virtual string NameShortFull { get; set; }
    }
}
