using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasSystem_R
    {
        bool System { get; }
    }
    public interface IHasSystem_RW
    {
        bool System { get; set; }
    }
}
