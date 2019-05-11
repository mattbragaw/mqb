using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasDataRowId_R
    {
        string RowId { get; }
    }
    public interface IHasDataRowId_RW
    {
        string RowId { get; set; }
    }
}
