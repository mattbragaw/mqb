using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasDataTypeIdForeign_R
    {
        string DataTypeIdForeign { get; }
    }
    public interface IHasDataTypeIdForeign_RW
    {
        string DataTypeIdForeign { get; set; }
    }
}
