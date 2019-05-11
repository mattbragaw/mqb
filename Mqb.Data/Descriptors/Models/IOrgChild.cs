using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors.Models
{
    public interface IOrgChild : IBaseNamed,
        IHasSystem_R,
        IHasParentOrgId_R, 
        IHasParentOrg_R
    {
    }
    public interface IOrgChildMutable : IBaseNamedMutable,
        IHasSystem_RW, 
        IHasParentOrgId_RW, 
        IHasParentOrg_RW
    {
    }
}
