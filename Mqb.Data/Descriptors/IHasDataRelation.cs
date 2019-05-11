using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasDataRelation_R
    {
        IDataRelation Relation { get; }
    }
    public interface IHasDataRelation_RW
    {
        IDataRelationMutable Relation { get; set; }
    }
}
