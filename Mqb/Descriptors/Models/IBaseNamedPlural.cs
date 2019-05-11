using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors.Models
{
    public interface IBaseNamedPlural : IBaseNamed, IHasNamePlural_R
    {
    }
    public interface IBaseNamedPluralMutable : IBaseNamedMutable, IHasNamePlural_R
    {
    }
}
