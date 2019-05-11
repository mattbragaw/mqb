using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasMultiplicityForeign_R
    {
        long MinForeign { get; }
        long? MaxForeign { get; }
    }
    public interface IHasMultiplicityForeign_RW
    {
        long MinForeign { get; set; }
        long? MaxForeign { get; set; }
    }
}
