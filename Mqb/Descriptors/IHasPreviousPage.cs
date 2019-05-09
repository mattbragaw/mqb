using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasPreviousPage_R
    {
        bool HasPreviousPage { get; }
    }
    public interface IHasPreviousPage_RW
    {
        bool HasPreviousPage { get; set; }
    }
}
