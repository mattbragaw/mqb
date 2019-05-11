using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Models
{
    public abstract class DataRowChild : Base, IDataRowChild
    {
        public DataRowChild(string id, string rowId) : this(id, rowId, null)
        {
        }
        public DataRowChild(string id, string rowId, IDataRow row) : base(id)
        {
            RowId = rowId;
            Row = row;
        }

        public string RowId { get; }
        public IDataRow Row { get; }
    }
    public abstract class DataRowChildMutable : BaseMutable, IDataRowChildMutable
    {
        public virtual string RowId { get; set; }
        public virtual IDataRowMutable Row { get; set; }
    }
}
