using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Models
{
    public abstract class BaseNamedPlural : BaseNamed, IBaseNamedPlural
    {
        public BaseNamedPlural(string id, string name, string namePlural) : base(id, name)
        {
            NamePlural = namePlural;
        }

        public string NamePlural { get; }
    }
    public abstract class BaseNamedPluralMutable : BaseNamedMutable, IBaseNamedPluralMutable
    {
        public virtual string NamePlural { get; set; }
    }
}
