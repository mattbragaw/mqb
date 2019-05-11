using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Models
{
    public class DataRow : DataTypeChild, IDataRow
    {
        public DataRow(string id, string dataTypeId) : 
            this(id, dataTypeId, null)
        {
        }
        public DataRow(string id, string dataTypeId, IEnumerable<IDataValue> values) :
            this(id, dataTypeId, values, null)
        {
        }
        public DataRow(string id, string dataTypeId, IEnumerable<IDataValue> values, IEnumerable<IDataReference> references) :
            this(id, dataTypeId, values, references, null)
        {
        }
        public DataRow(string id, string dataTypeId, IEnumerable<IDataValue> values, IEnumerable<IDataReference> references, IDataType dataType) : 
            base(id, dataTypeId, dataType)
        {
            Values = values;
            References = references;
        }

        public IEnumerable<IDataValue> Values { get; }
        public IEnumerable<IDataReference> References { get; }
    }
    public class DataRowMutable : DataTypeChildMutable, IDataRowMutable
    {
        public virtual IList<IDataValueMutable> Values { get; set; } = new List<IDataValueMutable>();
        public virtual IList<IDataReferenceMutable> References { get; set; } = new List<IDataReferenceMutable>();
    }
}
