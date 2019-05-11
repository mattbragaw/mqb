using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Models
{
    public class Org : OrgChild, IOrg
    {
        public Org(string id, bool system, string orgId, string name, string nameShort, string nameShortFull) : 
            this(id, system, orgId, name, nameShort, nameShortFull, null) { }
        public Org(string id, bool system, string orgId, string name, string nameShort, string nameShortFull, IEnumerable<IOrg> orgs) : 
            this(id, system, orgId, name, nameShort, nameShortFull, orgs, null) { }
        public Org(string id, bool system, string orgId, string name, string nameShort, string nameShortFull, IEnumerable<IOrg> orgs, IOrg parentOrg) : 
            base(id, orgId, name, parentOrg)
        {
            System = system;
            NameShort = nameShort;
            NameShortFull = nameShortFull;
            Orgs = orgs;
        }

        public bool System { get; }
        public string NameShort { get; }
        public string NameShortFull { get; }
        public IEnumerable<IOrg> Orgs { get; }
    }
    public class OrgMutable : OrgChildMutable, IOrgMutable
    {
        public virtual bool System { get; set; }
        public virtual IList<IOrgMutable> Orgs { get; set; } = new List<IOrgMutable>();
        public virtual string NameShort { get; set; }
        public virtual string NameShortFull { get; set; }
    }
}
