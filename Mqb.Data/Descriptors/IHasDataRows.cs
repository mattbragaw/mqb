using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasDataRows_R
    {
        IEnumerable<IDataRow> Rows { get; }
    }
    public interface IHasDataRows_RW
    {
        IList<IDataRowMutable> Rows { get; set; }
    }
}
