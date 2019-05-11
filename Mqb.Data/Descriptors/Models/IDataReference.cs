using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors.Models
{
    public interface IDataReference : IDataRowChild, 
        IHasDataRelationType_R
    {
    }
    public interface IDataReferenceMutable : IDataRowChildMutable,
        IHasDataRelationType_R
    {
    }
}
