using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasPageSize_R
    {
        int PageSize { get; }
    }
    public interface IHasPageSize_RW
    {
        int PageSize { get; set; }
    }
}
