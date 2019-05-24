using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors.Models
{
    public interface IDataRefs :
        IHasDataRef_R,
        IHasEntitiesRef_R,
        IHasDataTypesRef_R,
        IHasDataRowsRef_R,
        IHasDataRelationsRef_R,
        IHasDataReferencesRef_R,
        IHasOrgsRef_R,
        IHasSpacesRef_R
    {
    }
    public interface IDataRefsMutable : IDataRefs,
        IHasDataRef_RW,
        IHasEntitiesRef_RW,
        IHasDataTypesRef_RW,
        IHasDataRowsRef_RW,
        IHasDataRelationsRef_RW,
        IHasDataReferencesRef_RW,
        IHasOrgsRef_RW,
        IHasSpacesRef_RW
    {
    }
}
