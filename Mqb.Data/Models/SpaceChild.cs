using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Models
{
    public abstract class SpaceChild : Base, ISpaceChild
    {
        public SpaceChild(
            string id,
            bool system,
            string parentSpaceId,
            string nameSystem,
            string nameSystemFull) : 
            this(id, system, parentSpaceId, nameSystem, nameSystemFull, null)
        {
        }
        public SpaceChild(
            string id, 
            bool system, 
            string parentSpaceId, 
            string nameSystem,
            string nameSystemFull,
            ISpace parentSpace) : base(id)
        {
            System = system;
            ParentSpaceId = parentSpaceId;
            NameSystem = nameSystem;
            NameSystemFull = nameSystemFull;
            ParentSpace = parentSpace;
        }

        public bool System { get; }
        public string ParentSpaceId { get; }
        public string NameSystem { get; }
        public string NameSystemFull { get; }
        public ISpace ParentSpace { get; }
    }
    public abstract class SpaceChildMutable : BaseMutable, ISpaceChildMutable
    {
        public virtual bool System { get; set; }
        public virtual string ParentSpaceId { get; set; }
        public virtual string NameSystem { get; set; }
        public virtual string NameSystemFull { get; set; }
        public virtual ISpaceMutable ParentSpace { get; set; }
    }
}
