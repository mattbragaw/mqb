using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasParentSpaceId_R
    {
        string ParentSpaceId { get; }
    }
    public interface IHasParentSpaceId_RW
    {
        string ParentSpaceId { get; set; }
    }
}
