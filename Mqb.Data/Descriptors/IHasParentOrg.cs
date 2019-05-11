using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasParentOrg_R
    {
        IOrg ParentOrg { get; }
    }
    public interface IHasParentOrg_RW
    {
        IOrgMutable ParentOrg { get; set; }
    }
}
