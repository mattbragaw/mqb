using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasDataRelationType_R
    {
        DataRelationType DataRelationType { get; }
    }
    public interface IHasDataRelationType_RW
    {
        DataRelationType DataRelationType { get; set; }
    }
}
