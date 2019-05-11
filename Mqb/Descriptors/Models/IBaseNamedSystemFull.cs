using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors.Models
{
    public interface IBaseNamedSystemFull : IBaseNamedSystem, IHasNameSystemFull_R
    {
    }
    public interface IBaseNamedSystemFullMutable : IBaseNamedSystemMutable, IHasNameSystemFull_RW
    {
    }
}
