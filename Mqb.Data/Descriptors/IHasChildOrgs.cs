using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasChildOrgs_R
    {
        IEnumerable<IOrg> Orgs { get; }
    }
    public interface IHasChildOrgs_RW
    {
        IList<IOrgMutable> Orgs { get; set; }
    }
}
