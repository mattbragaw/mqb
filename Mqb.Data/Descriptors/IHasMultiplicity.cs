using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasMultiplicity_R
    {
        long Min { get; }
        long? Max { get; }
    }
    public interface IHasMultiplicity_RW
    {
        long Min { get; set; }
        long? Max { get; set; }
    }
}
