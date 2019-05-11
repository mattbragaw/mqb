using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Models
{
    public class DataType : OrgChild, IDataType
    {
        public DataType(string id, bool system, string orgId, string name, string namePlural, string nameSystem, string nameSystemPlural) : 
            this(id, system, orgId, name, namePlural, nameSystem, nameSystemPlural, null, null) { }
        public DataType(string id, bool system, string orgId, string name, string namePlural, string nameSystem, string nameSystemPlural, IEnumerable<IDataProperty> properties, IEnumerable<IDataRelation> relations) :
            this(id, system, orgId, name, namePlural, nameSystem, nameSystemPlural, properties, relations, null)
        { }
        public DataType(string id, bool system, string orgId, string name, string namePlural, string nameSystem, string nameSystemPlural, IEnumerable<IDataProperty> properties, IEnumerable<IDataRelation> relations, IEnumerable<IDataRow> rows) :
            this(id, system, orgId, name, namePlural, nameSystem, nameSystemPlural, properties, relations, rows, null)
        { }
        public DataType(string id, bool system, string orgId, string name, string namePlural, string nameSystem, string nameSystemPlural, IEnumerable<IDataProperty> properties, IEnumerable<IDataRelation> relations, IEnumerable<IDataRow> rows, IOrg parentOrg) : 
            base(id, orgId, name, parentOrg)
        {
            System = system;
            NamePlural = namePlural;
            NameSystem = nameSystem;
            NameSystemPlural = nameSystemPlural;
            Properties = properties;
            Relations = relations;
            Rows = rows;
        }

        public bool System { get; }
        public string NamePlural { get; }
        public string NameSystem { get; }
        public string NameSystemPlural { get; }
        public IEnumerable<IDataProperty> Properties { get; }
        public IEnumerable<IDataRelation> Relations { get; }
        public IEnumerable<IDataRow> Rows { get; }
    }
    public class DataTypeMutable : OrgChildMutable, IDataTypeMutable
    {
        public virtual bool System { get; set; }
        public virtual string NamePlural { get; set; }
        public virtual string NameSystem { get; set; }
        public virtual string NameSystemPlural { get; set; }
        public virtual IList<IDataPropertyMutable> Properties { get; set; } = new List<IDataPropertyMutable>();
        public virtual IList<IDataRelationMutable> Relations { get; set; } = new List<IDataRelationMutable>();
        public virtual IList<IDataRowMutable> Rows { get; set; } = new List<IDataRowMutable>();
    }
}
