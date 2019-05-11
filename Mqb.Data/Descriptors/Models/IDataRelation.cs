using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors.Models
{
    public interface IDataRelation : IDataTypeChild,
        IHasDataTypeIdForeign_R,
        IHasDataTypeForeign_R,
        IHasName_R,
        IHasNameSystem_R,
        IHasNameForeign_R,
        IHasNameSystemForeign_R,
        IHasDataRelationType_R,
        IHasMultiplicity_R,
        IHasMultiplicityForeign_R
    {
    }
    public interface IDataRelationMutable : IDataTypeChildMutable,
        IHasDataTypeIdForeign_RW,
        IHasDataTypeForeign_RW,
        IHasName_RW,
        IHasNameSystem_RW,
        IHasNameForeign_RW,
        IHasNameSystemForeign_RW,
        IHasDataRelationType_RW,
        IHasMultiplicity_RW,
        IHasMultiplicityForeign_RW
    {
    }
}
