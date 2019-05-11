using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasDataReferences_R
    {
        IEnumerable<IDataReference> References { get; }
    }
    public interface IHasDataReferences_RW
    {
        IList<IDataReferenceMutable> References { get; set; }
    }
}
