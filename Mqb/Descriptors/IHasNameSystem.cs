using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasNameSystem_R
    {
        string NameSystem { get; }
    }
    public interface IHasNameSystem_RW
    {
        string NameSystem { get; set; }
    }
}
