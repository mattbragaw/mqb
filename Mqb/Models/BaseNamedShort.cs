using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Models
{
    public abstract class BaseNamedShort : BaseNamed, IBaseNamedShort
    {
        public BaseNamedShort(string id, string name, string nameShort) : base(id, name)
        {
            NameShort = nameShort;
        }

        public string NameShort { get; }
    }
    public abstract class BaseNamedShortMutable : BaseNamedMutable, IBaseNamedShortMutable
    {
        public virtual string NameShort { get; set; }
    }
}
