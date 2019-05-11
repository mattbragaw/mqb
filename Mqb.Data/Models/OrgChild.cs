using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Models
{
    public abstract class OrgChild : BaseNamed, IOrgChild
    {
        public OrgChild(string id, bool system, string orgId, string name) : this(id, system, orgId, name, null) { }
        public OrgChild(string id, bool system, string orgId, string name, IOrg parentOrg) : base(id, name)
        {
            System = system;
            ParentOrgId = orgId;
            ParentOrg = parentOrg;
        }

        public bool System { get; }
        public string ParentOrgId { get; }
        public IOrg ParentOrg { get; }
    }
    public abstract class OrgChildMutable : BaseNamedMutable, IOrgChildMutable
    {
        public virtual bool System { get; set; }
        public virtual string ParentOrgId { get; set; }
        public virtual IOrgMutable ParentOrg { get; set; }
    }
}
