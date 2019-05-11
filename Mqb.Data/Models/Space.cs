using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Models
{
    public class Space : SpaceChild, ISpace
    {
        public Space(string id, bool system, string parentOrgId, string parentSpaceId, string name, string nameSystem, string nameSystemFull) : 
            this(id, system, parentOrgId, parentSpaceId, name, nameSystem, nameSystemFull, null, null)
        {
        }
        public Space(string id, bool system, string parentOrgId, string parentSpaceId, string name, string nameSystem, string nameSystemFull, IOrg parentOrg, ISpace parentSpace) : 
            base(id, system, parentSpaceId, nameSystem, nameSystemFull, parentSpace)
        {
            ParentOrgId = parentOrgId;
            Name = name;
            ParentOrg = parentOrg;
        }
        
        public string ParentOrgId { get; }
        public string Name { get; }
        public IOrg ParentOrg { get; }
    }
    public class SpaceMutable : SpaceChildMutable, ISpaceMutable
    {
        public virtual string ParentOrgId { get; set; }
        public virtual string Name { get; set; }
        public virtual IOrgMutable ParentOrg { get; set; }
    }
}
