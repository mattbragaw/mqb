using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasNameShort_R
    {
        string NameShort { get; }
    }
    public interface IHasNameShort_RW
    {
        string NameShort { get; set; }
    }
}
