using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors.Models
{
    public interface IOrgChild : IBaseNamed, 
        IHasParentOrgId_R, 
        IHasParentOrg_R
    {
    }
    public interface IOrgChildMutable : IBaseNamedMutable, 
        IHasParentOrgId_RW, 
        IHasParentOrg_RW
    {
    }
}
