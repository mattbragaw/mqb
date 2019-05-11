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
        public DataRow(string id, string dataTypeId, IDataType dataType) : 
            base(id, dataTypeId, dataType)
        {
        }
    }
    public class DataRowMutable : DataTypeChildMutable, IDataRowMutable
    {
    }
}
