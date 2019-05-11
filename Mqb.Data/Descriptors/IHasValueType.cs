using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasValueType_R
    {
        Type ValueType { get; }
    }
    public interface IHasValueType_RW
    {
        Type ValueType { get; set; }
    }
}
