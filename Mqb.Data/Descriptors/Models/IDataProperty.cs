using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors.Models
{
    public interface IDataProperty : IDataTypeChild,
        IHasSystem_R,
        IHasName_R,
        IHasNameSystem_R,
        IHasIndex_R,
        IHasRequired_R,
        IHasValueType_R
    {
    }
    public interface IDataPropertyMutable : IDataTypeChildMutable,
        IHasSystem_RW,
        IHasName_RW,
        IHasNameSystem_RW,
        IHasIndex_RW,
        IHasRequired_RW,
        IHasValueType_RW
    {
    }
}
