using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasTotalPages_R
    {
        int TotalPages { get; }
    }
    public interface IHasTotalPages_RW
    {
        int TotalPages { get; set; }
    }
}
