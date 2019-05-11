using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasParentSpace_R
    {
        ISpace ParentSpace { get; }
    }
    public interface IHasParentSpace_RW
    {
        ISpaceMutable ParentSpace { get; set; }
    }
}
