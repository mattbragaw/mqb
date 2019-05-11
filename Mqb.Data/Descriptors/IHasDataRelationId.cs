using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasDataRelationId_R
    {
        string RelationId { get; }
    }
    public interface IHasDataRelationId_RW
    {
        string RelationId { get; set; }
    }
}
