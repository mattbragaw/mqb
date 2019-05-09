using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasNextPage_R
    {
        bool HasNextPage { get; }
    }
    public interface IHasNextPage_RW
    {
        bool HasNextPage { get; set; }
    }
}
