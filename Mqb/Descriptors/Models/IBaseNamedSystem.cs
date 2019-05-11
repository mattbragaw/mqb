using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors.Models
{
    public interface IBaseNamedSystem : IBaseNamed, IHasNameSystem_R
    {
    }
    public interface IBaseNamedSystemMutable : IBaseNamedMutable, IHasNameSystem_RW
    {
    }
}
