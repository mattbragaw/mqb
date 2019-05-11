using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors.Models
{
    public interface IOrg : IOrgChild,
        IHasSystem_R,
        IHasNameShort_R,
        IHasNameShortFull_R,
        IHasChildOrgs_R
    {
    }
    public interface IOrgMutable : IOrgChildMutable,
        IHasSystem_RW,
        IHasNameShort_RW,
        IHasNameShortFull_RW,
        IHasChildOrgs_RW
    {
    }
}
