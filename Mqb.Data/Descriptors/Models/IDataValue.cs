using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors.Models
{
    public interface IDataValue : IDataRowChild,
        IHasDataPropertyId_R,
        IHasValue_R,
        IHasDataProperty_R
    {
    }
    public interface IDataValueMutable : IDataRowChildMutable,
        IHasDataPropertyId_RW, 
        IHasValue_RW,
        IHasDataProperty_RW
    {
    }
}
