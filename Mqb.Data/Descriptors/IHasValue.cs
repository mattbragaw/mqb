using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasValue_R
    {
        object Value { get; }
    }
    public interface IHasValue_RW
    {
        object Value { get; set; }
    }
}
