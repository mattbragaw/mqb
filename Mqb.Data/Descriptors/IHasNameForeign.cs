using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasNameForeign_R
    {
        string NameForeign { get; }
    }
    public interface IHasNameForeign_RW
    {
        string NameForeign { get; set; }
    }
}
