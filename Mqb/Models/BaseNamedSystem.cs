using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Models
{
    public abstract class BaseNamedSystem : BaseNamed, IBaseNamedSystem
    {
        public BaseNamedSystem(string id, string name, string nameSystem) : base(id, name)
        {
            NameSystem = nameSystem;
        }

        public string NameSystem { get; }
    }
    public abstract class BaseNamedSystemMutable : BaseNamedMutable, IBaseNamedSystemMutable
    {
        public virtual string NameSystem { get; set; }
    }
}
