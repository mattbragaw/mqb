using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasDataRowId_R
    {
        string DataRowId { get; }
    }
    public interface IHasDataRowId_RW
    {
        string DataRowId { get; set; }
    }
}
