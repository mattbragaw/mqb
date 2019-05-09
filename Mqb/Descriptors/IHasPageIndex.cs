using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasPageIndex_R
    {
        int PageIndex { get; }
    }
    public interface IHasPageIndex_RW
    {
        int PageIndex { get; set; }
    }
}
