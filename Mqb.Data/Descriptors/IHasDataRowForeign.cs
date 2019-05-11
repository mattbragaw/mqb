using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasDataRowForeign_R
    {
        IDataRow RowForeign { get; }
    }
    public interface IHasDataRowForeign_RW
    {
        IDataRowMutable RowForeign { get; set; }
    }
}
