using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors.Models
{
    public interface IBaseNamedShort : IBaseNamed, IHasNameShort_R
    {
    }
    public interface IBaseNamedShortMutable : IBaseNamedMutable, IHasNameShort_RW
    {
    }
}
