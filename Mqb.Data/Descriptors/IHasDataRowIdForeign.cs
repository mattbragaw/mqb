using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasDataRowIdForeign_R
    {
        string RowIdForeign { get; }
    }
    public interface IHasDataRowIdForeign_RW
    {
        string RowIdForeign { get; set; }
    }
}
