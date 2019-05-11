using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors.Models
{
    public interface IDataRow : IDataTypeChild,
        IHasDataValues_R,
        IHasDataReferences_R
    {
    }
    public interface IDataRowMutable : IDataTypeChildMutable,
        IHasDataValues_RW,
        IHasDataReferences_RW
    {
    }
}
