using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasDataProperty_R
    {
        IDataProperty Property { get; }
    }
    public interface IHasDataProperty_RW
    {
        IDataPropertyMutable Property { get; set; }
    }
}
