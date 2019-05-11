using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasDataProperties_R
    {
        IEnumerable<IDataProperty> Properties { get; }
    }
    public interface IHasDataProperties_RW
    {
        IList<IDataPropertyMutable> Properties { get; set; }
    }
}
