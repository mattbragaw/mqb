using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Models
{
    public abstract class DataRowChild : Base, IDataRowChild
    {
        public DataRowChild(string id, string dataRowId) : this(id, dataRowId, null)
        {
        }
        public DataRowChild(string id, string dataRowId, IDataRow dataRow) : base(id)
        {
            DataRowId = dataRowId;
            DataRow = dataRow;
        }

        public string DataRowId { get; }
        public IDataRow DataRow { get; }
    }
    public abstract class DataRowChildMutable : BaseMutable, IDataRowChildMutable
    {
        public virtual string DataRowId { get; set; }
        public virtual IDataRowMutable DataRow { get; set; }
    }
}
