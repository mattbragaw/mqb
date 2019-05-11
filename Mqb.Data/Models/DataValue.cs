using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Models
{
    public class DataValue : DataRowChild, IDataValue
    {
        public DataValue(string id, string dataRowId, string propertyId, object value) : 
            this(id, dataRowId, propertyId, value, null, null)
        {
        }
        public DataValue(string id, string dataRowId, string propertyId, object value, IDataRow dataRow, IDataProperty property) : 
            base(id, dataRowId, dataRow)
        {
            PropertyId = propertyId;
            Value = value;
            Property = property;
        }

        public string PropertyId { get; }
        public object Value { get; }
        public IDataProperty Property { get; }
    }
    public class DataValueMutable : DataRowChildMutable, IDataValueMutable
    {
        public virtual string PropertyId { get; set; }
        public virtual object Value { get; set; }
        public virtual IDataPropertyMutable Property { get; set; }
    }
}
