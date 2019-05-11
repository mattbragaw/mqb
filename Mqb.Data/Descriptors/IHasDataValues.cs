using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasDataValues_R
    {
        IEnumerable<IDataValue> Values { get; }
    }
    public interface IHasDataValues_RW
    {
        IList<IDataValueMutable> Values { get; set; }
    }
}
