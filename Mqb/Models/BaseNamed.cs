using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Models
{
    public abstract class BaseNamed : Base, IBaseNamed
    {
        public BaseNamed(string id, string name) : base(id)
        {
            Name = name;
        }

        public string Name { get; }
    }
    public abstract class BaseNamedMutable : BaseMutable, IBaseNamedMutable
    {
        public virtual string Name { get; set; }
    }
}
