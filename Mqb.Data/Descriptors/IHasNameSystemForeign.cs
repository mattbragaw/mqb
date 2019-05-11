using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasNameSystemForeign_R
    {
        string NameSystemForeign { get; }
    }
    public interface IHasNameSystemForeign_RW
    {
        string NameSystemForeign { get; set; }
    }
}
