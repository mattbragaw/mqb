using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasRequired_R
    {
        bool Required { get; }
    }
    public interface IHasRequired_RW
    {
        bool Required { get; set; }
    }
}
