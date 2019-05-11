using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasDataPropertyId_R
    {
        string PropertyId { get; }
    }
    public interface IHasDataPropertyId_RW
    {
        string PropertyId { get; set; }
    }
}
