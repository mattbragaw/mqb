using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Models
{
    public abstract class OrgChild : BaseNamed, IOrgChild
    {
        public OrgChild(string id, string orgId, string name) : this(id, name, orgId, null) { }
        public OrgChild(string id, string orgId, string name, IOrg parentOrg) : base(id, name)
        {
            ParentOrgId = orgId;
            ParentOrg = parentOrg;
        }

        public string ParentOrgId { get; }
        public IOrg ParentOrg { get; }
    }
    public abstract class OrgChildMutable : BaseNamedMutable, IOrgChildMutable
    {
        public virtual string ParentOrgId { get; set; }
        public virtual IOrgMutable ParentOrg { get; set; }
    }
}
