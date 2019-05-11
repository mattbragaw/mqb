using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors.Models
{
    public interface IDataType : IOrgChild, ISpaceChild,
        IHasNamePlural_R,
        IHasNameSystem_R,
        IHasNameSystemPlural_R,
        IHasDataProperties_R,
        IHasDataRelations_R,
        IHasDataRows_R
    {
    }
    public interface IDataTypeMutable : IOrgChildMutable, ISpaceChildMutable,
        IHasNamePlural_RW,
        IHasNameSystem_RW,
        IHasNameSystemPlural_RW,
        IHasDataProperties_RW,
        IHasDataRelations_RW,
        IHasDataRows_RW
    {
    }
}
