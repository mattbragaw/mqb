using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Models
{
    public class DataType : OrgChild, IDataType
    {
        public DataType(string id, bool system, string orgId, string parentSpaceId, string name, string namePlural, string nameSystem, string nameSystemPlural, string nameSystemFull) : 
            this(id, system, orgId, parentSpaceId, name, namePlural, nameSystem, nameSystemPlural, nameSystemFull, null, null) { }
        public DataType(string id, bool system, string orgId, string parentSpaceId, string name, string namePlural, string nameSystem, string nameSystemPlural, string nameSystemFull, IEnumerable<IDataProperty> properties, IEnumerable<IDataRelation> relations) :
            this(id, system, orgId, parentSpaceId, name, namePlural, nameSystem, nameSystemPlural, nameSystemFull, properties, relations, null)
        { }
        public DataType(string id, bool system, string orgId, string parentSpaceId, string name, string namePlural, string nameSystem, string nameSystemPlural, string nameSystemFull, IEnumerable<IDataProperty> properties, IEnumerable<IDataRelation> relations, IEnumerable<IDataRow> rows) :
            this(id, system, orgId, parentSpaceId, name, namePlural, nameSystem, nameSystemPlural, nameSystemFull, properties, relations, rows, null, null)
        { }
        public DataType(string id, bool system, string orgId, string parentSpaceId, string name, string namePlural, string nameSystem, string nameSystemPlural, string nameSystemFull, IEnumerable<IDataProperty> properties, IEnumerable<IDataRelation> relations, IEnumerable<IDataRow> rows, IOrg parentOrg, ISpace parentSpace) : 
            base(id, system, orgId, name, parentOrg)
        {
            ParentSpaceId = parentSpaceId;
            NamePlural = namePlural;
            NameSystem = nameSystem;
            NameSystemPlural = nameSystemPlural;
            NameSystemFull = nameSystemFull;
            Properties = properties;
            Relations = relations;
            Rows = rows;
            ParentSpace = parentSpace;
        }

        public string ParentSpaceId { get; }
        public string NamePlural { get; }
        public string NameSystem { get; }
        public string NameSystemPlural { get; }
        public string NameSystemFull { get; }
        public IEnumerable<IDataProperty> Properties { get; }
        public IEnumerable<IDataRelation> Relations { get; }
        public IEnumerable<IDataRow> Rows { get; }
        public ISpace ParentSpace { get; }
    }
    public class DataTypeMutable : OrgChildMutable, IDataTypeMutable
    {
        public virtual string ParentSpaceId { get; set; }
        public virtual string NamePlural { get; set; }
        public virtual string NameSystem { get; set; }
        public virtual string NameSystemPlural { get; set; }
        public virtual string NameSystemFull { get; set; }
        public virtual IList<IDataPropertyMutable> Properties { get; set; } = new List<IDataPropertyMutable>();
        public virtual IList<IDataRelationMutable> Relations { get; set; } = new List<IDataRelationMutable>();
        public virtual IList<IDataRowMutable> Rows { get; set; } = new List<IDataRowMutable>();
        public virtual ISpaceMutable ParentSpace { get; set; }
    }
}
