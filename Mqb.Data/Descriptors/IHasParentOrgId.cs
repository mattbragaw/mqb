using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasParentOrgId_R
    {
        string ParentOrgId { get; }
    }
    public interface IHasParentOrgId_RW
    {
        string ParentOrgId { get; set; }
    }
}
