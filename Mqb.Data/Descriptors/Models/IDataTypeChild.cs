using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors.Models
{
    public interface IDataTypeChild : IBase,
        IHasDataTypeId_R,
        IHasDataType_R
    {
    }
    public interface IDataTypeChildMutable : IBaseMutable,
        IHasDataTypeId_RW,
        IHasDataType_RW
    {
    }
}
