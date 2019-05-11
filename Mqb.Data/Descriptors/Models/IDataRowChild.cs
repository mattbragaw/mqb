using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors.Models
{
    public interface IDataRowChild : IBase,
        IHasDataRowId_R,
        IHasDataRow_R
    {
    }
    public interface IDataRowChildMutable : IBaseMutable,
        IHasDataRowId_RW,
        IHasDataRow_RW
    {
    }
}
