using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasNamePlural_R
    {
        string NamePlural { get; }
    }
    public interface IHasNamePlural_RW
    {
        string NamePlural { get; set; }
    }
}
