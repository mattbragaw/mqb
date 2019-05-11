using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Models
{
    public class DataReference : DataRowChild, IDataReference
    {
        public DataReference(string id, string rowId, string rowIdForeign, string relationId) : 
            this(id, rowId, rowIdForeign, relationId, null, null, null)
        {
        }
        public DataReference(string id, string rowId, string rowIdForeign, string relationId, IDataRelation relation, IDataRow row, IDataRow rowForeign) : 
            base(id, rowId, row)
        {
            RowIdForeign = rowIdForeign;
            RelationId = relationId;
            Relation = relation;
            RowForeign = rowForeign;
        }

        public string RowIdForeign { get; }
        public string RelationId { get; }
        public IDataRelation Relation { get; }
        public IDataRow RowForeign { get; }
    }
}
