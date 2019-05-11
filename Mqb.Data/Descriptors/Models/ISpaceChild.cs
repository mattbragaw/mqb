using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors.Models
{
    public interface ISpaceChild : IBase,
        IHasSystem_R,
        IHasNameSystem_R,
        IHasNameSystemFull_R,
        IHasParentSpaceId_R,
        IHasParentSpace_R
    {
    }
    public interface ISpaceChildMutable : IBaseMutable,
        IHasSystem_RW,
        IHasNameSystem_RW,
        IHasNameSystemFull_RW,
        IHasParentSpaceId_RW,
        IHasParentSpace_RW
    {
    }
}
