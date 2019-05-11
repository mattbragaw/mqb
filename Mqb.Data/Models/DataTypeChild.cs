using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Models
{
    public abstract class DataTypeChild : Base, IDataTypeChild
    {
        public DataTypeChild(string id, string dataTypeId) : this(id, dataTypeId, null) { }
        public DataTypeChild(string id, string dataTypeId, IDataType dataType) : base(id)
        {
            DataTypeId = dataTypeId;
            DataType = dataType;
        }

        public string DataTypeId { get; }
        public IDataType DataType { get; }
    }
    public abstract class DataTypeChildMutable : BaseMutable, IDataTypeChildMutable
    {
        public virtual string DataTypeId { get; set; }
        public virtual IDataTypeMutable DataType { get; set; }
    }
}
