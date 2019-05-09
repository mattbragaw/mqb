using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasName_R
    {
        string Name { get; }
    }
    public interface IHasName_RW
    {
        string Name { get; set; }
    }
}
