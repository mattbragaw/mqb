using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Descriptors
{
    public interface IHasId_R
    {
        string Id { get; }
    }
    public interface IHasId_RW
    {
        string Id { get; set; }
    }
}
