using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors.Models
{
    public interface IDataType : IOrgChild,
        IHasNamePlural_R,
        IHasNameSystem_R,
        IHasNameSystemPlural_R,
        IHasSystem_R,
        IHasDataProperties_R,
        IHasDataRelations_R,
        IHasDataRows_R
    {
    }
    public interface IDataTypeMutable : IOrgChildMutable,
        IHasNamePlural_RW,
        IHasNameSystem_RW,
        IHasNameSystemPlural_RW, 
        IHasSystem_RW,
        IHasDataProperties_RW,
        IHasDataRelations_RW,
        IHasDataRows_RW
    {
    }
}
