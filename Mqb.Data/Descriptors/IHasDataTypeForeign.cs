using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasDataTypeForeign_R
    {
        IDataType DataTypeForeign { get; }
    }
    public interface IHasDataTypeForeign_RW
    {
        IDataTypeMutable DataTypeForeign { get; set; }
    }
}
