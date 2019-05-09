using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasTotalCount_R
    {
        int TotalCount { get; }
    }
    public interface IHasTotalCount_RW
    {
        int TotalCount { get; set; }
    }
}
