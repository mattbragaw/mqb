using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasDataRelations_R
    {
        IEnumerable<IDataRelation> Relations { get; }
    }
    public interface IHasDataRelations_RW
    {
        IList<IDataRelationMutable> Relations { get; set; }
    }
}
