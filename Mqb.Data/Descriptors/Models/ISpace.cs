using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors.Models
{
    public interface ISpace : ISpaceChild, IOrgChild
    {
    }
    public interface ISpaceMutable : ISpaceChildMutable, IOrgChildMutable
    {
    }
}
