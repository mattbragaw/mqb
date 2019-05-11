using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors.Models
{
    public interface IDataReference : IDataRowChild,
        IHasDataRowForeign_R,
        IHasDataRowIdForeign_R,
        IHasDataRelationId_R,
        IHasDataRelation_R
    {
    }
    public interface IDataReferenceMutable : IDataRowChildMutable,
        IHasDataRowForeign_RW,
        IHasDataRowIdForeign_RW,
        IHasDataRelationId_RW,
        IHasDataRelation_RW
    {
    }
}
