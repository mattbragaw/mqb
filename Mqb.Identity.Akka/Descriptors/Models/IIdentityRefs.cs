using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors.Models
{
    public interface IIdentityRefs : IDataRefs,
        IHasIdentityRef_R,
        IHasIdentityUsersRef_R,
        IHasIdentityRolesRef_R
    {
    }
    public interface IIdentityRefsMutable : IIdentityRefs, IDataRefsMutable,
        IHasIdentityRef_RW,
        IHasIdentityUsersRef_RW,
        IHasIdentityRolesRef_RW
    {
    }
}
