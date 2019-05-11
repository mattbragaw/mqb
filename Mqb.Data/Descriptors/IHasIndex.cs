using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasIndex_R
    {
        int Index { get; }
    }
    public interface IHasIndex_RW
    {
        int Index { get; set; }
    }
}
